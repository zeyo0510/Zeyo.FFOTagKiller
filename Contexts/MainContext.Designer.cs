using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Zeyo.FFOTagKiller.Contexts
{
  partial class MainContext
  {
    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.components != null)
        {
          this.components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components                = new Container();
      this.appNotifyIcon             = new NotifyIcon(this.components);
      this.appContextMenuStrip       = new ContextMenuStrip(this.components);
      this.settingsToolStripMenuItem = new ToolStripMenuItem();
      this.exitToolStripMenuItem     = new ToolStripMenuItem();
      this.killTimer                 = new Timer(this.components);
      // appNotifyIcon
      {
        this.appNotifyIcon.ContextMenuStrip = this.appContextMenuStrip;
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Zeyo.FFOTagKiller.Resources.app.ico"))
        {
          this.appNotifyIcon.Icon = new Icon(stream);
        }
        this.appNotifyIcon.Text    = "Zeyo.FFOTagKiller";
        this.appNotifyIcon.Visible = true;
      }
      // appContextMenuStrip
      {
        this.appContextMenuStrip.Name       = "appContextMenuStrip";
        this.appContextMenuStrip.Font       = new Font(FontFamily.GenericMonospace, 8f);
        this.appContextMenuStrip.RenderMode = ToolStripRenderMode.System;
        this.appContextMenuStrip.Items.Add(this.settingsToolStripMenuItem);
        this.appContextMenuStrip.Items.Add(new ToolStripSeparator());
        this.appContextMenuStrip.Items.Add(this.exitToolStripMenuItem);
        this.appContextMenuStrip.Opening += this.appContextMenuStrip_Opening;
      }
      // settingsToolStripMenuItem
      {
        this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        this.settingsToolStripMenuItem.Text = "設定";
        this.settingsToolStripMenuItem.Click += this.settingsToolStripMenuItem_Click;
      }
      // exitToolStripMenuItem
      {
        this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        this.exitToolStripMenuItem.Text = "結束";
        this.exitToolStripMenuItem.Click += this.exitToolStripMenuItem_Click;
      }
      // killTimer
      {
        this.killTimer.Enabled  = true;
        this.killTimer.Interval = 15000;
        this.killTimer.Tick += this.killTimer_Tick;
      }
    }

    private NotifyIcon        appNotifyIcon             = null;
    private ContextMenuStrip  appContextMenuStrip       = null;
    private ToolStripMenuItem settingsToolStripMenuItem = null;
    private ToolStripMenuItem exitToolStripMenuItem     = null;
    private Timer             killTimer                 = null;
  }
}
