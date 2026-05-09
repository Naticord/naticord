using Naticord.Classes;
using Naticord.Classes.ExtendedGlass;
using Naticord.Forms.SetupFlows;
using Naticord.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms.SetupFlows
{
    public partial class SetupFlowPiece : GlassForm
    {
        private (Form form, string name)[] _pages;
        private int _currentPage = 0;

        private string WINDOWS_11 = "Windows 11 - 10";
        private string WINDOWS_8DOT1 = "Windows 8.1 - 7";

        public SetupFlowPiece()
        {
            DecideOSSettings();
            InitializeComponent();
            this.MinimumSize = this.Size;

            // Apply the correct composition to the window!
            CompSet(DetectDwmStatus.IsDwmEnabled(), true, this);
            // Apply the toolbar padding to SetupFlowPiece!
            // No, there isn't a better way to do this... It works though!
            if (Properties.Settings.Default.layoutMode == WINDOWS_11) { buttonPanel.Width -= 16; buttonPanel.Left += 8; }

            _pages = new (Form form, string name)[]
            {
                (new IntroductionPage(), "Introduction"),
                (new AppearancePage(this), "Appearance"),
                (new FinishedPage(), "You're all set!")
            };

            foreach (var page in _pages)
            {
                page.form.TopLevel = false;
                page.form.FormBorderStyle = FormBorderStyle.None;
                page.form.Dock = DockStyle.Fill;

                pagePanel.Controls.Add(page.form);

                page.form.Show();
                page.form.Hide();
            }

            nextButton.ButtonClick += OnNextClicked;
            backButton.ButtonClick += OnBackClicked;

            NavigateTo(0);
        }

        private void DecideOSSettings()
        {
            // Decide the rendering mode that Naticord should use on the titlebar, based on OS and theme settings
            if (DetectDwmStatus.IsDwmEnabled())
            {
                if (DetectOSVersion.IsWindows7To81()) { Properties.Settings.Default.renderMode = "Aero"; }
                else if (DetectOSVersion.IsWindows10()) { Properties.Settings.Default.renderMode = "Acrylic"; }
                else if (DetectOSVersion.IsWindows11()) { Properties.Settings.Default.renderMode = "Mica"; }
            }
            else { Properties.Settings.Default.renderMode = "Composition disabled"; }
            RenderMode = Properties.Settings.Default.renderMode;

            // Decide the toolbar mode that Naticord should use, based on OS (only set default on first run)
            if (DetectOSVersion.IsWindows7To81()) { Properties.Settings.Default.layoutMode = WINDOWS_8DOT1; }
            else if (DetectOSVersion.IsWindows10() || DetectOSVersion.IsWindows11()) { Properties.Settings.Default.layoutMode = WINDOWS_11; }

            Properties.Settings.Default.Save();
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPage < _pages.Length - 1)
                NavigateTo(_currentPage + 1);
            else
            {
                Properties.Settings.Default.hasCompletedSetup = true;
                Properties.Settings.Default.Save();

                // Continue to the client
                Client clientForm = new Client();
                clientForm.Show();

                this.Hide();
            }
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            if (_currentPage > 0)
                NavigateTo(_currentPage - 1);
        }

        private void NavigateTo(int index)
        {
            _pages[_currentPage].form.Hide();
            _currentPage = index;

            _pages[_currentPage].form.Show();
            backButton.Visible = index > 0;

            nextButton.ButtonLabel = index == _pages.Length - 1 ? "Finish up!" : "Next page";
            SetPagePieceHeader();
        }

        private void SetPagePieceHeader()
        {
            if (pageCountRenderer.DuiWindow == null) return;

            string text = $"{_pages[_currentPage].name} - Page {_currentPage + 1} out of {_pages.Length}";
            pageCountRenderer.DuiWindow.SetText("pageCountText", text);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // This is a shitty hack that we need because for some reason CenterScreen doesn't work?
            // Thanks Windows Forms! You are the best UI framework I have ever used!
            var screen = Screen.FromPoint(Cursor.Position).WorkingArea;
            this.Location = new Point(
                screen.Left + (screen.Width - this.Width) / 2,
                screen.Top + (screen.Height - this.Height) / 2
            );

            if (DetectDwmStatus.IsDwmEnabled())
            {
                buttonPanel.Top = 0;
                pageCountRenderer.Top = glassMargin.Top;
            }
            else
            {
                buttonPanel.Top = 0;
                buttonPanel.BackColor = SystemColors.ButtonFace;
                pageCountRenderer.Top = buttonPanel.Bottom;
            }

            int panelTop = pageCountRenderer.Bottom;

            pagePanel.Top = panelTop;
            pagePanel.Height = ClientSize.Height - panelTop;

            SetPagePieceHeader();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _pages != null)
            {
                foreach (var page in _pages)
                    page.form.Dispose();
                _pages = null;
            }
            base.Dispose(disposing);
        } 
    }
}