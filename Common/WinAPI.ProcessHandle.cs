using System;
using System.Runtime.InteropServices;

namespace Zeyo.FFOTagKiller.Common
{
  partial class WinAPI
  {
    [Flags]
    internal enum DUPLICATE_HANDLE_OPTIONS : uint
    {
      DUPLICATE_CLOSE_SOURCE = 0x00000001,
      DUPLICATE_SAME_ACCESS = 0x00000002
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "DuplicateHandle")]
    internal static extern bool DUPLICATE_HANDLE
    (
      IntPtr SOURCE_PROCESS_HANDLE,
      IntPtr SOURCE_HANDLE,
      IntPtr TARGET_PROCESS_HANDLE,
      out IntPtr TARGET_HANDLE,
      uint DESIRED_ACCESS,
      bool INHERIT_HANDLE,
      DUPLICATE_HANDLE_OPTIONS OPTIONS
    );
  }
}
