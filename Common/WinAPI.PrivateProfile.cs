using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Zeyo.FFOTagKiller.Common
{
  partial class WinAPI
  {
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileString")]
    internal static extern int GET_PRIVATE_PROFILE_STRING
    (
      string APP_NAME,
      string KEY_NAME,
      string DEFAULT,
      StringBuilder RETURNED_STRING,
      int SIZE,
      string FILE_NAME
    );

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "WritePrivateProfileString")]
    internal static extern bool WRITE_PRIVATE_PROFILE_STRING
    (
      string APP_NAME,
      string KEY_NAME,
      string STRING,
      string FILE_NAME
    );
  }
}
