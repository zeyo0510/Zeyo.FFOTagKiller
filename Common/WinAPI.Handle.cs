using System;
using System.Runtime.InteropServices;

namespace Zeyo.FFOTagKiller.Common
{
  partial class WinAPI
  {
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CloseHandle")]
    internal static extern bool CLOSE_HANDLE
    (
      IntPtr OBJECT
    );
  }
}
