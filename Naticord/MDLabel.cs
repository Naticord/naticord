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
        private ContextMenuStrip contextMenu;

        public Image MDImage
        {
            get => _mdImage;
            set
            {
                _mdImage = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(BackColor))
                graphics.FillRectangle(brush, ClientRectangle);

            if (string.IsNullOrEmpty(Text)) return;

            var parsedContent = ParseMarkdownContent(Text);
            RenderMarkdownContent(graphics, parsedContent);
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");

            copyItem.Click += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    Clipboard.SetText(Text);
                }
            };

            contextMenu.Items.Add(copyItem);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(this, e.Location);
            }
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

                    segments.Add(new MarkdownSegment(text, font, false, isLink));
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
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
                Trimming = StringTrimming.Word
            };

            Color linkColor = Color.FromArgb(0, 102, 204);
            float x = 0, y = 0;
            int lineHeight = 0;
            int maxWidth = Width;

            foreach (var segment in segments)
            {
                if (segment.IsNewLine)
                {
                    y += lineHeight;
                    x = 0;
                    lineHeight = 0;
                    continue;
                }

                if (!string.IsNullOrEmpty(segment.Text))
                {
                    SizeF textSize = graphics.MeasureString(segment.Text, segment.Font, (int)(maxWidth - x), format);
                    Brush textBrush = segment.IsLink ? new SolidBrush(linkColor) : new SolidBrush(ForeColor);
                    var layoutRect = new RectangleF(x, y, maxWidth - x, Height);

                    if (x + textSize.Width > maxWidth)
                    {
                        y += lineHeight;
                        x = 0;
                        lineHeight = 0;
                        layoutRect = new RectangleF(x, y, maxWidth, Height);
                    }

                    graphics.DrawString(segment.Text, segment.Font, textBrush, layoutRect, format);
                    x += textSize.Width;
                    lineHeight = Math.Max(lineHeight, (int)textSize.Height);
                }
            }

            y += lineHeight;

            if (MDImage != null && !ImageFormat.Gif.Equals(MDImage.RawFormat))
            {
                int imgMaxWidth = 500;
                int newWidth = MDImage.Width;
                int newHeight = MDImage.Height;

                if (newWidth > imgMaxWidth)
                {
                    float scaleFactor = (float)imgMaxWidth / newWidth;
                    newWidth = imgMaxWidth;
                    newHeight = (int)(MDImage.Height * scaleFactor);
                }

                graphics.DrawImage(MDImage, (int)x, (int)y, newWidth, newHeight);
                y += newHeight;
            }

            Height = (int)y;
        }

        private class MarkdownSegment
        {
            public string Text { get; }
            public Font Font { get; }
            public bool IsNewLine { get; }
            public bool IsLink { get; }

            public MarkdownSegment(string text, Font font, bool isNewLine, bool isLink)
            {
                Text = text;
                Font = font;
                IsNewLine = isNewLine;
                IsLink = isLink;
            }
        }
    }
}