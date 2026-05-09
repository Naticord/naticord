using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Naticord.Classes.ExtendedGlass.WindowsFormsAero;

namespace Naticord.UserControls
{
    public partial class GlassExtendedButton : UserControl
    {
        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);
        private static readonly Cursor _handCursor = new Cursor(LoadCursor(IntPtr.Zero, 32649));

        private string _buttonLabel;
        private Image _buttonIcon;
        private bool _compositionDisabled;

        public event EventHandler ButtonClick;

        public GlassExtendedButton()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            InitializeComponent();

            // Initialize underscore variables
            _buttonLabel = "Button";
            _buttonIcon = null;
            _compositionDisabled = false;

            // Make the underscored buttonLabel variable to buttonLabel text
            buttonLabel.Text = _buttonLabel;

            this.Cursor = _handCursor;
            this.Click += AllParts_Click;
            foreach (Control ctrl in Controls)
            {
                ctrl.Cursor = _handCursor;
                ctrl.Click += AllParts_Click;
            }

            buttonIcon.Paint += (s, e) =>
            {
                if (buttonIcon.Image != null && !_compositionDisabled)
                    DrawImageOnGlass(e.Graphics, buttonIcon.Image, new Rectangle(0, 0, buttonIcon.Width, buttonIcon.Height));
            };
            buttonLabel.RebindPaintSubscriptions();
        }

        // This makes it so if you click anywhere on the button, it'll register.
        private void AllParts_Click(object sender, EventArgs e) { ButtonClick?.Invoke(this, e); }

        [Browsable(true)]
        [Category("Appearance")]
        public string ButtonLabel
        {
            get => _buttonLabel;
            set
            {
                if (_buttonLabel != value)
                {
                    _buttonLabel = value;
                    buttonLabel.Text = _buttonLabel;
                }
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        public Image ButtonIcon
        {
            get => _buttonIcon;
            set
            {
                if (_buttonIcon != value)
                {
                    _buttonIcon = value;
                    buttonIcon.Image = PremultiplyAlpha(_buttonIcon);
                }
            }
        }

        private bool _reversed = false;

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Reverses the order of the elements in the control.")]
        public bool ReverseOrder
        {
            get => _reversed;
            set
            {
                if (_reversed != value)
                {
                    _reversed = value;
                    LayoutControls();
                }
            }
        }

        private void LayoutControls()
        {
            const int gap = 5;
            if (_reversed)
            {
                buttonLabel.TextAlign = HorizontalAlignment.Right;
                buttonLabel.Location = new System.Drawing.Point(0, buttonLabel.Top);
                buttonIcon.Location = new System.Drawing.Point(buttonLabel.Right + gap, buttonIcon.Top);
            }
            else
            {
                buttonLabel.TextAlign = HorizontalAlignment.Left;
                buttonIcon.Location = new System.Drawing.Point(0, buttonIcon.Top);
                buttonLabel.Location = new System.Drawing.Point(buttonIcon.Right + gap, buttonLabel.Top);
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Disables composition on the button label text.")]
        public bool CompositionDisabled
        {
            get => _compositionDisabled;
            set
            {
                if (_compositionDisabled != value)
                {
                    _compositionDisabled = value;
                    buttonLabel.CompositionDisabled = _compositionDisabled;
                    buttonLabel.Invalidate();
                    buttonIcon.Invalidate();
                }
            }
        }

        private void DrawImageOnGlass(Graphics outputG, Image image, Rectangle blitBounds)
        {
            if (blitBounds.Width <= 0 || blitBounds.Height <= 0) return;

            var outputHdc = outputG.GetHdc();
            var memDc = Methods.CreateCompatibleDC(outputHdc);

            var bmi = new BitmapInfo
            {
                biSize = Marshal.SizeOf(typeof(BitmapInfo)),
                biWidth = blitBounds.Width,
                biHeight = -blitBounds.Height,
                biPlanes = 1,
                biBitCount = 32,
                biCompression = 0
            };

            IntPtr dib = Methods.CreateDIBSection(outputHdc, ref bmi, 0, 0, IntPtr.Zero, 0);
            Methods.SelectObject(memDc, dib);

            using (Graphics memG = Graphics.FromHdc(memDc))
                memG.DrawImageUnscaled(image, 0, 0);

            Methods.BitBlt(outputHdc, blitBounds.X, blitBounds.Y, blitBounds.Width, blitBounds.Height, memDc, 0, 0, BitBltOp.SRCCOPY);

            Methods.DeleteObject(dib);
            Methods.DeleteDC(memDc);
            outputG.ReleaseHdc(outputHdc);
        }

        private Bitmap PremultiplyAlpha(Image image)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }
            return bmp;
        }
    }
}