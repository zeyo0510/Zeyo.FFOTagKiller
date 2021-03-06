using System;
using System.Runtime.InteropServices;

namespace Zeyo.FFOTagKiller.Common
{
  partial class WinAPI
  {
    [Flags]
    internal enum PROCESS_ACCESS_RIGHTS : uint
    {
      PROCESS_ALL = 0x001F0FFF,
      PROCESS_TERMINATE = 0x00000001,
      PROCESS_CREATE_THREAD = 0x00000002,
      PROCESS_VM_OPERATION = 0x00000008,
      PROCESS_VM_READ = 0x00000010,
      PROCESS_VM_WRITE = 0x00000020,
      PROCESS_DUP_HANDLE = 0x00000040,
      PROCESS_CREATE_PROCESS = 0x00000080,
      PROCESS_SET_QUOTA = 0x00000100,
      PROCESS_SET_INFORMATION = 0x00000200,
      PROCESS_QUERY_INFORMATION = 0x00000400,
      PROCESS_SUSPEND_RESUME = 0x00000800,
      PROCESS_QUERY_LIMITED_INFORMATION = 0x00001000,
      SYNCHRONIZE = 0x00100000,
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenProcess")]
    internal static extern IntPtr OPEN_PROCESS
    (
      PROCESS_ACCESS_RIGHTS DESIRED_ACCESS,
      bool INHERIT_HANDLE,
      int PROCESS_ID
    );
  }
}
