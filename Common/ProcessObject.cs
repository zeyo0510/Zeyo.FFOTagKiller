using System;

namespace Zeyo.FFOTagKiller.Common
{
  internal sealed class ProcessObject
  {
    internal ProcessObject(int pid, IntPtr handle, string type, string name)
    {
      this.processID    = pid;
      this.objectHandle = handle;
      this.objectType   = type;
      this.objectName   = name;
    }

    private int processID = -1;
    public int ProcessID
    {
      get
      {
        return this.processID;
      }
    }

    private IntPtr objectHandle = IntPtr.Zero;
    public IntPtr ObjectHandle
    {
      get
      {
        return this.objectHandle;
      }
    }

    private string objectType = "";
    public string ObjectType
    {
      get
      {
        return this.objectType;
      }
    }

    private string objectName = "";
    public string ObjectName
    {
      get
      {
        return this.objectName;
      }
    }

    public bool Kill()
    {
      bool retValue = false;
      /************************************************/
      IntPtr srcProcess = WinAPI.OPEN_PROCESS(WinAPI.PROCESS_ACCESS_RIGHTS.PROCESS_ALL, false, this.ProcessID);
      IntPtr srcHandle = this.ObjectHandle;
      /************************************************/
      IntPtr trgProcess = IntPtr.Zero;
      IntPtr trgHandle = IntPtr.Zero;
      /************************************************/
      retValue = WinAPI.DUPLICATE_HANDLE(srcProcess, srcHandle, trgProcess, out trgHandle, 0, false, WinAPI.DUPLICATE_HANDLE_OPTIONS.DUPLICATE_CLOSE_SOURCE);
      /************************************************/
      if (WinAPI.CLOSE_HANDLE(srcProcess))
      {
        
      }
      /************************************************/
      return retValue;
    }
  }
}
