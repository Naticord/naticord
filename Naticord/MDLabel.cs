using System;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;

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
            Graphics g = e.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            if (string.IsNullOrEmpty(Text)) return;

            var parsedContent = await ParseMarkdownAsync(Text);
            RenderContent(g, parsedContent);
        }

        private async Task<(string Text, Font FontType, Image Image)[]> ParseMarkdownAsync(string text)
        {
            var parts = Regex.Split(text, @"(\*\*.*?\*\*|\*.*?\*|!\[.*?\]\(.*?\))");
            var result = new (string, Font, Image)[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                Font font = Font;
                Image image = null;

                if (part.StartsWith("**") && part.EndsWith("**"))
                {
                    font = new Font(Font, FontStyle.Bold);
                    part = part.Substring(2, part.Length - 4);
                }
                else if (part.StartsWith("*") && part.EndsWith("*"))
                {
                    font = new Font(Font, FontStyle.Italic);
                    part = part.Substring(1, part.Length - 2);
                }
                else if (part.StartsWith("![") && part.EndsWith(")"))
                {
                    var imageUrl = Regex.Match(part, @"\!\[.*?\]\((.*?)\)").Groups[1].Value;
                    image = await LoadImageAsync(imageUrl);
                    part = "";
                }

                result[i] = (part, font, image);
            }

            return result;
        }

        private async Task<Image> LoadImageAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        return Image.FromStream(ms);
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        private void RenderContent(Graphics g, (string Text, Font FontType, Image Image)[] parsedContent)
        {
            float x = 0;
            float y = 0;
            int lineHeight = 0;
            int contentWidth = 0;

            foreach (var (partText, font, image) in parsedContent)
            {
                if (image != null)
                {
                    g.DrawImage(image, (int)x, (int)y, image.Width, image.Height);
                    x += image.Width + 5;
                    lineHeight = Math.Max(lineHeight, image.Height);
                }
                else if (!string.IsNullOrEmpty(partText))
                {
                    Size textSize = TextRenderer.MeasureText(partText, font, new Size(int.MaxValue, int.MaxValue),
                        TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                    TextRenderer.DrawText(g, partText, font, new Point((int)x, (int)y), ForeColor,
                        TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);
                    x += textSize.Width - 7;
                    lineHeight = Math.Max(lineHeight, textSize.Height);
                }
            }

            contentWidth = (int)x;

            y += lineHeight + 5;
            x = 0;

            if (MDImage != null)
            {
                g.DrawImage(MDImage, (int)x, (int)y, MDImage.Width, MDImage.Height);
                contentWidth = Math.Max(contentWidth, MDImage.Width);
                y += MDImage.Height;
            }

            Width = contentWidth;
            Height = (int)y;
        }
    }
}