using System;
using System.Runtime.InteropServices;

namespace Zeyo.FFOTagKiller.Common
{
  partial class WinAPI
  {
    [Flags]
    internal enum SYSTEM_INFORMATION_CLASS : uint
    {
      SYSTEM_HANDLE_INFORMATION = 16
    }

    [Flags]
    internal enum OBJECT_INFORMATION_CLASS : uint
    {
      OBJECT_BASIC_INFORMATION = 0,
      OBJECT_NAME_INFORMATION = 1,
      OBJECT_TYPE_INFORMATION = 2,
      OBJECT_ALL_TYPES_INFORMATION = 3,
      OBJECT_HANDLE_INFORMATION = 4
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct OBJECT_BASIC_INFORMATION
    {
      public uint ATTRIBUTES;
      public uint GRANTED_ACCESS;
      public uint HANDLE_COUNT;
      public uint POINTER_COUNT;
      public uint PAGED_POOL_USAGE;
      public uint NON_PAGED_POOL_USAGE;
      public uint RESERVED_1;
      public uint RESERVED_2;
      public uint RESERVED_3;
      public uint NAME_INFORMATION_LENGTH;
      public uint TYPE_INFORMATION_LENGTH;
      public uint SECURITY_DESCRIPTOR_LENGTH;
      public System.Runtime.InteropServices.ComTypes.FILETIME CREATE_TIME;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct OBJECT_NAME_INFORMATION
    {
      public UNICODE_STRING NAME;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct OBJECT_TYPE_INFORMATION
    {
      public UNICODE_STRING NAME;
      public int OBJECT_COUNT;
      public int HANDLE_COUNT;
      public int RESERVED_1;
      public int RESERVED_2;
      public int RESERVED_3;
      public int RESERVED_4;
      public int PEAK_OBJECT_COUNT;
      public int PEAK_HANDLE_COUNT;
      public int RESERVED_5;
      public int RESERVED_6;
      public int RESERVED_7;
      public int RESERVED_8;
      public int INVALID_ATTRIBUTES;
      public GENERIC_MAPPING GENERIC_MAPPING;
      public int VALID_ACCESS;
      public byte UNKNOWN;
      public byte MAINTAIN_HANDLE_DATABASE;
      public int POOL_TYPE;
      public int PAGED_POOL_USAGE;
      public int NON_PAGED_POOL_USAGE;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct SYSTEM_HANDLE_INFORMATION
    {
      public int OWNER_PID;
      public byte OBJECT_TYPE;
      public byte HANDLE_FLAGS;
      public ushort HANDLE_VALUE;
      public UIntPtr OBJECT_POINTER;
      public IntPtr ACCESS_MASK;
    }

    [Flags]
    internal enum NT_STATUS : uint
    {
      STATUS_SUCCESS = 0x00000000,
      STATUS_INFO_LENGTH_MISMATCH = 0xC0000004,
      STATUS_INVALID_HANDLE = 0xC0000008
    }

    [DllImport("ntdll.dll", CharSet = CharSet.Unicode, EntryPoint = "NtQuerySystemInformation")]
    internal static extern NT_STATUS NT_QUERY_SYSTEM_INFORMATION
    (
      SYSTEM_INFORMATION_CLASS SYSTEM_INFORMATION_CLASS,
      IntPtr SYSTEM_INFORMATION,
      int SYSTEM_INFORMATION_LENGTH,
      out int RETURN_LENGTH
    );

    [DllImport("ntdll.dll", CharSet = CharSet.Unicode, EntryPoint = "NtQueryObject")]
    internal static extern NT_STATUS NT_QUERY_OBJECT
    (
      IntPtr OBJECT_HANDLE,
      OBJECT_INFORMATION_CLASS OBJECT_INFORMATION_CLASS,
      IntPtr OBJECT_INFORMATION,
      int OBJECT_INFORMATION_LENGTH,
      out int RETURN_LENGTH
    );
  }
}
