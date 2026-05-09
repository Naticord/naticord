using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Naticord.Classes.ExtendedGlass.WindowsFormsAero;

namespace Naticord.UserControls
{
    internal sealed class ThemedText : IDisposable
    {
        public const int DefaultGlowSize = 10;
        private static VisualStyleRenderer _renderer;

        private static VisualStyleRenderer GetRenderer()
        {
            if (_renderer != null) return _renderer;

            if (VisualStyleRenderer.IsSupported &&
                VisualStyleRenderer.IsElementDefined(VisualStyleElement.Window.Caption.Active))
            {
                _renderer = new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
            }

            return _renderer;
        }

        private bool _invalidated = true;

        private string _text = string.Empty;
        private Font _font = SystemFonts.CaptionFont;
        private TextFormatFlags _formatFlags = TextFormatFlags.Default;

        private Padding _padding = Padding.Empty;

        private int _win32Color = ColorTranslator.ToWin32(Color.Black);

        private int _glowSize = DefaultGlowSize;
        private bool _glowEnabled = true;

        private IntPtr _textHdc = IntPtr.Zero;
        private IntPtr _dibSection = IntPtr.Zero;

        private int _cachedWidth = -1;
        private int _cachedHeight = -1;

        public string Text
        {
            get => _text;
            set { if (_text != value) { _text = value; _invalidated = true; } }
        }

        public Font Font
        {
            get => _font;
            set { if (_font != value) { _font = value; _invalidated = true; } }
        }

        public Padding Padding
        {
            get => _padding;
            set { if (_padding != value) { _padding = value; _invalidated = true; } }
        }

        public Color Color
        {
            get => ColorTranslator.FromWin32(_win32Color);
            set { _win32Color = ColorTranslator.ToWin32(value); _invalidated = true; }
        }

        public TextFormatFlags FormatFlags
        {
            get => _formatFlags;
            set { if (_formatFlags != value) { _formatFlags = value; _invalidated = true; } }
        }

        public int GlowSize
        {
            get => _glowSize;
            set { if (_glowSize != value) { _glowSize = value; _invalidated = true; } }
        }

        public bool GlowEnabled
        {
            get => _glowEnabled;
            set { if (_glowEnabled != value) { _glowEnabled = value; _invalidated = true; } }
        }

        public void Dispose()
        {
            FreeDib();
            GC.SuppressFinalize(this);
        }

        ~ThemedText() => FreeDib();

        private void FreeDib()
        {
            if (_dibSection != IntPtr.Zero)
            {
                Methods.DeleteObject(_dibSection);
                _dibSection = IntPtr.Zero;
            }
            if (_textHdc != IntPtr.Zero)
            {
                Methods.DeleteDC(_textHdc);
                _textHdc = IntPtr.Zero;
            }
        }

        private static readonly Methods.BlendFunction PremultipliedBlend = new Methods.BlendFunction
        {
            BlendOp = Methods.AC_SRC_OVER,
            BlendFlags = 0,
            SourceConstantAlpha = 0xFF,
            AlphaFormat = Methods.AC_SRC_ALPHA
        };

        public void Draw(Graphics g, System.Drawing.Point location, Size size) =>
            Draw(g, location.X, location.Y, size.Width, size.Height);

        public void Draw(Graphics g, Rectangle rect) =>
            Draw(g, rect.X, rect.Y, rect.Width, rect.Height);

        public void Draw(Graphics g, int x, int y, int width, int height)
        {
            if (VisualStyleRenderer.IsSupported)
            {
                var hdc = g.GetHdc();
                try
                {
                    var src = EnsureHdc(hdc, width, height, offsetForGlow: 0);
                    Methods.AlphaBlend(hdc, x, y, width, height, src, 0, 0, width, height, PremultipliedBlend);
                }
                finally { g.ReleaseHdc(hdc); }
            }
            else
            {
                var bounds = new Rectangle(
                    x + _padding.Left, y + _padding.Top,
                    width - _padding.Horizontal, height - _padding.Vertical);
                TextRenderer.DrawText(g, _text, _font, bounds, ColorTranslator.FromWin32(_win32Color), _formatFlags);
            }
        }

        public void DrawInBounds(Graphics g, int width, int height)
        {
            int inflate = _glowEnabled ? _glowSize : 0;

            if (!VisualStyleRenderer.IsSupported || inflate == 0)
            {
                Draw(g, 0, 0, width, height);
                return;
            }

            int dibW = width + 2 * inflate;
            int dibH = height + 2 * inflate;

            var hdc = g.GetHdc();
            try
            {
                var src = EnsureHdc(hdc, dibW, dibH, inflate);
                Methods.AlphaBlend(hdc, 0, 0, width, height, src, inflate, inflate, width, height, PremultipliedBlend);
            }
            finally { g.ReleaseHdc(hdc); }
        }

        public void DrawOverflow(Graphics parentG, int controlLeft, int controlTop, int width, int height)
        {
            int inflate = _glowEnabled ? _glowSize : 0;
            if (!VisualStyleRenderer.IsSupported || inflate == 0) return;

            int dibW = width + 2 * inflate;
            int dibH = height + 2 * inflate;

            var hdc = parentG.GetHdc();
            try
            {
                var src = EnsureHdc(hdc, dibW, dibH, inflate);
                Methods.AlphaBlend(hdc, controlLeft - inflate, controlTop - inflate,
                    dibW, dibH, src, 0, 0, dibW, dibH, PremultipliedBlend);
            }
            finally { parentG.ReleaseHdc(hdc); }
        }

        public void BlendOverflow(Graphics dstG, int dstX, int dstY, int width, int height)
        {
            int inflate = _glowEnabled ? _glowSize : 0;
            if (!VisualStyleRenderer.IsSupported || inflate == 0) return;

            int dibW = width + 2 * inflate;
            int dibH = height + 2 * inflate;

            var hdc = dstG.GetHdc();
            try
            {
                var src = EnsureHdc(hdc, dibW, dibH, inflate);
                Methods.AlphaBlend(hdc, dstX - inflate, dstY - inflate,
                    dibW, dibH, src, 0, 0, dibW, dibH, PremultipliedBlend);
            }
            finally { dstG.ReleaseHdc(hdc); }
        }

        private IntPtr EnsureHdc(IntPtr outputHdc, int width, int height, int offsetForGlow)
        {
            if (width == _cachedWidth && height == _cachedHeight && !_invalidated)
                return _textHdc;

            FreeDib();

            _cachedWidth = width;
            _cachedHeight = height;

            _textHdc = Methods.CreateCompatibleDC(outputHdc);

            var info = new BitmapInfo
            {
                biSize = Marshal.SizeOf(typeof(BitmapInfo)),
                biWidth = width,
                biHeight = -height,
                biPlanes = 1,
                biBitCount = 32,
                biCompression = 0
            };
            _dibSection = Methods.CreateDIBSection(outputHdc, ref info, 0, 0, IntPtr.Zero, 0);
            Methods.SelectObject(_textHdc, _dibSection);

            IntPtr hFont = _font.ToHfont();
            Methods.SelectObject(_textHdc, hFont);

            var dttOpts = new DttOpts
            {
                dwSize = Marshal.SizeOf(typeof(DttOpts)),
                dwFlags = DttOptsFlags.DTT_COMPOSITED | DttOptsFlags.DTT_TEXTCOLOR,
                crText = _win32Color
            };
            if (_glowEnabled)
            {
                dttOpts.dwFlags |= DttOptsFlags.DTT_GLOWSIZE;
                dttOpts.iGlowSize = _glowSize;
            }

            var paddedBounds = new Rect(
                _padding.Left + offsetForGlow,
                _padding.Top + offsetForGlow,
                width - _padding.Right - offsetForGlow,
                height - _padding.Bottom - offsetForGlow);

            var renderer = GetRenderer();
            if (renderer != null)
            {
                int hr = Methods.DrawThemeTextEx(renderer.Handle, _textHdc, 0, 0,
                    _text, -1, (int)_formatFlags, ref paddedBounds, ref dttOpts);
                if (hr != 0)
                    Marshal.ThrowExceptionForHR(hr);
            }
            else
            {
                using (var g = Graphics.FromHdc(_textHdc))
                {
                    TextRenderer.DrawText(g, _text, _font,
                        Rectangle.FromLTRB(paddedBounds.Left, paddedBounds.Top, paddedBounds.Right, paddedBounds.Bottom),
                        ColorTranslator.FromWin32(_win32Color), _formatFlags);
                }
            }

            Methods.DeleteObject(hFont);
            _invalidated = false;

            return _textHdc;
        }
    }
}