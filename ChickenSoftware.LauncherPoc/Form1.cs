using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsbLibrary;

namespace ChickenSoftware.LauncherPoc
{
    public partial class Form1 : Form
    {
        private byte[] CMD = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 2 };

        private byte[] UP0 = new byte[] { 0, 2 };

        private byte[] DOWN0 = new byte[] { 0, 1 };

        private byte[] LEFT0 = new byte[] { 0, 4 };

        private byte[] RIGHT0 = new byte[] { 0, 8 };

        private byte[] FIRE0 = new byte[] { 0, 16 };

        private byte[] GET_STATUS0 = new byte[] { 0, 64 };

        private byte[] STOP0 = new byte[] { 0, 32 };

        private byte[] UP1 = new byte[] { 0, 2, 2, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] DOWN1 = new byte[] { 0, 2, 1, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] LEFT1 = new byte[] { 0, 2, 4, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] RIGHT1 = new byte[] { 0, 2, 8, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] FIRE1 = new byte[] { 0, 2, 16, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] STOP1 = new byte[] { 0, 2, 32, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] GET_STATUS1 = new byte[] { 0, 1, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] LED_ON = new byte[] { 0, 3, 1, 0, 0, 0, 0, 0, 0 };

        private byte[] LED_OFF = new byte[] { 0, 3, 0, 0, 0, 0, 0, 0, 0 };

        private byte[] UP;

        private byte[] DOWN;

        private byte[] LEFT;

        private byte[] RIGHT;

        private byte[] FIRE;

        private byte[] STOP;

        private byte[] GET_STATUS;

        private int VersionProfile;
        private TimeSpan Vertical;
        private TimeSpan Horizon;
        private bool DevicePresent;
        private UsbHidPort USB;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                CreateParams style = createParams;
                style.Style = style.Style | 131072;
                CreateParams createParam = createParams;
                createParam.Style = createParam.Style & -12582913;
                return createParams;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void SetCMDProfile()
        {
            if (this.VersionProfile == 0)
            {
                this.UP = this.UP0;
                this.DOWN = this.DOWN0;
                this.LEFT = this.LEFT0;
                this.RIGHT = this.RIGHT0;
                this.FIRE = this.FIRE0;
                this.STOP = this.STOP0;
                this.GET_STATUS = this.GET_STATUS0;
                return;
            }
            if (this.VersionProfile == 1)
            {
                this.UP = this.UP1;
                this.DOWN = this.DOWN1;
                this.LEFT = this.LEFT1;
                this.RIGHT = this.RIGHT1;
                this.FIRE = this.FIRE1;
                this.STOP = this.STOP1;
                this.GET_STATUS = this.GET_STATUS1;
            }
        }

        private void CMDHandle(byte[] Data)
        {
            byte num;
            num = (this.VersionProfile != 1 ? Data[1] : Data[2]);
            //this.Current_Status = num;
            //if (this.Firing && (num & 16) == 16)
            //{
            //    this.DelayStop = true;
            //    this.timerCnt = 0;
            //}
            //if (this.Fired && (num & 16) == 0)
            //{
            //    Form_Fidget fireTimeCnt = this;
            //    fireTimeCnt.FireTimeCnt = fireTimeCnt.FireTimeCnt + 1;
            //    if (this.FireTimeCnt > 25)
            //    {
            //        this.myUI.GetElementByName("Fire").isDisplayFore = false;
            //        this.myUI.myPictureBox.Invalidate();
            //        this.Fired = false;
            //    }
            //}
            //if (this.ReSet)
            //{
            //    if (this.ReSet_Status == 0)
            //    {
            //        if ((num & 1) == 1)
            //        {
            //            this.ReSet_Status = 2;
            //        }
            //        else
            //        {
            //            this.SendUSBData(this.DOWN);
            //            this.CurrentAction = Form_Fidget.STATUS.DOWN_LIMIT;
            //            this.ReSet_Status = 1;
            //            this.ResetTimeout = 0;
            //        }
            //    }
            //    if (this.ReSet_Status == 1 && (num & 1) == 1)
            //    {
            //        this.ReSet_Status = 2;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 2)
            //    {
            //        if ((num & 4) == 4)
            //        {
            //            this.ReSet_Status = 4;
            //        }
            //        else
            //        {
            //            this.SendUSBData(this.LEFT);
            //            this.CurrentAction = Form_Fidget.STATUS.LEFT_LIMIT;
            //            this.ReSet_Status = 3;
            //            this.ResetTimeout = 0;
            //        }
            //    }
            //    if (this.ReSet_Status == 3 && (num & 4) == 4)
            //    {
            //        this.ReSet_Status = 4;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 4)
            //    {
            //        this.ReSet_Status = 5;
            //        this.SendUSBData(this.RIGHT);
            //        this.CurrentAction = Form_Fidget.STATUS.RIGHT_LIMIT;
            //        this.start = DateTime.Now;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 5 && (num & 8) == 8)
            //    {
            //        this.ReSet_Status = 6;
            //        this.Horizon = DateTime.Now.Subtract(this.start);
            //        this.Horizon = TimeSpan.FromMilliseconds(this.Horizon.TotalMilliseconds / 2 * 0.9);
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 6)
            //    {
            //        this.ReSet_Status = 7;
            //        this.SendUSBData(this.LEFT);
            //        this.CurrentAction = Form_Fidget.STATUS.LEFT_LIMIT;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 7 && (num & 4) == 4)
            //    {
            //        this.ReSet_Status = 8;
            //        this.start = DateTime.Now;
            //        this.SendUSBData(this.RIGHT);
            //        this.CurrentAction = Form_Fidget.STATUS.RIGHT_LIMIT;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 8)
            //    {
            //        this.stop = DateTime.Now.Subtract(this.start);
            //        if (this.stop > this.Horizon)
            //        {
            //            this.ReSet_Status = 9;
            //            this.SendUSBData(this.UP);
            //            this.CurrentAction = Form_Fidget.STATUS.UP_LIMIT;
            //            this.start = DateTime.Now;
            //            this.ResetTimeout = 0;
            //        }
            //    }
            //    if (this.ReSet_Status == 9 && (num & 2) == 2)
            //    {
            //        this.ReSet_Status = 10;
            //        this.Vertical = DateTime.Now.Subtract(this.start);
            //        this.Vertical = TimeSpan.FromMilliseconds(this.Vertical.TotalMilliseconds / 2);
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 10)
            //    {
            //        this.ReSet_Status = 11;
            //        this.SendUSBData(this.DOWN);
            //        this.CurrentAction = Form_Fidget.STATUS.DOWN_LIMIT;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 11 && (num & 1) == 1)
            //    {
            //        this.ReSet_Status = 12;
            //        this.start = DateTime.Now;
            //        this.SendUSBData(this.UP);
            //        this.CurrentAction = Form_Fidget.STATUS.UP_LIMIT;
            //        this.ResetTimeout = 0;
            //    }
            //    if (this.ReSet_Status == 12)
            //    {
            //        this.stop = DateTime.Now.Subtract(this.start);
            //        if (this.stop > this.Vertical)
            //        {
            //            this.SendUSBData(this.STOP);
            //            this.CurrentAction = Form_Fidget.STATUS.FIRED;
            //            this.ResetTimeout = 0;
            //            this.ReSet = false;
            //            this.ReSeted = true;
            //        }
            //    }
            //}
        }

        private void USB_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            this.CMDHandle(args.data);
        }
        
        private void USB_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            string str = (string)sender;
            if (base.InvokeRequired)
            {
                EventHandler eventHandler = new EventHandler(this.USB_OnSpecifiedDeviceArrived);
                object[] objArray = new object[] { sender, e };
                base.Invoke(eventHandler, objArray);
                return;
            }
            this.DevicePresent = true;
            if (this.USB.ProductId != 4112)
            {
                this.VersionProfile = 0;
                this.Vertical = TimeSpan.Parse("00:00:00.8720499");
                this.Horizon = TimeSpan.Parse("00:00:08.1919686");
            }
            else
            {
                this.VersionProfile = 1;
                this.SendUSBData(this.LED_ON);
                this.Vertical = TimeSpan.Parse("00:00:00.1959375");
                this.Horizon = TimeSpan.Parse("00:00:02.9640925");
            }
            this.SetCMDProfile();
        }

        private void USB_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            string str = (string)sender;
            if (!base.InvokeRequired)
            {
                this.DevicePresent = false;
                return;
            }
            EventHandler eventHandler = new EventHandler(this.USB_OnSpecifiedDeviceRemoved);
            object[] objArray = new object[] { sender, e };
            base.Invoke(eventHandler, objArray);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            this.SendUSBData(this.UP);
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            this.SendUSBData(this.FIRE);
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            this.SendUSBData(this.DOWN);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            this.SendUSBData(this.RIGHT);

        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            this.SendUSBData(this.LEFT);
        }

        private void SendUSBData(byte[] Data)
        {
            if (this.USB.SpecifiedDevice != null)
            {
                try
                {
                    this.USB.SpecifiedDevice.SendData(Data);
                }
                catch (Exception exception)
                {
                    Debug.Write(exception.ToString());
                    //Apparently do nothing?
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.USB.VID_List[0] = 2689;
            this.USB.PID_List[0] = 1793;
            this.USB.VID_List[1] = 8483;
            this.USB.PID_List[1] = 4112;
            this.USB.ID_List_Cnt = 2;
            this.USB.RegisterHandle(base.Handle);

        }

        protected override void WndProc(ref Message m)
        {
            this.USB.ParseMessages(ref m);
            if (m.Msg == SingleProgramInstance.WakeupMessage)
            {
                if (base.WindowState == FormWindowState.Minimized)
                {
                    base.Visible = true;
                    base.WindowState = FormWindowState.Normal;
                }
                base.Activate();
            }
            base.WndProc(ref m);
        }

    }
}
