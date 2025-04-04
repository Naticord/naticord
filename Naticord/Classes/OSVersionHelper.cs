using System.Runtime.InteropServices;

namespace Naticord.Classes
{
    internal class OSVersionHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int RtlGetVersion(ref OSVERSIONINFOEX versionInfo);

        public static string GetWindowsVersion()
        {
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

            if (RtlGetVersion(ref osVersionInfo) == 0)
            {
                if (osVersionInfo.dwMajorVersion == 6)
                {
                    switch (osVersionInfo.dwMinorVersion)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return "Windows 7 - 8.1";
                    }
                }
                else if (osVersionInfo.dwMajorVersion == 10)
                {
                    return osVersionInfo.dwBuildNumber >= 22000 ? "Windows 11" : "Windows 10";
                }
            }
            return "Unknown";
        }
    }
}