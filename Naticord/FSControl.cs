using System.Drawing;
using System.Windows.Forms;

namespace Naticord
{
    public partial class FSControl : UserControl
    {
        public FSControl()
        {
            InitializeComponent();
        }

        public string LText
        {
            get => nameLabel.Text;
            set
            {
                if (value.Length >= 19)
                    nameLabel.Text = value.Substring(0, 16) + "...";
                else
                    nameLabel.Text = value;
            }
        }

        public Image PFPPic
        {
            get => profilePictureItem.Image;
            set => profilePictureItem.Image = value;
        }
    }
}
