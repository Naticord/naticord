using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Naticord
{
    public partial class CommandLink : Button
    {
        private const int BS_COMMANDLINK = 0x0000000E;
        private const int BCM_SETNOTE = 0x00001609;

        private string noteText = "";

        public CommandLink()
        {
            FlatStyle = FlatStyle.System;
            this.CreateHandle();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= BS_COMMANDLINK;
                return cp;
            }
        }

        [Category("Appearance")]
        [Description("The secondary text displayed below the main button text.")]
        public string Note
        {
            get => noteText;
            set
            {
                noteText = value;
                if (IsHandleCreated)
                {
                    SendMessage(Handle, BCM_SETNOTE, IntPtr.Zero, value);
                    this.Invalidate();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Note = noteText;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
    }
}
