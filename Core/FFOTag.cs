using System;
using System.Diagnostics;
using Zeyo.FFOTagKiller.Common;

namespace Zeyo.FFOTagKiller.Core
{
  public class FFOTag
  {
    private ProcessObjectManager pom = null;
    private ProcessObject        po  = null;

    private FFOTag(string tag)
    {
      this.Tag = tag;
    }

    public FFOTag(Process process, string tag) : this(tag)
    {
      this.pom = new ProcessObjectManager(process);
      this.po  = null;
    }

    private string tag = "";
    public string Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        this.tag = value;
      }
    }

    public bool Exists
    {
      get
      {
        foreach (ProcessObject item in this.pom.Items)
        {
          if (item.ObjectType.Equals("Mutant") && item.ObjectName.Contains(this.Tag))
          {
            return (this.po = item) != null;
          }
        }
        return false;
      }
    }

    public bool Kill()
    {
      return  1==1
      &&      this.Exists
      &&      this.po.Kill()
      ;
    }
  }
}
