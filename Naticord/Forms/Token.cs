using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Token : Form
    {
        private string tokenText;
        private Login loginForm;

        public Token(Login loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.AcceptButton = okButton;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            tokenText = tokenBox.Text;
            Properties.Settings.Default.token = tokenText;
            Properties.Settings.Default.Save();
            // Continue to client
            loginForm.Hide();
        }
    }
}
