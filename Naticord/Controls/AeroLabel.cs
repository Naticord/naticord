// Taken from WindowsFormsAero

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Naticord.Classes;

namespace Naticord.Controls
{
    [DefaultProperty("Text")]
    public class AeroLabel : Control
    {
        private ThemedText _themeText;

        private bool _compositionDisabled = false;

        // CompositionDisabled property
        /// <summary>
        /// Disables composition and forces text color to white on non-transparent backgrounds.
        /// </summary>
        [
            Description("Disables composition and forces text color to white on non-transparent backgrounds."),
            Category("Appearance"),
            DefaultValue(false)
        ]
        public bool CompositionDisabled
        {
            get { return _compositionDisabled; }
            set
            {
                _compositionDisabled = value;
                Invalidate();
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            _themeText = new ThemedText();
            UpdateText();

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_themeText != null)
            {
                _themeText.Dispose();
            }
            _themeText = null;

            base.OnHandleDestroyed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Invalidate(false);
        }

        private void UpdateText()
        {
            if (_themeText != null)
            {
                _themeText.Text = Text;
                _themeText.Font = Font;
                _themeText.Padding = Padding;
                _themeText.Color = ForeColor;
                _themeText.FormatFlags = CreateFormatFlags();
            }

            Invalidate(false);
        }

        #region Control properties overriding

        public override string Text
        {
            set
            {
                base.Text = value;
                UpdateText();
            }
        }

        public override Font Font
        {
            set
            {
                base.Font = value;
                UpdateText();
            }
        }

        public new Padding Padding
        {
            get { return base.Padding; }
            set
            {
                base.Padding = value;
                UpdateText();
            }
        }

        public override Color ForeColor
        {
            set
            {
                base.ForeColor = value;
                UpdateText();
            }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { }
        }

        [Browsable(false)]
        public new Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { }
        }

        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { }
        }

        #endregion Control properties overriding

        #region Glow properties

        private int _glowSize = ThemedText.DefaultGlowSize;

        /// <summary>
        /// Size of the glow effect around the text.
        /// </summary>
        [
        Description("Size of the glow effect around the text."),
        Category("Appearance"),
        DefaultValue(ThemedText.DefaultGlowSize)
        ]
        public int GlowSize
        {
            get
            {
                return _glowSize;
            }
            set
            {
                _glowSize = value;
                UpdateText();
            }
        }

        private bool _glowEnabled = true;

        /// <summary>
        /// Enables or disables the glow effect around the text.
        /// </summary>
        [
        Description("Enables or disables the glow effect around the text."),
        Category("Appearance"),
        DefaultValue(true)
        ]
        public bool GlowEnabled
        {
            get
            {
                return _glowEnabled;
            }
            set
            {
                _glowEnabled = value;
                UpdateText();
            }
        }

        #endregion Glow properties

        #region Text format properties

        private HorizontalAlignment _horizontal = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the horizontal text alignment setting.
        /// </summary>
        [
        Description("Horizontal text alignment."),
        Category("Appearance"),
        DefaultValue(typeof(HorizontalAlignment), "Left")
        ]
        public HorizontalAlignment TextAlign
        {
            get { return _horizontal; }
            set
            {
                _horizontal = value;
                UpdateText();
            }
        }

        private VerticalAlignment _vertical = VerticalAlignment.Top;

        /// <summary>
        /// Gets or sets the vertical text alignment setting.
        /// </summary>
        [
        Description("Vertical text alignment."),
        Category("Appearance"),
        DefaultValue(typeof(VerticalAlignment), "Top")
        ]
        public VerticalAlignment TextAlignVertical
        {
            get { return _vertical; }
            set
            {
                _vertical = value;
                UpdateText();
            }
        }

        private bool _singleLine = true;

        /// <summary>
        /// Gets or sets whether the text will be laid out on a single line or on
        /// multiple lines.
        /// </summary>
        [
        Description("Single line text."),
        Category("Appearance"),
        DefaultValue(true)
        ]
        public bool SingleLine
        {
            get { return _singleLine; }
            set
            {
                _singleLine = value;
                UpdateText();
            }
        }

        private bool _endEllipsis = false;

        /// <summary>
        /// Gets or sets whether the text lines over the label's border should
        /// be trimmed with an ellipsis.
        /// </summary>
        [
        Description("Removes the end of trimmed lines and replaces them with an ellipsis."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool EndEllipsis
        {
            get { return _endEllipsis; }
            set
            {
                _endEllipsis = value;
                UpdateText();
            }
        }

        private bool _wordBreak = false;

        /// <summary>
        /// Gets or sets whether the text should break only at the end of a word.
        /// </summary>
        [
        Description("Break the text at the end of a word."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool WordBreak
        {
            get { return _wordBreak; }
            set
            {
                _wordBreak = value;
                UpdateText();
            }
        }

        private bool _wordEllipsis = false;

        /// <summary>
        /// Gets or sets whether the text should be trimmed to the last word
        /// and an ellipse should be placed at the end of the line.
        /// </summary>
        [
        Description("Trims the line to the nearest word and an ellipsis is placed at the end of a trimmed line."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool WordEllipsis
        {
            get { return _wordBreak; }
            set
            {
                _wordBreak = value;
                UpdateText();
            }
        }

        #endregion Text format properties

        #region Events

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowMessage.WM_NCHITTEST && !DesignMode)
            {
                base.WndProc(ref m);

                m.Result = (IntPtr)HitTest.HTTRANSPARENT;

                return;
            }

            base.WndProc(ref m);
        }

        #endregion Events

        #region Drawing

        protected TextFormatFlags CreateFormatFlags()
        {
            TextFormatFlags ret = TextFormatFlags.Default;

            switch (_horizontal)
            {
                case HorizontalAlignment.Left:
                    ret |= TextFormatFlags.Left; break;
                case HorizontalAlignment.Center:
                    ret |= TextFormatFlags.HorizontalCenter; break;
                case HorizontalAlignment.Right:
                    ret |= TextFormatFlags.Right; break;
            }

            switch (_vertical)
            {
                case VerticalAlignment.Top:
                    ret |= TextFormatFlags.Top; break;
                case VerticalAlignment.Center:
                    ret |= TextFormatFlags.VerticalCenter; break;
                case VerticalAlignment.Bottom:
                    ret |= TextFormatFlags.Bottom; break;
            }

            if (_singleLine)
                ret |= TextFormatFlags.SingleLine;

            if (_endEllipsis)
                ret |= TextFormatFlags.EndEllipsis;

            if (_wordBreak)
                ret |= TextFormatFlags.WordBreak;
            if (_wordEllipsis)
                ret |= TextFormatFlags.WordEllipsis;

            if (RightToLeft == RightToLeft.Yes)
                ret |= TextFormatFlags.RightToLeft;

            return ret;
        }

        private StringFormat CreateStringFormat()
        {
            var sf = new StringFormat();

            switch (_horizontal)
            {
                case HorizontalAlignment.Left:
                    sf.Alignment = StringAlignment.Near; break;
                case HorizontalAlignment.Center:
                    sf.Alignment = StringAlignment.Center; break;
                case HorizontalAlignment.Right:
                    sf.Alignment = StringAlignment.Far; break;
            }

            switch (_vertical)
            {
                case VerticalAlignment.Top:
                    sf.LineAlignment = StringAlignment.Near; break;
                case VerticalAlignment.Center:
                    sf.LineAlignment = StringAlignment.Center; break;
                case VerticalAlignment.Bottom:
                    sf.LineAlignment = StringAlignment.Far; break;
            }

            return sf;
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            //Invalidate parent because of transparency
            if (Parent != null)
                Parent.Invalidate(ClientRectangle, false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode)
            {
                e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor),
                    new RectangleF(Padding.Left, Padding.Top, Size.Width - Padding.Horizontal, Size.Height - Padding.Vertical),
                    CreateStringFormat());
                return;
            }

            if (_compositionDisabled && !BackColor.A.Equals(0))
            {
                e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor),
                    new RectangleF(Padding.Left, Padding.Top, Size.Width - Padding.Horizontal, Size.Height - Padding.Vertical),
                    CreateStringFormat());
                return;
            }

            if (Visible)
            {
                _themeText.Draw(e.Graphics, 0, 0, Size.Width, Size.Height);
            }
        }

        #endregion Drawing
    }
}