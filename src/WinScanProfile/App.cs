using System;
using System.Threading;
using System.Windows.Forms;

namespace WinScanProfile {
    internal static class App {

        private static readonly Mutex SetupMutex = new(false, @"Global\JosipMedved_WinScanProfile");

        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());

            SetupMutex.Close();
        }

    }
}
