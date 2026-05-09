// This has been taken from the 'aerocord' project found here:
// https://github.com/jukfiuune/aerocord
// Credit goes to them for this

using System;
using System.Runtime.InteropServices;

namespace Naticord.Classes.ExtendedGlass
{
    public class GlassInvoke
    {
        public class ParameterTypes
        {
            public enum DWMWINDOWATTRIBUTE { DWMWA_SYSTEMBACKDROP_TYPE = 38 }
            public struct MARGINS
            {
                public int cxLeftWidth;
                public int cxRightWidth;
                public int cyTopHeight;
                public int cyBottomHeight;
            }
        }

        public static class DwmMethods
        {
            [DllImport("dwmapi.dll")]
            private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref GlassInvoke.ParameterTypes.MARGINS pMarInset);

            [DllImport("dwmapi.dll")]
            private static extern int DwmSetWindowAttribute(IntPtr hwnd, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE dwAttribute, ref int pvAttribute, int cbAttribute);

            public static int ExtendFrame(IntPtr hwnd, GlassInvoke.ParameterTypes.MARGINS margins)
                => DwmExtendFrameIntoClientArea(hwnd, ref margins);

            public static int SetWindowAttribute(IntPtr hwnd, GlassInvoke.ParameterTypes.DWMWINDOWATTRIBUTE attribute, int parameter)
                => DwmSetWindowAttribute(hwnd, attribute, ref parameter, sizeof(int));
        }
    }
}