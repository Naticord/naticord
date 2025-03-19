using System;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            InitializeComponent();
            AutoSize = false;
        }

        protected override async void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;

            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(BackColor))
                graphics.FillRectangle(brush, ClientRectangle);

            if (string.IsNullOrEmpty(Text) && MDImage == null) return;

            var parsedContent = await ParseMarkdownAsync(Text);
            RenderContent(graphics, parsedContent);
        }

        private async Task<(string Text, Font FontType, Image Image, bool IsNewLine, bool IsLink)[]> ParseMarkdownAsync(string markdownText)
        {
            var lines = markdownText.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            var parsedContent = new List<(string Text, Font FontType, Image Image, bool IsNewLine, bool IsLink)>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    parsedContent.Add((string.Empty, Font, null, true, false));
                    continue;
                }

                var segments = Regex.Split(line, @"(\#\#\#\s.*|\#\#\s.*|\#\s.*|\*\*\*.*?\*\*\*|\*\*.*?\*\*|\*.*?\*|__.*?__|\~\~.*?\~\~|\!\[.*?\]\(.*?\)|\[.*?\]\(https?://[^\s]+\)|https?://[^\s]+)");
                foreach (var segment in segments)
                {
                    if (string.IsNullOrWhiteSpace(segment)) continue;

                    string textSegment = segment.Trim();
                    bool isLink = false;
                    Font font = Font;
                    Image image = null;

                    if (textSegment.StartsWith("[") && textSegment.Contains("](") && textSegment.EndsWith(")"))
                    {
                        isLink = true;
                        font = new Font(Font.FontFamily, Font.Size, FontStyle.Underline);
                        textSegment = Regex.Replace(textSegment, @"\[([^\]]+)\]\([^\)]+\)", "$1");
                    }
                    else if (Uri.IsWellFormedUriString(textSegment, UriKind.Absolute))
                    {
                        isLink = true;
                        font = new Font(Font.FontFamily, Font.Size, FontStyle.Underline);
                    }
                    else
                    {
                        textSegment = HandleMarkdownFormatting(ref font, textSegment);
                    }

                    parsedContent.Add((textSegment, font, image, false, isLink));
                }

                parsedContent.Add((string.Empty, Font, null, true, false));
            }

            return parsedContent.ToArray();
        }

        private string HandleMarkdownFormatting(ref Font font, string textSegment)
        {
            if (textSegment.StartsWith("### "))
            {
                font = new Font(Font.FontFamily, Font.Size + 4, FontStyle.Bold);
                return textSegment.Substring(4);
            }
            if (textSegment.StartsWith("## "))
            {
                font = new Font(Font.FontFamily, Font.Size + 6, FontStyle.Bold);
                return textSegment.Substring(3);
            }
            if (textSegment.StartsWith("# "))
            {
                font = new Font(Font.FontFamily, Font.Size + 8, FontStyle.Bold);
                return textSegment.Substring(2);
            }
            if (textSegment.StartsWith("***") && textSegment.EndsWith("***"))
            {
                font = new Font(Font, FontStyle.Bold | FontStyle.Italic);
                return textSegment.Substring(3, textSegment.Length - 6);
            }
            if (textSegment.StartsWith("**") && textSegment.EndsWith("**"))
            {
                font = new Font(Font, FontStyle.Bold);
                return textSegment.Substring(2, textSegment.Length - 4);
            }
            if (textSegment.StartsWith("*") && textSegment.EndsWith("*"))
            {
                font = new Font(Font, FontStyle.Italic);
                return textSegment.Substring(1, textSegment.Length - 2);
            }
            if (textSegment.StartsWith("__") && textSegment.EndsWith("__"))
            {
                font = new Font(Font, FontStyle.Underline);
                return textSegment.Substring(2, textSegment.Length - 4);
            }
            if (textSegment.StartsWith("~~") && textSegment.EndsWith("~~"))
            {
                font = new Font(Font, FontStyle.Strikeout);
                return textSegment.Substring(2, textSegment.Length - 4);
            }

            return textSegment;
        }

        private void RenderContent(Graphics graphics, (string Text, Font FontType, Image Image, bool IsNewLine, bool IsLink)[] parsedContent)
        {
            StringFormat stringFormat = new StringFormat { FormatFlags = StringFormatFlags.NoWrap };
            Color linkColor = Color.FromArgb(0, 102, 204);
            int lineHeight = 0, contentWidth = 0;
            bool isFirstWord = true;
            float x = 0, y = 0;

            foreach (var (text, font, image, isNewLine, isLink) in parsedContent)
            {
                if (isNewLine)
                {
                    y += lineHeight; x = 0;
                    isFirstWord = true;
                    lineHeight = 0;
                    continue;
                }

                if (image != null)
                {
                    int maxWidth = 500;
                    int newWidth = image.Width;
                    int newHeight = image.Height;

                    if (newWidth > maxWidth)
                    {
                        float scaleFactor = (float)maxWidth / newWidth;
                        newWidth = maxWidth;
                        newHeight = (int)(image.Height * scaleFactor);
                    }

                    graphics.DrawImage(image, (int)x, (int)y, newWidth, newHeight);
                    x += newWidth + 4;
                    lineHeight = Math.Max(lineHeight, newHeight);
                }
                else if (!string.IsNullOrEmpty(text))
                {
                    string textToDraw = isFirstWord ? text.TrimStart() : text;
                    Brush textBrush = isLink ? new SolidBrush(linkColor) : new SolidBrush(ForeColor);
                    SizeF textSize = graphics.MeasureString(textToDraw, font, new PointF(x, y), stringFormat);
                    graphics.DrawString(textToDraw, font, textBrush, new PointF(x, y), stringFormat);

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
                    if (!Properties.Settings.Default.gifwarning)
                    {
                        new CMessageBox("A heads up",
                            "Naticord doesn't support GIFs right now. We are sorry for the inconvenience. " +
                            "This will be added in a later release. This message won't show again.")
                            .Show();

                        Properties.Settings.Default.gifwarning = true;
                        Properties.Settings.Default.Save();
                    }
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
    }
}