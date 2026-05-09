using DirectUI.Net.Events;
using DirectUI.Net.Hosting;
using Naticord.Forms.SetupFlows;
using Naticord.Networking.WebSockets;
using System;
using System.Windows.Forms;

namespace Naticord.Forms.LoginFlows
{
    public partial class QRLogin : Form
    {
        private readonly DuiFormAttachment _qrHost;
        private AuthSocket _authSocket;
        private Login _loginForm;

        public QRLogin(Login loginForm)
        {
            InitializeComponent();

            _loginForm = loginForm;
            _qrHost = new DuiFormAttachment(this);
        }

        private async void QRLogin_Load(object sender, EventArgs e)
        {
            _qrHost.LoadXmlFile("User Interface\\QR.xml", resId: "qrMain");
            _qrHost.ButtonClicked += OnDuiClick;

            _authSocket = new AuthSocket();
            _authSocket.qrCodeReady += OnQrCodeReady;
            _authSocket.tokenReceived += OnTokenReceived;

            try
            {
                await _authSocket.StartSocket();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to the gateway! " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void OnQrCodeReady(string url)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    Width = qrPictureBox.Width,
                    Height = qrPictureBox.Height,
                    Margin = 1
                }
            };
            qrPictureBox.Image = writer.Write(url);
        }

        private void OnTokenReceived(string token)
        {
            Properties.Settings.Default.dscToken = token;
            Properties.Settings.Default.Save();

            this.Close();
            if (!Properties.Settings.Default.hasCompletedSetup)
            {
                // Continue to the setup flow
                SetupFlowPiece setupFlowPiece = new SetupFlowPiece();
                setupFlowPiece.Show();

                _loginForm.Hide(); // Can't close since it is the host form

            }
            else
            {
                // Continue to the client
                Client clientForm = new Client();
                clientForm.Show();

                _loginForm.Hide(); // Can't close since it is the host form
            }
        }

        private void OnDuiClick(object s, DuiClickEventArgs e) { if (e.ElementId == "btnCancelQr") { Close(); } }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _authSocket?.StopSocket();
            _authSocket?.Dispose();
            base.OnFormClosed(e);
        }
    }
}