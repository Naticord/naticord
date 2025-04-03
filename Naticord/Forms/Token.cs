using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Naticord.Forms
{
    public partial class Token : Form
    {
        private string tokenText;
        private Login loginForm;
        private bool isClassicMode = !Application.RenderWithVisualStyles || !VisualStyleInformation.IsEnabledByUser;

        public Token(Login loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.AcceptButton = okButton;
            if (isClassicMode)
            {
                // Do nothing if Classic is enabled, make it's look ugly.
            }
            else
            {
                okButton.BackColor = Color.Transparent;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            tokenText = tokenBox.Text;
            Properties.Settings.Default.token = tokenText;
            Properties.Settings.Default.Save();
            Client clientForm = new Client();
            clientForm.Show();
            loginForm.Hide();
            this.Close();
        }
    }
}