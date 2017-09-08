using System;
using System.Reflection;
using System.Threading;

namespace ChickenSoftware.LauncherPoc
{
    public class SingleProgramInstance : IDisposable
    {
        public static uint WakeupMessage;

        private static IntPtr HWND_BROADCAST;

        private Mutex _processSync;

        private bool _owned;

        public bool IsSingleInstance
        {
            get
            {
                return this._owned;
            }
        }

        static SingleProgramInstance()
        {
            SingleProgramInstance.HWND_BROADCAST = (IntPtr)65535;
        }

        public SingleProgramInstance()
            : this("")
        {
        }

        public SingleProgramInstance(string identifier)
        {
            SingleProgramInstance.WakeupMessage = NativeMethods.RegisterWindowMessage("MyApplication_Wakeup");
            this._processSync = new Mutex(true, string.Concat(Assembly.GetExecutingAssembly().GetName().Name, identifier), out this._owned);
        }

        public void Dispose()
        {
            this.Release();
            GC.SuppressFinalize(this);
        }

        ~SingleProgramInstance()
        {
            this.Release();
        }

        public void RaiseOtherProcess()
        {
            NativeMethods.SendNotifyMessage(SingleProgramInstance.HWND_BROADCAST, SingleProgramInstance.WakeupMessage, IntPtr.Zero, IntPtr.Zero);
        }

        private void Release()
        {
            if (this._owned)
            {
                this._processSync.ReleaseMutex();
                this._owned = false;
            }
        }
    }
}