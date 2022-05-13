using System;
using System.Runtime.InteropServices;

namespace Zeyo.FFOTagKiller.Common
{
  internal static partial class WinAPI
  {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct UNICODE_STRING
    {
      public ushort LENGTH;
      public ushort MAXIMUM_LENGTH;
      public IntPtr BUFFER;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct GENERIC_MAPPING
    {
      public int GENERIC_READ;
      public int GENERIC_WRITE;
      public int GENERIC_EXECUTE;
      public int GENERIC_ALL;
    }
  }
}
