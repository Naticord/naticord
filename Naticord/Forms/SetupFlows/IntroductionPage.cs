using DirectUI.Net.Hosting;
using System;
using System.Windows.Forms;

namespace Naticord.Forms.SetupFlows
{
    public partial class IntroductionPage : Form
    {
        private readonly DuiFormAttachment _introHost;

        public IntroductionPage()
        {
            InitializeComponent();
            _introHost = new DuiFormAttachment(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _introHost.LoadXmlFile("User Interface\\SetupFlow\\Introduction.xml", resId: "introMain");
        }
    }
}