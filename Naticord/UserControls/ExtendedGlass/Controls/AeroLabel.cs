using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Naticord.Classes.ExtendedGlass.WindowsFormsAero;

namespace Naticord.UserControls
{
    [DefaultProperty("Text")]
    public class AeroLabel : Control
    {
        private ThemedText _themeText;
        private Control _lastParent;
        private Control _lastGrandparent;
        private readonly List<Control> _subscribedSiblings = new List<Control>();

        private bool _compositionDisabled;
        private bool _restoringBackground;
        private bool _fixClippingBug;

        [
            Description("Skips drawing overflow on transparent parents to fix double glow issues in certain containers."),
            Category("Appearance"),
            DefaultValue(false)
        ]
        public bool FixClippingBug
        {
            get => _fixClippingBug;
            set { _fixClippingBug = value; Invalidate(); }
        }

        [
            Description("Disables composition and forces text color on non-transparent backgrounds."),
            Category("Appearance"),
            DefaultValue(false)
        ]
        public bool CompositionDisabled
        {
            get => _compositionDisabled;
            set { _compositionDisabled = value; Invalidate(); }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _themeText = new ThemedText();
            SyncThemeText();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            UnsubscribeAll();
            _themeText?.Dispose();
            _themeText = null;
            base.OnHandleDestroyed(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            RebindSubscriptions();
            base.OnParentChanged(e);
        }

        public void RebindPaintSubscriptions() => RebindSubscriptions();

        private void RebindSubscriptions()
        {
            UnsubscribeAll();

            _lastParent = Parent;
            if (Parent == null) return;

            Parent.Paint += OnParentPaint;
            Parent.ControlAdded += OnSiblingsChanged;
            Parent.ControlRemoved += OnSiblingsChanged;
            Parent.ParentChanged += OnParentParentChanged;

            _lastGrandparent = Parent.Parent;
            if (_lastGrandparent != null)
                _lastGrandparent.Paint += OnGrandparentPaint;

            SubscribeSiblings();
        }

        private void UnsubscribeAll()
        {
            foreach (var s in _subscribedSiblings)
                s.Paint -= OnSiblingPaint;
            _subscribedSiblings.Clear();

            if (_lastGrandparent != null)
            {
                _lastGrandparent.Paint -= OnGrandparentPaint;
                _lastGrandparent = null;
            }

            if (_lastParent != null)
            {
                _lastParent.Paint -= OnParentPaint;
                _lastParent.ControlAdded -= OnSiblingsChanged;
                _lastParent.ControlRemoved -= OnSiblingsChanged;
                _lastParent.ParentChanged -= OnParentParentChanged;
                _lastParent = null;
            }
        }

        private void SubscribeSiblings()
        {
            if (Parent == null) return;
            foreach (Control sibling in Parent.Controls)
            {
                if (sibling == this) continue;
                sibling.Paint += OnSiblingPaint;
                _subscribedSiblings.Add(sibling);
            }
        }

        private void OnSiblingsChanged(object sender, ControlEventArgs e)
        {
            foreach (var s in _subscribedSiblings)
                s.Paint -= OnSiblingPaint;
            _subscribedSiblings.Clear();
            SubscribeSiblings();
        }

        private void OnParentParentChanged(object sender, EventArgs e)
        {
            if (_lastGrandparent != null)
                _lastGrandparent.Paint -= OnGrandparentPaint;
            _lastGrandparent = Parent?.Parent;
            if (_lastGrandparent != null)
                _lastGrandparent.Paint += OnGrandparentPaint;
        }

        private bool GlowDrawable => Visible && _glowEnabled && !_compositionDisabled && _themeText != null;

        private bool IsFullyTransparent(Control c)
        {
            return c.BackColor.A == 0 && c.BackgroundImage == null;
        }

        private void OnParentPaint(object sender, PaintEventArgs e)
        {
            if (GlowDrawable && !_restoringBackground)
            {
                if (_fixClippingBug && Parent.Parent != null && IsFullyTransparent(Parent)) return;
                _themeText.DrawOverflow(e.Graphics, Left, Top, Width, Height);
            }
        }

        private void OnGrandparentPaint(object sender, PaintEventArgs e)
        {
            if (GlowDrawable && Parent != null && !_restoringBackground)
            {
                _themeText.BlendOverflow(e.Graphics, Parent.Left + Left, Parent.Top + Top, Width, Height);
            }
        }

        private void OnSiblingPaint(object sender, PaintEventArgs e)
        {
            if (!GlowDrawable || _restoringBackground) return;
            var sibling = (Control)sender;
            _themeText.BlendOverflow(e.Graphics, Left - sibling.Left, Top - sibling.Top, Width, Height);
        }

        public override string Text
        {
            get => base.Text;
            set { base.Text = value; SyncThemeText(); }
        }

        public override Font Font
        {
            get => base.Font;
            set { base.Font = value; SyncThemeText(); }
        }

        public new Padding Padding
        {
            get => base.Padding;
            set { base.Padding = value; SyncThemeText(); }
        }

        public override Color ForeColor
        {
            get => base.ForeColor;
            set { base.ForeColor = value; SyncThemeText(); }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get => base.BackColor;
            set { }
        }

        [Browsable(false)]
        public new Image BackgroundImage
        {
            get => base.BackgroundImage;
            set { }
        }

        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout
        {
            get => base.BackgroundImageLayout;
            set { }
        }

        private int _glowSize = ThemedText.DefaultGlowSize;

        [
            Description("Size of the glow effect around the text."),
            Category("Appearance"),
            DefaultValue(ThemedText.DefaultGlowSize)
        ]
        public int GlowSize
        {
            get => _glowSize;
            set { _glowSize = value; SyncThemeText(); }
        }

        private bool _glowEnabled = true;

        [
            Description("Enables or disables the glow effect around the text."),
            Category("Appearance"),
            DefaultValue(true)
        ]
        public bool GlowEnabled
        {
            get => _glowEnabled;
            set { _glowEnabled = value; SyncThemeText(); }
        }

        private HorizontalAlignment _horizontal = HorizontalAlignment.Left;

        [
            Description("Horizontal text alignment."),
            Category("Appearance"),
            DefaultValue(typeof(HorizontalAlignment), "Left")
        ]
        public HorizontalAlignment TextAlign
        {
            get => _horizontal;
            set { _horizontal = value; SyncThemeText(); }
        }

        private VerticalAlignment _vertical = VerticalAlignment.Top;

        [
            Description("Vertical text alignment."),
            Category("Appearance"),
            DefaultValue(typeof(VerticalAlignment), "Top")
        ]
        public VerticalAlignment TextAlignVertical
        {
            get => _vertical;
            set { _vertical = value; SyncThemeText(); }
        }

        private bool _singleLine = true;

        [
            Description("Lay out text on a single line."),
            Category("Appearance"),
            DefaultValue(true)
        ]
        public bool SingleLine
        {
            get => _singleLine;
            set { _singleLine = value; SyncThemeText(); }
        }

        private bool _endEllipsis;

        [
            Description("Trim overflowing text with an ellipsis."),
            Category("Appearance"),
            DefaultValue(false)
        ]
        public bool EndEllipsis
        {
            get => _endEllipsis;
            set { _endEllipsis = value; SyncThemeText(); }
        }

        private bool _wordBreak;

        [
            Description("Break text only at word boundaries."),
            Category("Appearance"),
            DefaultValue(false)
        ]
        public bool WordBreak
        {
            get => _wordBreak;
            set { _wordBreak = value; SyncThemeText(); }
        }

        private bool _wordEllipsis;

        [
            Description("Trim to the nearest word and append an ellipsis."),
            Category("Appearance"),
            DefaultValue(false)
        ]
        public bool WordEllipsis
        {
            get => _wordEllipsis;
            set { _wordEllipsis = value; SyncThemeText(); }
        }

        private void SyncThemeText()
        {
            if (_themeText == null) return;

            _themeText.Text = Text;
            _themeText.Font = Font;
            _themeText.Padding = Padding;
            _themeText.Color = ForeColor;
            _themeText.FormatFlags = BuildFormatFlags();
            _themeText.GlowSize = _glowSize;
            _themeText.GlowEnabled = _glowEnabled;

            Invalidate();
        }

        private TextFormatFlags BuildFormatFlags()
        {
            var flags = TextFormatFlags.Default;

            switch (_horizontal)
            {
                case HorizontalAlignment.Left: flags |= TextFormatFlags.Left; break;
                case HorizontalAlignment.Center: flags |= TextFormatFlags.HorizontalCenter; break;
                case HorizontalAlignment.Right: flags |= TextFormatFlags.Right; break;
            }

            switch (_vertical)
            {
                case VerticalAlignment.Top: flags |= TextFormatFlags.Top; break;
                case VerticalAlignment.Center: flags |= TextFormatFlags.VerticalCenter; break;
                case VerticalAlignment.Bottom: flags |= TextFormatFlags.Bottom; break;
            }

            if (_singleLine) flags |= TextFormatFlags.SingleLine;
            if (_endEllipsis) flags |= TextFormatFlags.EndEllipsis;
            if (_wordBreak) flags |= TextFormatFlags.WordBreak;
            if (_wordEllipsis) flags |= TextFormatFlags.WordEllipsis;

            if (RightToLeft == RightToLeft.Yes)
                flags |= TextFormatFlags.RightToLeft;

            return flags;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);
            if (Parent == null) return;

            int inflate = _glowEnabled ? _glowSize : 0;
            var inflatedInParent = new Rectangle(
                Left - inflate, Top - inflate,
                Width + 2 * inflate, Height + 2 * inflate);

            Parent.Invalidate(inflatedInParent, false);

            if (Parent.Parent != null)
            {
                Parent.Parent.Invalidate(new Rectangle(
                    Parent.Left + Left - inflate, Parent.Top + Top - inflate,
                    Width + 2 * inflate, Height + 2 * inflate), false);
            }

            foreach (Control sibling in Parent.Controls)
            {
                if (sibling == this) continue;
                var siblingBounds = new Rectangle(sibling.Left, sibling.Top, sibling.Width, sibling.Height);
                var overlap = Rectangle.Intersect(inflatedInParent, siblingBounds);
                if (overlap.IsEmpty) continue;

                sibling.Invalidate(new Rectangle(
                    overlap.X - sibling.Left, overlap.Y - sibling.Top,
                    overlap.Width, overlap.Height), false);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent == null || DesignMode) return;

            _restoringBackground = true;
            var state = e.Graphics.Save();
            try
            {
                e.Graphics.TranslateTransform(-Left, -Top);
                using (var pe = new PaintEventArgs(e.Graphics, new Rectangle(Left, Top, Width, Height)))
                    InvokePaintBackground(Parent, pe);
            }
            finally
            {
                e.Graphics.Restore(state);
                _restoringBackground = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode || (_compositionDisabled && BackColor.A != 0))
            {
                using (var sf = BuildStringFormat())
                    e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor),
                        new RectangleF(Padding.Left, Padding.Top,
                            Width - Padding.Horizontal, Height - Padding.Vertical), sf);
                return;
            }

            if (!Visible) return;

            if (_glowEnabled)
                _themeText.DrawInBounds(e.Graphics, Width, Height);
            else
                _themeText.Draw(e.Graphics, 0, 0, Width, Height);
        }

        private StringFormat BuildStringFormat()
        {
            var sf = new StringFormat();

            switch (_horizontal)
            {
                case HorizontalAlignment.Left: sf.Alignment = StringAlignment.Near; break;
                case HorizontalAlignment.Center: sf.Alignment = StringAlignment.Center; break;
                case HorizontalAlignment.Right: sf.Alignment = StringAlignment.Far; break;
            }

            switch (_vertical)
            {
                case VerticalAlignment.Top: sf.LineAlignment = StringAlignment.Near; break;
                case VerticalAlignment.Center: sf.LineAlignment = StringAlignment.Center; break;
                case VerticalAlignment.Bottom: sf.LineAlignment = StringAlignment.Far; break;
            }

            return sf;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == (int)WindowMessage.WM_NCHITTEST && !DesignMode)
                m.Result = (IntPtr)HitTest.HTTRANSPARENT;
        }
    }
}