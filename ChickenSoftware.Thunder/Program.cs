using CommonComponent;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Missile_Launcher
{
    internal static class Program
    {
        private static Mutex onlyOne;

        [STAThread]
        private static void Main()
        {
            using (SingleProgramInstance singleProgramInstance = new SingleProgramInstance("THUNDER"))
            {
                if (!singleProgramInstance.IsSingleInstance)
                {
                    singleProgramInstance.RaiseOtherProcess();
                }
                else
                {
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandle.CurrentDomain_UnhandledException);
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    string[] commandLineArgs = Environment.GetCommandLineArgs();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    if ((int)commandLineArgs.Length != 2 || !(commandLineArgs[1] == "/hide"))
                    {
                        Application.Run(new Form_Fidget(false));
                    }
                    else
                    {
                        Application.Run(new Form_Fidget(true));
                    }
                }
            }
        }
    }
}
