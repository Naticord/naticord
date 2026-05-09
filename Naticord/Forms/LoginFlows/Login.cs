using DirectUI.Net.Events;
using Naticord.Forms.SetupFlows;
using Naticord.Networking;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Aztec.Internal;

namespace Naticord.Forms.LoginFlows
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // Input the event subscribers right now!
            loginDuiRenderer.ButtonClicked += OnDuiClick;
        }

        private async Task SignInSequence()
        {
            string dscToken = loginDuiRenderer.Element("editToken").GetText();
            if (string.IsNullOrEmpty(dscToken))
            {
                MessageBox.Show("You have not entered a Discord token, put one in and try again.", "Naticord", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Debug.WriteLine($"The entered token is: {dscToken}");

            // Verify that the token actually functions correctly
            string corrToken = await API.Instance.SendAPI("/users/@me", HttpMethod.Get, dscToken, null, null, null, null);

            // Check for the "Unauthorized" in the request, which means the token isn't correct
            if (corrToken.Contains("Unauthorized"))
            {
                Debug.WriteLine("Token is not correct and hasn't been saved to local settings.");
                var forceBox = MessageBox.Show(
                    "Your token is incorrect, please verify that you have entered it correctly and try again. If you are sure this is the right token, click 'OK' to force it to save.",
                    "Naticord",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error
                );

                if (forceBox == DialogResult.OK) { SaveToken(dscToken); }
                return;
            }
            else
            {
                Debug.WriteLine("Token has been saved to local settings.");
                SaveToken(dscToken);
            }
        }

        private async void OnDuiClick(object s, DuiClickEventArgs e)
        {
            if (e.ElementId == "btnSignIn")
            {
                loginDuiRenderer.Element("btnSignIn").SetEnabled(false);

                // Attempt to sign into Discord with a token
                try { await SignInSequence(); }
                finally { loginDuiRenderer.Element("btnSignIn").SetEnabled(true); }
            }
            else if (e.ElementId == "btnQrLaunch")
            {
                loginDuiRenderer.Element("btnQrLaunch").SetEnabled(false);

                // Open the QR login dialog
                new QRLogin(this).ShowDialog();
                loginDuiRenderer.Element("btnQrLaunch").SetEnabled(true);
            }
        }

        private void SaveToken(string dscToken)
        {
            Properties.Settings.Default.dscToken = dscToken;
            Properties.Settings.Default.Save();

            if (!Properties.Settings.Default.hasCompletedSetup)
            {
                // Continue to the setup flow
                SetupFlowPiece setupFlowPiece = new SetupFlowPiece();
                setupFlowPiece.Show();

                this.Hide();
            }
            else
            {
                // Continue to the client
                Client clientForm = new Client();
                clientForm.Show();

                this.Hide();
            }
        }
    }
}