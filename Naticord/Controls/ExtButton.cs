using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Controls
{
    public partial class ExtButton : UserControl
    {
        private string _buttonLabel;
        private Image _buttonIcon;
        private bool _compositionDisabled;

        public event EventHandler ButtonClick;

        public ExtButton()
        {
            InitializeComponent();
            _buttonLabel = "Button";
            _buttonIcon = null;
            _compositionDisabled = false;
            buttonLabel.Text = _buttonLabel;

            this.Click += AllParts_Click;
            foreach (Control ctrl in Controls)
                ctrl.Click += AllParts_Click;

            buttonIcon.Paint += (s, e) =>
            {
                if (buttonIcon.Image != null)
                {
                    e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                    e.Graphics.DrawImage(buttonIcon.Image, new Rectangle(0, 0, buttonIcon.Width, buttonIcon.Height));
                }
            };
        }

        private void AllParts_Click(object sender, EventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }

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
                }
            }
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