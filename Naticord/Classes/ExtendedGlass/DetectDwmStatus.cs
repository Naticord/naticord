using System;
using System.Runtime.InteropServices;

namespace Naticord.Classes.ExtendedGlass
{
    internal class DetectDwmStatus
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern void DwmIsCompositionEnabled(out bool pfEnabled);

        public static bool IsDwmEnabled()
        {
            try
            {
                DwmIsCompositionEnabled(out bool isEnabled);
                return isEnabled;
            }
            catch (Exception) { return false; }
        }
    }
}