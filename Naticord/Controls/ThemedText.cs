// Taken from WindowsFormsAero

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Naticord.Classes;

namespace Naticord.Controls
{
    internal class ThemedText : IDisposable
    {

        private static int _win32Black = ColorTranslator.ToWin32(Color.Black);

        private static VisualStyleRenderer GetRenderer()
        {
            if (VisualStyleRenderer.IsSupported &&
                VisualStyleRenderer.IsElementDefined(VisualStyleElement.Window.Caption.Active))
            {
                return new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
            }

            return null;
        }
        public ThemedText()
        {

        }

        private bool _invalidated = true;

        private string _text = string.Empty;

        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                    _invalidated = true;
                _text = value;
            }
        }

        private Font _font = SystemFonts.CaptionFont;

        public Font Font
        {
            get { return _font; }
            set
            {
                if (_font != value)
                    _invalidated = true;
                _font = value;
            }
        }

        private Padding _padding = Padding.Empty;

        public Padding Padding
        {
            get { return _padding; }
            set
            {
                if (_padding != value)
                    _invalidated = true;
                _padding = value;
            }
        }

        private int _win32Color = ColorTranslator.ToWin32(Color.Black);

        public Color Color
        {
            get { return ColorTranslator.FromWin32(_win32Black); }
            set
            {
                _invalidated = true;
                _win32Color = ColorTranslator.ToWin32(value);
            }
        }

        private TextFormatFlags _formatFlags = TextFormatFlags.Default;

        public TextFormatFlags FormatFlags
        {
            get { return _formatFlags; }
            set
            {
                if (_formatFlags != value)
                    _invalidated = true;
                _formatFlags = value;
            }
        }

        /// <summary>
        /// Default glow size.
        /// </summary>
        public const int DefaultGlowSize = 10;

        /// <summary>
        /// Glow size used commonly by Office 2007 in titles.
        /// </summary>
        public const int Word2007GlowSize = 15;

        private int _glowSize = DefaultGlowSize;

        public int GlowSize
        {
            get
            {
                return _glowSize;
            }
            set
            {
                if (_glowSize != value)
                    _invalidated = true;
                _glowSize = value;
            }
        }

        private bool _glowEnabled = true;

        public bool GlowEnabled
        {
            get
            {
                return _glowEnabled;
            }
            set
            {
                if (_glowEnabled != value)
                    _invalidated = true;
                _glowEnabled = value;
            }
        }

        #region IDisposable Members

        ~ThemedText()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_textHdc != IntPtr.Zero)
            {
                Methods.DeleteDC(_textHdc);
                _textHdc = IntPtr.Zero;
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        public void Draw(Graphics g, System.Drawing.Point location, System.Drawing.Size size)
        {
            Draw(g, location.X, location.Y, size.Width, size.Height);
        }

        public void Draw(Graphics g, Rectangle rect)
        {
            Draw(g, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void Draw(Graphics g, int x, int y, int width, int height)
        {
            if (VisualStyleRenderer.IsSupported)
            {
                var outputHdc = g.GetHdc();

                var sourceHdc = PrepareHdc(outputHdc, width, height);

                Methods.BitBlt(outputHdc,
                    x, y, width, height,
                    sourceHdc, 0, 0,
                    BitBltOp.SRCCOPY);

                g.ReleaseHdc(outputHdc);
            }
            else
            {
                Rectangle paddedBounds = new Rectangle(
                    x + _padding.Left,
                    y + _padding.Top,
                    width - _padding.Horizontal,
                    height - _padding.Vertical
                );

                TextRenderer.DrawText(
                    g,
                    _text,
                    _font,
                    paddedBounds,
                    ColorTranslator.FromWin32(_win32Color),
                    _formatFlags
                );
            }

            System.Diagnostics.Debug.WriteLine("ThemedText.Draw");
        }

        private IntPtr _textHdc = IntPtr.Zero;
        private IntPtr _dibSectionRef;
        private int _lastHdcWidth = -1;
        private int _lastHdcHeight = -1;

        /// <summary>
        /// Ensures that a valid source HDC exists and has been rendered to.
        /// </summary>
        private IntPtr PrepareHdc(IntPtr outputHdc, int width, int height)
        {
            if (width == _lastHdcWidth && height == _lastHdcHeight && !_invalidated)
                return _textHdc;

            _lastHdcWidth = width;
            _lastHdcHeight = height;

            if (_textHdc != IntPtr.Zero)
            {
                Methods.DeleteObject(_dibSectionRef);
                Methods.DeleteDC(_textHdc);
            }
            _textHdc = Methods.CreateCompatibleDC(outputHdc);

            // Create a DIB-Bitmap on which to draw
            var info = new BitmapInfo()
            {
                biSize = Marshal.SizeOf(typeof(BitmapInfo)),
                biWidth = width,
                biHeight = -height, // DIB use top-down ref system, thus we set negative height
                biPlanes = 1,
                biBitCount = 32,
                biCompression = 0
            };
            _dibSectionRef = Methods.CreateDIBSection(outputHdc, ref info, 0, 0, IntPtr.Zero, 0);
            Methods.SelectObject(_textHdc, _dibSectionRef);

            // Create the Font to use
            IntPtr hFont = Font.ToHfont();
            Methods.SelectObject(_textHdc, hFont);

            // Prepare options
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

            // Set full bounds with padding
            Rect paddedBounds = new Rect(
                _padding.Left, _padding.Top,
                width - _padding.Right, height - _padding.Bottom
            );

            // Draw
            var renderer = GetRenderer();
            if (renderer != null)
            {
                int ret = Methods.DrawThemeTextEx(renderer.Handle, _textHdc, 0, 0,
                    _text, -1,
                    (int)_formatFlags, ref paddedBounds, ref dttOpts);
                if (ret != 0)
                    Marshal.ThrowExceptionForHR(ret);
            }
            else
            {
                using (Graphics g = Graphics.FromHdc(_textHdc))
                {
                    TextRenderer.DrawText(
                        g,
                        _text,
                        _font,
                        Rectangle.FromLTRB(paddedBounds.Left, paddedBounds.Top, paddedBounds.Right, paddedBounds.Bottom),
                        ColorTranslator.FromWin32(_win32Color),
                        _formatFlags
                    );
                }
            }

            // Clean up
            Methods.DeleteObject(hFont);

            return _textHdc;
        }

    }

}