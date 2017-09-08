using System;
using System.IO;
using System.Threading;

namespace CommonComponent
{
    public class ExceptionHandle
    {
        private static string NamePath;

        static ExceptionHandle()
        {
            ExceptionHandle.NamePath = "Thunder";
        }

        public ExceptionHandle()
        {
        }

        public static void Application_ThreadException(object a, ThreadExceptionEventArgs e)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folderPath = Path.Combine(folderPath, ExceptionHandle.NamePath);
            folderPath = Path.Combine(folderPath, "exception.log");
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(folderPath, true))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString("G"));
                    streamWriter.WriteLine("/********************************/");
                    for (Exception i = e.Exception; i != null; i = i.InnerException)
                    {
                        streamWriter.WriteLine(i.Message);
                        streamWriter.WriteLine(i.StackTrace);
                        streamWriter.WriteLine(i.Source);
                        streamWriter.WriteLine(i.TargetSite);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folderPath = Path.Combine(folderPath, ExceptionHandle.NamePath);
            folderPath = Path.Combine(folderPath, "crash.log");
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(folderPath, true))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString("G"));
                    streamWriter.WriteLine("/********************************/");
                    for (Exception i = e.ExceptionObject as Exception; i != null; i = i.InnerException)
                    {
                        streamWriter.WriteLine(i.Message);
                        streamWriter.WriteLine(i.StackTrace);
                        streamWriter.WriteLine(i.Source);
                        streamWriter.WriteLine(i.TargetSite);
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }

        public static void Exception_Log(Exception exp)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folderPath = Path.Combine(folderPath, ExceptionHandle.NamePath);
            folderPath = Path.Combine(folderPath, "CatchedException.log");
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(folderPath, true))
                {
                    streamWriter.WriteLine(DateTime.Now.ToString("G"));
                    streamWriter.WriteLine("/********************************/");
                    while (exp != null)
                    {
                        streamWriter.WriteLine(exp.Message);
                        streamWriter.WriteLine(exp.StackTrace);
                        streamWriter.WriteLine(exp.Source);
                        streamWriter.WriteLine(exp.TargetSite);
                        exp = exp.InnerException;
                    }
                }
            }
            catch (Exception exception)
            {
            }
        }
    }
}