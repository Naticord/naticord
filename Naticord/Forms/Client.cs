using Naticord.Classes.ExtendedGlass;
using Naticord.Forms.ClientFlows;
using Naticord.Forms.SettingsFlows;
using Naticord.Networking;
using Naticord.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class Client : GlassForm
    {
        private string dscToken = Properties.Settings.Default.dscToken;

        private string WINDOWS_11 = "Windows 11 - 10";
        private string WINDOWS_8DOT1 = "Windows 8.1 - 7";

        private readonly List<DuiListItem> _friendItems = new List<DuiListItem>();

        private IntPtr _hDmIcon;
        private IntPtr _hServerIcon;

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public void LoadFormIntoPanel(Form form)
        {
            foreach (Control c in contentPanel.Controls)
            {
                c.Dispose();
            }
            contentPanel.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(form);
            form.Show();
        }

        private void SetSidebarIcon(string baseName, string elementId, ref IntPtr hCache)
        {
            if (hCache != IntPtr.Zero) { DeleteObject(hCache); hCache = IntPtr.Zero; }

            string suffix = string.Equals(Properties.Settings.Default.iconStyle, "Skeuomorphic", StringComparison.OrdinalIgnoreCase) ? "-skeuo" : "";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExtResources", $"{baseName}{suffix}.bmp");

            if (File.Exists(path))
            {
                using (var src = Image.FromFile(path))
                using (var bmp = new Bitmap(src))
                    hCache = bmp.GetHbitmap(Color.Black);
            }

            sidebarRenderer.DuiWindow?.SetContentBitmap(elementId, hCache);
        }

        internal DuiListItem AddFriendItem(string username, string statusText, Bitmap avatar, int id, int statusCode, int extraStatusCode = 5)
        {
            var duiWin = sidebarRenderer.DuiWindow;
            if (duiWin == null) return null;

            var listItem = new DuiListItem(id);

            using (duiWin.BeginDefer())
            {
                listItem.Mount(duiWin, "friendList");
                _friendItems.Add(listItem);

                listItem.SetUsername(username);
                listItem.SetStatus(statusText);

                if (avatar != null) listItem.SetProfilePicture(avatar);
                listItem.SetStatusIcon(statusCode, extraStatusCode);
            }

            return listItem;
        }

        public Client()
        {
            InitializeComponent();
            MinimumSize = new Size(800, 500);

            // Apply the correct composition to the window!
            CompSet(DetectDwmStatus.IsDwmEnabled(), false, null, this);
            // Apply the toolbar layout to the buttonPanel, like in SetupFlowPiece
            if (Properties.Settings.Default.layoutMode == WINDOWS_11) { buttonPanel.Width -= 16; buttonPanel.Left += 8; }
        }

        private async void OnLoad(object sender, EventArgs e)
        {
            // Load the StatusPage into the contentPanel
            StatusPage statusPage = new StatusPage();
            LoadFormIntoPanel(statusPage);

            await SetUserInformation();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // (If enabled!) Disable the GitHub button on the toolbar
            if (Properties.Settings.Default.disableGitHubButton) { githubButton.Visible = false; }

            // Configure the user interface to look nice on all different system metric sizes
            sidebarRenderer.SetCueBanner("searchBox", "Search...");
            sidebarRenderer.SetCueBannerItalic("searchBox");

            var screen = Screen.FromPoint(Cursor.Position).WorkingArea;
            this.Location = new Point(
                screen.Left + (screen.Width - this.Width) / 2,
                screen.Top + (screen.Height - this.Height) / 2
            );

            if (DetectDwmStatus.IsDwmEnabled())
            {
                buttonPanel.Top = 0;
                sidebarRenderer.Top = glassMargin.Top;
            }
            else
            {
                buttonPanel.Top = 0;
                sidebarRenderer.Top = buttonPanel.Bottom;
            }

            int panelTop = sidebarRenderer.Top;

            contentPanel.Top = panelTop;
            sidebarRenderer.Height = ClientSize.Height - panelTop;
            contentPanel.Height = ClientSize.Height - panelTop;

            AddFriendItem("Placeholder username", "Listening to a song", null, 0, 1, 1);

            SetSidebarIcon("dm", "dmPic", ref _hDmIcon);
            SetSidebarIcon("servers", "serverPic", ref _hServerIcon);
        }

        private async Task SetUserInformation()
        {
            // Use saved details first before actually loading data from Discord
            usernameLabel.Text = Properties.Settings.Default.dscUsername;

            string userInfo = await API.Instance.SendAPI("users/@me", HttpMethod.Get, dscToken, null, null, null, null);
            if (userInfo != null)
            {
                if (userInfo.Contains("Unauthorized"))
                {
                    MessageBox.Show("Your token has expired or is invalid. Naticord will sign you out and you will need to sign in again.", "Naticord", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Wipe all Discord properties
                    Properties.Settings.Default.dscToken = null;
                    Properties.Settings.Default.dscUsername = null;
                    Properties.Settings.Default.dscUid = null;
                    Properties.Settings.Default.Save();

                    Application.Restart();
                }
                else
                {
                    var parsedUser = JsonNode.Parse(userInfo).AsObject();

                    string userId = parsedUser["id"]?.GetValue<string>();
                    string dscUsername = parsedUser["username"]?.GetValue<string>() ?? "anonymous_user";
                    string displayName = parsedUser["global_name"]?.GetValue<string>() ?? dscUsername;
                    string avatarHash = parsedUser["avatar"]?.GetValue<string>();

                    // Save properties to settings for use!
                    Properties.Settings.Default.dscUsername = displayName;
                    Properties.Settings.Default.dscUid = userId;
                    Properties.Settings.Default.Save();

                    usernameLabel.Text = displayName;
                    // TODO: Add avatar support
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_hDmIcon != IntPtr.Zero) { DeleteObject(_hDmIcon); _hDmIcon = IntPtr.Zero; }
            if (_hServerIcon != IntPtr.Zero) { DeleteObject(_hServerIcon); _hServerIcon = IntPtr.Zero; }

            base.OnFormClosed(e);
        }

        private void accountButton_ButtonClick(object sender, EventArgs e)
        {
            accountContext.Show(accountButton, new System.Drawing.Point(0, accountButton.Height));
        }

        private void settingsButton_ButtonClick(object sender, EventArgs e)
        {
            SettingsFlowPiece settingsPiece = new SettingsFlowPiece();
            settingsPiece.ShowDialog();
        }

        private void logOutStripItem_Click(object sender, EventArgs e)
        {
            // Wipe all Discord properties
            Properties.Settings.Default.dscToken = null;
            Properties.Settings.Default.dscUsername = null;
            Properties.Settings.Default.dscUid = null;
            Properties.Settings.Default.Save();

            var resetAllDlg = MessageBox.Show("Do you want to reset all of your Naticord preferences too?", "Naticord", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resetAllDlg == DialogResult.Yes)
            {
                // Wipe all of the Naticord settings to their defaults
                Properties.Settings.Default.renderMode = null;
                Properties.Settings.Default.hasCompletedSetup = false;
                Properties.Settings.Default.iconStyle = "Skeuomorphic";
                Properties.Settings.Default.layoutMode = null;
                Properties.Settings.Default.disableGitHubButton = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                // Do nothing...
            }

            Properties.Settings.Default.Save();
            Application.Restart();
        }
    }
}