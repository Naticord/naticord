// Taken from WindowsFormsAero

using System;

namespace Naticord.Classes.ExtendedGlass.WindowsFormsAero
{
    [Flags]
    internal enum DttOptsFlags : int
    {
        DTT_TEXTCOLOR = 1,
        DTT_BORDERCOLOR = 2,
        DTT_SHADOWCOLOR = 4,
        DTT_SHADOWTYPE = 8,
        DTT_SHADOWOFFSET = 16,
        DTT_BORDERSIZE = 32,
        DTT_CALCRECT = 512,
        DTT_APPLYOVERLAY = 1024,
        DTT_GLOWSIZE = 2048,
        DTT_COMPOSITED = 8192
    }
}