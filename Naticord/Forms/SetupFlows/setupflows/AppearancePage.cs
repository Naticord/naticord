using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naticord.Forms.SetupFlows
{
    public partial class AppearancePage : Form
    {
        public AppearancePage()
        {
            InitializeComponent();
        }

        private void skeuoRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (skeuoRadio.Checked == true)
            {
                Properties.Settings.Default.iconStyle = "Skeuomorphic";

                // Change all of the picture boxes to the right style
                accountIcon.Image = Properties.Resources.account_skeuo;
                settingsIcon.Image = Properties.Resources.cog_skeuo;
                githubIcon.Image = Properties.Resources.github_skeuo;
                dmIcon.Image = Properties.Resources.dm_skeuo;
                serversIcon.Image = Properties.Resources.servers_skeuo;
            }
        }

        private void modernRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (modernRadio.Checked == true)
            {
                Properties.Settings.Default.iconStyle = "Modern";

                // Change all of the picture boxes to the right style
                accountIcon.Image = Properties.Resources.account;
                settingsIcon.Image = Properties.Resources.cog;
                githubIcon.Image = Properties.Resources.github;
                dmIcon.Image = Properties.Resources.dm;
                serversIcon.Image = Properties.Resources.servers;
            }
        }
    }
}
