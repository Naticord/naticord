using DirectUI.Net.Hosting;
using System;
using System.Windows.Forms;

namespace Naticord.Forms.ClientFlows
{
    public partial class StatusPage : Form
    {
        private readonly DuiFormAttachment _statusHost;

        public StatusPage()
        {
            InitializeComponent();
            _statusHost = new DuiFormAttachment(this);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _statusHost.LoadXmlFile("User Interface\\ClientFlow\\StatusPage.xml", resId: "statusMain");
        }
    }
}