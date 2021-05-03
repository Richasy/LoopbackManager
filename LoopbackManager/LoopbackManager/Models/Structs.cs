using System;
using System.Runtime.InteropServices;

namespace LoopbackManager.Models
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SID_AND_ATTRIBUTES
    {
        public IntPtr Sid;
        public uint Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct INET_FIREWALL_AC_CAPABILITIES
    {
        public uint count;
        public IntPtr capabilities; //SID_AND_ATTRIBUTES
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct INET_FIREWALL_AC_BINARIES
    {
        public uint count;
        public IntPtr binaries;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct INET_FIREWALL_APP_CONTAINER
    {
        internal IntPtr appContainerSid;
        internal IntPtr userSid;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string appContainerName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string displayName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string description;
        internal INET_FIREWALL_AC_CAPABILITIES capabilities;
        internal INET_FIREWALL_AC_BINARIES binaries;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string workingDirectory;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string packageFullName;
    }
}
