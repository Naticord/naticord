using DirectUI.Net.Hosting;
using System;
using System.Windows.Forms;

namespace Naticord.Forms.SetupFlows
{
    public partial class FinishedPage : Form
    {
        private readonly DuiFormAttachment _finishedHost;

        public FinishedPage()
        {
            InitializeComponent();
            _finishedHost = new DuiFormAttachment(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _finishedHost.LoadXmlFile("User Interface\\SetupFlow\\Finished.xml", resId: "finishMain");
        }
    }
}