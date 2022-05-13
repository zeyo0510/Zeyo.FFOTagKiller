using System;
using System.Windows.Forms;
using Zeyo.FFOTagKiller.Contexts;

namespace Zeyo.FFOTagKiller
{
  internal sealed class App
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainContext());
    }
  }
}
