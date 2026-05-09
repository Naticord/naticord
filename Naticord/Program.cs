using DirectUI.Net;
using Naticord.Forms;
using Naticord.Forms.LoginFlows;
using Naticord.Forms.SetupFlows;
using System;
using System.Windows.Forms;

namespace Naticord
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize DirectUI before starting any form!
            DuiInitializer.Instance.Initialize();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.dscToken))
            {
                if (!Properties.Settings.Default.hasCompletedSetup) { Application.Run(new SetupFlowPiece()); }
                else { Application.Run(new Client()); }
            }
            else { Application.Run(new Login()); }
        }
    }
}