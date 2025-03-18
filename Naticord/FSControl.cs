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

        public void ClickedDesignChange(bool clicked)
        {
            if (clicked)
            {
                nameLabel.ForeColor = Color.White;
                this.BackColor = Color.MediumPurple;
            }
            else
            {
                nameLabel.ForeColor = Color.Black;
                this.BackColor = Color.White;
            }
        }

        public string LText
        {
            get => nameLabel.Text;
            set
            {
                if (value.Length >= 17)
                    nameLabel.Text = value.Substring(0, 14) + "...";
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
