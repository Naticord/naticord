using Naticord.Classes;
using Naticord.Classes.ExtendedGlass;
using Naticord.Forms.SetupFlows;
using Naticord.UserControls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naticord.Forms
{
    public partial class SetupFlowPiece : GlassForm
    {
        private Form[] _pages;
        private int _currentPage = 0;

        public SetupFlowPiece()
        {
            if (DetectDwmStatus.IsDwmEnabled())
            {
                if (DetectOSVersion.IsWindows7To81()) { Properties.Settings.Default.renderMode = "Aero"; }
                else if (DetectOSVersion.IsWindows10()) { Properties.Settings.Default.renderMode = "Acrylic"; }
                else if (DetectOSVersion.IsWindows11()) { Properties.Settings.Default.renderMode = "Mica"; }
            }
            else { Properties.Settings.Default.renderMode = "Composition disabled"; }
            Properties.Settings.Default.Save();

            RenderMode = Properties.Settings.Default.renderMode;

            InitializeComponent();
            CompSet(DetectDwmStatus.IsDwmEnabled(), true, this);

            _pages = new Form[]
            {
                new IntroductionPage(),
                new AppearancePage(),
            };

            foreach (Form page in _pages)
            {
                page.TopLevel = false;
                page.FormBorderStyle = FormBorderStyle.None;
                page.Dock = DockStyle.Fill;

                pagePanel.Controls.Add(page);

                page.Show();
                page.Hide();
            }

            nextButton.ButtonClick += OnNextClicked;
            backButton.ButtonClick += OnBackClicked;

            NavigateTo(0);
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_currentPage < _pages.Length - 1)
                NavigateTo(_currentPage + 1);
        }

        private void OnBackClicked(object sender, EventArgs e)
        {
            if (_currentPage > 0)
                NavigateTo(_currentPage - 1);
        }

        private void NavigateTo(int index)
        {
            _pages[_currentPage].Hide();
            _currentPage = index;

            _pages[_currentPage].Show();
            backButton.Visible = index > 0;

            nextButton.ButtonLabel = index == _pages.Length - 1 ? "Finish up!" : "Next page";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _pages != null)
            {
                foreach (Form page in _pages)
                    page.Dispose();
                _pages = null;
            }
            base.Dispose(disposing);
        }
    }
}