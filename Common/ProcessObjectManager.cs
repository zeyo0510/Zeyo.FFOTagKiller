using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Zeyo.FFOTagKiller.Common;

namespace Zeyo.FFOTagKiller.Common
{
  internal sealed class ProcessObjectManager
  {
    private Process process = null;

    public ProcessObjectManager(Process process)
    {
      this.process = process;
    }

    public ProcessObject[] Items
    {
      get
      {
        return this.GetHandles(this.process);
      }
    }

    private ProcessObject[] GetHandles(Process process)
    {
      List<ProcessObject> retValue = new List<ProcessObject>();
      /************************************************/
      int size = 0;
      /************************************************/
      WinAPI.NT_STATUS result;
      /************************************************/
      IntPtr forkProcess = WinAPI.OPEN_PROCESS(WinAPI.PROCESS_ACCESS_RIGHTS.PROCESS_DUP_HANDLE, false, process.Id);
      /************************************************/
      do
      {
        IntPtr tmpHandleBaseAddress = Marshal.AllocHGlobal(size);
        if ((result = WinAPI.NT_QUERY_SYSTEM_INFORMATION(WinAPI.SYSTEM_INFORMATION_CLASS.SYSTEM_HANDLE_INFORMATION, tmpHandleBaseAddress, size, out size)) ==  WinAPI.NT_STATUS.STATUS_SUCCESS)
        {
          int handleCount = Marshal.ReadInt32(tmpHandleBaseAddress);
          for (int i = 0; i < handleCount; i++)
          {
            int handleOffset = (IntPtr.Size + (i * Marshal.SizeOf(typeof(WinAPI.SYSTEM_HANDLE_INFORMATION))));
            IntPtr handleAddress = new IntPtr(tmpHandleBaseAddress.ToInt64() + handleOffset);
            WinAPI.SYSTEM_HANDLE_INFORMATION info = (WinAPI.SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(handleAddress, typeof(WinAPI.SYSTEM_HANDLE_INFORMATION));
            if (info.OWNER_PID == process.Id)
            {
              ProcessObject item = GetObject(forkProcess, info);
              if (item != null)
              {
                retValue.Add(item);
              }
            }
          }
        }
        Marshal.FreeHGlobal(tmpHandleBaseAddress);
      } while (result == WinAPI.NT_STATUS.STATUS_INFO_LENGTH_MISMATCH);
      /************************************************/
      if (WinAPI.CLOSE_HANDLE(forkProcess))
      {
        
      }
      /************************************************/
      return retValue.ToArray();
    }

    private ProcessObject GetObject(IntPtr forkProcess, WinAPI.SYSTEM_HANDLE_INFORMATION handleInfo)
    {
      ProcessObject retValue = null;
      /************************************************/
      int size = Marshal.SizeOf(typeof(WinAPI.OBJECT_BASIC_INFORMATION));
      /************************************************/
      WinAPI.NT_STATUS result;
      /************************************************/
      IntPtr forkHandle = new IntPtr(handleInfo.HANDLE_VALUE);
      /************************************************/
      if (WinAPI.DUPLICATE_HANDLE(forkProcess, forkHandle, Process.GetCurrentProcess().Handle, out forkHandle, 0, false, WinAPI.DUPLICATE_HANDLE_OPTIONS.DUPLICATE_SAME_ACCESS))
      {
        do
        {
          IntPtr tmpObjectAddress = Marshal.AllocHGlobal(size);
          if ((result = WinAPI.NT_QUERY_OBJECT(forkHandle, WinAPI.OBJECT_INFORMATION_CLASS.OBJECT_BASIC_INFORMATION, tmpObjectAddress, size, out size)) == WinAPI.NT_STATUS.STATUS_SUCCESS)
          {
            WinAPI.OBJECT_BASIC_INFORMATION info = (WinAPI.OBJECT_BASIC_INFORMATION)Marshal.PtrToStructure(tmpObjectAddress, typeof(WinAPI.OBJECT_BASIC_INFORMATION));
            string type = this.GetType(forkHandle, info);
            string name = this.GetName(forkHandle, info);
            retValue = new ProcessObject(handleInfo.OWNER_PID, new IntPtr(handleInfo.HANDLE_VALUE), type, name);
          }
          Marshal.FreeHGlobal(tmpObjectAddress);
        } while (result == WinAPI.NT_STATUS.STATUS_INFO_LENGTH_MISMATCH);
      }
      /************************************************/
      if (WinAPI.CLOSE_HANDLE(forkHandle))
      {
        
      }
      /************************************************/
      return retValue;
    }

    private string GetType(IntPtr forkHandle, WinAPI.OBJECT_BASIC_INFORMATION basicInfo)
    {
      string retValue = "";
      /************************************************/
      int size = (int)basicInfo.TYPE_INFORMATION_LENGTH;
      /************************************************/
      WinAPI.NT_STATUS result;
      /************************************************/
      do
      {
        IntPtr tmpTypeAddress = Marshal.AllocHGlobal(size);
        if ((result = WinAPI.NT_QUERY_OBJECT(forkHandle, WinAPI.OBJECT_INFORMATION_CLASS.OBJECT_TYPE_INFORMATION, tmpTypeAddress, size, out size)) == WinAPI.NT_STATUS.STATUS_SUCCESS)
        {
          WinAPI.OBJECT_TYPE_INFORMATION info = (WinAPI.OBJECT_TYPE_INFORMATION)Marshal.PtrToStructure(tmpTypeAddress, typeof(WinAPI.OBJECT_TYPE_INFORMATION));
          if (info.NAME.BUFFER != IntPtr.Zero)
          {
            retValue = Marshal.PtrToStringUni(new IntPtr(info.NAME.BUFFER.ToInt64()));
          }
        }
        Marshal.FreeHGlobal(tmpTypeAddress);
      } while (result == WinAPI.NT_STATUS.STATUS_INFO_LENGTH_MISMATCH);
      /************************************************/
      return retValue;
    }

    private string GetName(IntPtr forkHandle, WinAPI.OBJECT_BASIC_INFORMATION basicInfo)
    {
      string retValue = "";
      /************************************************/
      int size = (int)basicInfo.NAME_INFORMATION_LENGTH;
      /************************************************/
      WinAPI.NT_STATUS result;
      /************************************************/
      do
      {
        IntPtr tmpNameAddress = Marshal.AllocHGlobal(size);
        if ((result = WinAPI.NT_QUERY_OBJECT(forkHandle, WinAPI.OBJECT_INFORMATION_CLASS.OBJECT_NAME_INFORMATION, tmpNameAddress, size, out size)) == WinAPI.NT_STATUS.STATUS_SUCCESS)
        {
          WinAPI.OBJECT_NAME_INFORMATION info = (WinAPI.OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(tmpNameAddress, typeof(WinAPI.OBJECT_NAME_INFORMATION));
          if (info.NAME.BUFFER != IntPtr.Zero)
          {
            retValue = Marshal.PtrToStringUni(new IntPtr(info.NAME.BUFFER.ToInt64()));
          }
        }
        Marshal.FreeHGlobal(tmpNameAddress);
      } while (result == WinAPI.NT_STATUS.STATUS_INFO_LENGTH_MISMATCH);
      /************************************************/
      return retValue;
    }
  }
}
