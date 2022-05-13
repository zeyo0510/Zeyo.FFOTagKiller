using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Zeyo.FFOTagKiller.Common;

namespace Zeyo.FFOTagKiller.Dialogs
{
  partial class SettingsDialog
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
      this.configPropertyGrid = new PropertyGrid();
      this.okButton           = new Button();
      this.cancelButton       = new Button();
      /************************************************/
      // configPropertyGrid
      {
        this.configPropertyGrid.Name           = "configPropertyGrid";
        this.configPropertyGrid.Anchor         = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        this.configPropertyGrid.Location       = new Point(10, 10);
        this.configPropertyGrid.SelectedObject = Config.Default;
        this.configPropertyGrid.Size           = new Size(280, 255);
      }
      // okButton
      {
        this.okButton.Name         = "okButton";
        this.okButton.Anchor       = AnchorStyles.Right | AnchorStyles.Bottom;
        this.okButton.DialogResult = DialogResult.OK;
        this.okButton.Text         = "確定";
        this.okButton.Location     = new Point(138, 270);
        this.okButton.Size         = new Size(75, 20);
      }
      // cancelButton
      {
        this.cancelButton.Name         = "cancelButton";
        this.cancelButton.Anchor       = AnchorStyles.Right | AnchorStyles.Bottom;
        this.cancelButton.DialogResult = DialogResult.Cancel;
        this.cancelButton.Text         = "取消";
        this.cancelButton.Location     = new Point(216, 270);
        this.cancelButton.Size         = new Size(75, 20);
      }
      // SettingsDialog
      {
        base.Name            = "SettingsDialog";
        base.AcceptButton    = this.okButton;
        base.AutoScaleMode   = AutoScaleMode.None;
        base.CancelButton    = this.cancelButton;
        base.ClientSize      = new Size(300, 300);
        base.Font            = new Font(FontFamily.GenericMonospace, 8f);
        base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        base.ShowInTaskbar   = false;
        base.StartPosition   = FormStartPosition.CenterScreen;
        base.Text            = "設定";
        base.Controls.Add(this.configPropertyGrid);
        base.Controls.Add(this.okButton);
        base.Controls.Add(this.cancelButton);
      }
    }

    private PropertyGrid configPropertyGrid = null;
    private Button       okButton           = null;
    private Button       cancelButton       = null;
  }
}
