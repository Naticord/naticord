// Taken from WindowsFormsAero

using System;
using System.Runtime.InteropServices;

namespace Naticord.Classes {
    [StructLayout(LayoutKind.Sequential)]
    internal struct DttOpts {
        public int dwSize;
        public DttOptsFlags dwFlags;
        public int crText;
        public int crBorder;
        public int crShadow;
        public int iTextShadowType;
        public Point ptShadowOffset;
        public int iBorderSize;
        public int iFontPropId;
        public int iColorPropId;
        public int iStateId;
        public bool fApplyOverlay;
        public int iGlowSize;
        public int pfnDrawTextCallback;
        public IntPtr lParam;
    }
}
