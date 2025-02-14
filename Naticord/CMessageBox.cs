using System;
using System.Windows.Forms;

namespace Naticord
{
    public partial class CMessageBox : Form
    {
        public CMessageBox(string messageTitleText, string messageContentText)
        {
            InitializeComponent();

            messageTitle.Text = messageTitleText;
            messageContent.Text = messageContentText;
            this.Text = messageTitleText;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(messageContent.Text);
        }
    }
}
