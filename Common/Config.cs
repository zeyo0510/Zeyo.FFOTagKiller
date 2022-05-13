using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms.Design;
using Zeyo.FFOTagKiller.Common;

namespace Zeyo.FFOTagKiller.Common
{
  internal class Config
  {
    public Config(string fileName)
    {
      if (!File.Exists(fileName))
      {
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Zeyo.FFOTagKiller.Resources.FFOTagKiller.ini"))
        {
          using (StreamReader reader = new StreamReader(stream))
          {
            File.WriteAllText(fileName, reader.ReadToEnd());
          }
        }
      }
      /************************************************/
      if ((this.fileName = Path.GetFullPath(fileName)) != "")
      {
        this.Reload();
      }
    }

    private static Config instance = null;
    public static Config Default
    {
      get
      {
        return  instance = instance
        ??      new Config(string.Format("{0}.ini", Process.GetCurrentProcess().ProcessName));
      }
    }

    private string fileName = "";
    [Category("#Profile")]
    public string FileName
    {
      get
      {
        return this.fileName;
      }
    }

    private string application = "";
    [Category("Game")]
    [Description("遊戲主程式路徑")]
    [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
    public string Application
    {
      get
      {
        return this.application;
      }
      set
      {
        this.application = value;
      }
    }

    private string tag = "";
    [Category("Multiclient")]
    [Description("多客戶端限制標籤")]
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

    public bool Save()
    {
      return  1==1
      &&      this.Write("Game       ".Trim(), "Application".Trim(), this.Application)
      &&      this.Write("Multiclient".Trim(), "Tag        ".Trim(), this.Tag)
      ;
    }

    public void Reload()
    {
      this.Application = this.Read("Game       ".Trim(), "Application".Trim(), "game.bin   ".Trim());
      this.Tag         = this.Read("Multiclient".Trim(), "Tag        ".Trim(), "FFClientTag".Trim());
    }

    private string Read(string section, string key, string defaultValue)
    {
      StringBuilder buffer = new StringBuilder(512);
      /************************************************/
      return  WinAPI.GET_PRIVATE_PROFILE_STRING(section, key, defaultValue, buffer, buffer.Capacity, this.FileName) == buffer.Length
      ?       buffer.ToString()
      :       "";
    }

    private bool Write(string section, string key, string value)
    {
      return WinAPI.WRITE_PRIVATE_PROFILE_STRING(section, key, value, this.FileName);
    }
  }
}
