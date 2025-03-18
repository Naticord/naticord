using System;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Naticord
{
    public partial class MDLabel : Label
    {
        public MDLabel()
        {
            InitializeComponent();
            AutoSize = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            if (string.IsNullOrEmpty(Text))
                return;

            var parsedText = ParseMarkdown(Text);
            RenderText(g, parsedText);
        }

        private (string Text, Font FontType)[] ParseMarkdown(string text)
        {
            var parts = Regex.Split(text, @"(\*\*.*?\*\*|\*.*?\*)");
            var result = new (string, Font)[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                Font font = this.Font;

                if (part.StartsWith("**") && part.EndsWith("**"))
                {
                    font = new Font(this.Font, FontStyle.Bold);
                    part = part.Substring(2, part.Length - 4);
                }
                else if (part.StartsWith("*") && part.EndsWith("*"))
                {
                    font = new Font(this.Font, FontStyle.Italic);
                    part = part.Substring(1, part.Length - 2);
                }

                result[i] = (part, font);
            }

            return result;
        }

        private void RenderText(Graphics g, (string Text, Font FontType)[] parsedText)
        {
            float x = 0;
            float y = 0;
            int totalWidth = 0;
            int maxHeight = 0;

            foreach (var (partText, font) in parsedText)
            {
                Size textSize = TextRenderer.MeasureText(partText, font, new Size(int.MaxValue, int.MaxValue),
                    TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                TextRenderer.DrawText(g, partText, font, new Point((int)x, (int)y), ForeColor,
                    TextFormatFlags.NoPadding | TextFormatFlags.NoClipping);

                x += textSize.Width - 7;
                totalWidth += textSize.Width - 7;
                maxHeight = Math.Max(maxHeight, textSize.Height);
            }

            this.Width = totalWidth;
            this.Height = maxHeight;
        }
    }
}