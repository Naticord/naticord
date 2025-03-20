using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Naticord
{
    public partial class Message : UserControl
    {
        public Message()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }

        public string authorText
        {
            get => authorLabel.Text;
            set { authorLabel.Text = value; CAutoSize(); }
        }

        public string messageContentText
        {
            get => messageContent.Text;
            set { messageContent.Text = value; CAutoSize(); }
        }

        public Image PFPPicAuthor
        {
            get => profilePictureAuthor.Image;
            set => profilePictureAuthor.Image = value;
        }

        public Image attachmentImageDisplay
        {
            get => messageContent.MDImage;
            set
            {
                messageContent.MDImage = value;
                bool isGif = value != null && ImageFormat.Gif.Equals(value.RawFormat);

                if (isGif)
                {
                    messageContent.Text = $"{messageContent.Text}\n***Naticord doesn't support GIFs right now.***";
                    messageContent.MDImage = null;
                }

                CAutoSize();
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            CAutoSize();
        }

        // Basically AutoSize.
        private void CAutoSize()
        {
            int padding = 3;
            int totalHeight = padding;

            if (profilePictureAuthor.Visible)
                totalHeight = Math.Max(totalHeight, profilePictureAuthor.Bottom + padding);
            if (authorLabel.Visible)
                totalHeight = Math.Max(totalHeight, authorLabel.Bottom + padding);
            if (messageContent.Visible)
                totalHeight = Math.Max(totalHeight, messageContent.Bottom + padding);
            if (messageContent.MDImage != null)
                totalHeight = Math.Max(totalHeight, messageContent.Bottom + padding);

            this.Height = totalHeight;
        }
    }
}
