using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChickenSoftware.LauncherPoc
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (SingleProgramInstance singleProgramInstance = new SingleProgramInstance("THUNDER"))
            {
                if (!singleProgramInstance.IsSingleInstance)
                {
                    singleProgramInstance.RaiseOtherProcess();
                }
                else
                {
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
            }

        }
    }
}
