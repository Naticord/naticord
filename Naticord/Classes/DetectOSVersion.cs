using System;
using System.Runtime.InteropServices;

namespace Naticord.Classes
{
    internal class DetectOSVersion
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [DllImport("ntdll.dll")]
        private static extern int RtlGetVersion(ref OSVERSIONINFOEX lpVersionInformation);

        private static Version GetWindowsVersion()
        {
            var osInfo = new OSVERSIONINFOEX { dwOSVersionInfoSize = (uint)Marshal.SizeOf<OSVERSIONINFOEX>() };
            if (RtlGetVersion(ref osInfo) == 0)
            {
                return new Version(
                    (int)osInfo.dwMajorVersion,
                    (int)osInfo.dwMinorVersion,
                    (int)osInfo.dwBuildNumber
                );
            }

            return Environment.OSVersion.Version;
        }

        public static int MajorVersion => GetWindowsVersion().Major;
        public static int MinorVersion => GetWindowsVersion().Minor;
        public static int BuildNumber => GetWindowsVersion().Build;

        public static bool IsWindows7To81() => MajorVersion == 6 && MinorVersion >= 1;
        public static bool IsWindows10() => MajorVersion == 10 && BuildNumber < 22000;
        public static bool IsWindows11() => MajorVersion == 10 && BuildNumber >= 22000;

        public static bool IsAtLeastWindows10() => MajorVersion >= 10;
        public static bool IsAtLeastWindows11() => MajorVersion == 10 && BuildNumber >= 22000;

        public static string GetFriendlyWindowsVersion()
        {
            if (IsWindows11()) return "Windows 11";
            if (IsWindows10()) return "Windows 10";
            if (IsWindows7To81()) return "Windows 7/8/8.1";
            return $"Unknown system";
        }
    }
}