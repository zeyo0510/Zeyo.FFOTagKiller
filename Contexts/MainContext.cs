using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Zeyo.FFOTagKiller.Common;
using Zeyo.FFOTagKiller.Core;
using Zeyo.FFOTagKiller.Dialogs;

namespace Zeyo.FFOTagKiller.Contexts
{
  internal partial class MainContext : ApplicationContext
  {
    public MainContext()
    {
      this.InitializeComponent();
    }

    private void appContextMenuStrip_Opening(object sender, CancelEventArgs e)
    {
      
    }

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (SettingsDialog dialog = new SettingsDialog())
      {
        switch (dialog.ShowDialog())
        {
          case DialogResult.OK:
          {
            if (Config.Default.Save())
            {
              if (MessageBox.Show("某些設定，需要重新啟動應用程式才會生效。是否繼續？", "重啟程式？", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
              {
                Application.Restart();
              }
            }
            else
            {
              if (MessageBox.Show(string.Format("無法讀寫 {0} 檔案。", Config.Default.FileName), "存取被拒。", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
              {
                // TODO: do-nothing.
              }
            }
            break;
          }
          case DialogResult.Cancel:
          {
            Config.Default.Reload();
            break;
          }
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void killTimer_Tick(object sender, EventArgs e)
    {
      foreach (Process proc in Process.GetProcessesByName(Path.GetFileName(Config.Default.Application)))
      {
        if (!proc.HasExited && proc.MainModule.FileName.Equals(Config.Default.Application))
        {
          FFOTag killer = new FFOTag(proc, Config.Default.Tag);
          if (killer.Kill())
          {
            this.appNotifyIcon.ShowBalloonTip(3000, "偵測到防止多開標籤。", string.Format("殺死了！應用程式({0})的標籤。", proc.Id), ToolTipIcon.Info);
          }
        }
      }
    }
  }
}
