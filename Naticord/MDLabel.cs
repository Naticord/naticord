using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Naticord
{
    public partial class MDLabel : Label
    {
        private Image _mdImage;

        public Image MDImage
        {
            get => _mdImage;
            set
            {
                _mdImage = value;
                Invalidate();
            }
        }

        public MDLabel()
        {
            AutoSize = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(BackColor))
                graphics.FillRectangle(brush, ClientRectangle);

            if (string.IsNullOrEmpty(Text) && MDImage == null) return;

            var parsedContent = ParseMarkdownContent(Text);
            RenderMarkdownContent(graphics, parsedContent);
        }

        private List<MarkdownSegment> ParseMarkdownContent(string markdownText)
        {
            var segments = new List<MarkdownSegment>();
            var lines = markdownText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    segments.Add(new MarkdownSegment("", Font, true, false));
                    continue;
                }

                var lineSegments = Regex.Split(line, @"(\*\*\*[^\*]+\*\*\*|\*\*[^\*]+\*\*|\*[^\*]+\*|__[^_]+__|~~[^~]+~~|!\[.*?\]\(.*?\)|\[.*?\]\(https?://[^\s]+\)|https?://[^\s]+)");

                foreach (var segment in lineSegments)
                {
                    if (string.IsNullOrWhiteSpace(segment)) continue;

                    string text = segment.Trim();
                    bool isLink = false;
                    Font font = Font;
                    Image image = null;

                    if (text.StartsWith("[") && text.Contains("](") && text.EndsWith(")"))
                    {
                        isLink = true;
                        font = new Font(Font.FontFamily, Font.Size, FontStyle.Underline);
                        text = Regex.Replace(text, @"\[([^\]]+)\]\([^\)]+\)", "$1");
                    }
                    else if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
                    {
                        isLink = true;
                        font = new Font(Font.FontFamily, Font.Size, FontStyle.Underline);
                    }
                    else
                    {
                        text = ApplyMarkdownFormatting(ref font, text);
                    }

                    segments.Add(new MarkdownSegment(text, font, false, isLink, image));
                }

                segments.Add(new MarkdownSegment("", Font, true, false));
            }

            return segments;
        }

        private string ApplyMarkdownFormatting(ref Font font, string text)
        {
            if (text.StartsWith("### "))
            {
                font = new Font(Font.FontFamily, Font.Size + 4, FontStyle.Bold);
                return text.Substring(4);
            }
            if (text.StartsWith("## "))
            {
                font = new Font(Font.FontFamily, Font.Size + 6, FontStyle.Bold);
                return text.Substring(3);
            }
            if (text.StartsWith("# "))
            {
                font = new Font(Font.FontFamily, Font.Size + 8, FontStyle.Bold);
                return text.Substring(2);
            }
            if (text.StartsWith("***") && text.EndsWith("***"))
            {
                font = new Font(Font, FontStyle.Bold | FontStyle.Italic);
                return text.Substring(3, text.Length - 6);
            }
            if (text.StartsWith("**") && text.EndsWith("**"))
            {
                font = new Font(Font, FontStyle.Bold);
                return text.Substring(2, text.Length - 4);
            }
            if (text.StartsWith("*") && text.EndsWith("*"))
            {
                font = new Font(Font, FontStyle.Italic);
                return text.Substring(1, text.Length - 2);
            }
            if (text.StartsWith("__") && text.EndsWith("__"))
            {
                font = new Font(Font, FontStyle.Underline);
                return text.Substring(2, text.Length - 4);
            }
            if (text.StartsWith("~~") && text.EndsWith("~~"))
            {
                font = new Font(Font, FontStyle.Strikeout);
                return text.Substring(2, text.Length - 4);
            }

            return text;
        }

        private void RenderMarkdownContent(Graphics graphics, List<MarkdownSegment> segments)
        {
            StringFormat format = new StringFormat { FormatFlags = StringFormatFlags.NoWrap };
            Color linkColor = Color.FromArgb(0, 102, 204);
            float x = 0, y = 0;
            int lineHeight = 0, contentWidth = 0;
            bool isFirstWord = true;

            foreach (var segment in segments)
            {
                if (segment.IsNewLine)
                {
                    y += lineHeight;
                    x = 0;
                    lineHeight = 0;
                    isFirstWord = true;
                    continue;
                }

                if (segment.Image != null)
                {
                    int maxWidth = 500;
                    int newWidth = segment.Image.Width;
                    int newHeight = segment.Image.Height;

                    if (newWidth > maxWidth)
                    {
                        float scaleFactor = (float)maxWidth / newWidth;
                        newWidth = maxWidth;
                        newHeight = (int)(segment.Image.Height * scaleFactor);
                    }

                    graphics.DrawImage(segment.Image, (int)x, (int)y, newWidth, newHeight);
                    x += newWidth + 4;
                    lineHeight = Math.Max(lineHeight, newHeight);
                }
                else if (!string.IsNullOrEmpty(segment.Text))
                {
                    string textToDraw = isFirstWord ? segment.Text.TrimStart() : segment.Text;
                    Brush textBrush = segment.IsLink ? new SolidBrush(linkColor) : new SolidBrush(ForeColor);

                    SizeF textSize = graphics.MeasureString(textToDraw, segment.Font, new PointF(x, y), format);
                    graphics.DrawString(textToDraw, segment.Font, textBrush, new PointF(x, y), format);

                    x += textSize.Width;
                    lineHeight = Math.Max(lineHeight, (int)textSize.Height);
                    isFirstWord = false;
                }

                contentWidth = Math.Max(contentWidth, (int)x);
            }

            y += lineHeight;
            x = 0;

            if (MDImage != null)
            {
                bool isGif = ImageFormat.Gif.Equals(MDImage.RawFormat);

                if (isGif)
                {
                    new CMessageBox("A heads up...", "GIFs are currently not supported. This will be added in a later release. This message won't show again.").Show();
                }
                else
                {
                    int maxWidth = 500;
                    int newWidth = MDImage.Width;
                    int newHeight = MDImage.Height;

                    if (newWidth > maxWidth)
                    {
                        float scaleFactor = (float)maxWidth / newWidth;
                        newWidth = maxWidth;
                        newHeight = (int)(MDImage.Height * scaleFactor);
                    }

                    graphics.DrawImage(MDImage, (int)x, (int)y, newWidth, newHeight);
                    contentWidth = Math.Max(contentWidth, newWidth);
                    y += newHeight;
                }
            }

            Width = contentWidth;
            Height = (int)y;
            GC.Collect();
        }

        private class MarkdownSegment
        {
            public string Text { get; }
            public Font Font { get; }
            public bool IsNewLine { get; }
            public bool IsLink { get; }
            public Image Image { get; }

            public MarkdownSegment(string text, Font font, bool isNewLine, bool isLink, Image image = null)
            {
                Text = text;
                Font = font;
                IsNewLine = isNewLine;
                IsLink = isLink;
                Image = image;
            }
        }
    }
}