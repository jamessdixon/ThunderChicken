using CommonComponent;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Media;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using UsbLibrary;

namespace Missile_Launcher
{
    public partial class Form_Fidget : Form
    {
        private string appPath;

        private string uiPath;

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

        private bool DebugMode;

        private bool Firing;

        private bool Fired;

        private bool nFire;

        private bool ReSet;

        private bool ReSeted;

        private bool Calibrate;

        private bool Action;

        private bool DelayStop;

        private bool SoundOn = true;

        private bool FireCheck;

        private int FireCnt;

        private int FireTimeCnt;

        private int FireTimeout;

        private int ResetTimeout;

        private int ReSet_Status;

        private DateTime start;

        private TimeSpan Vertical;

        private TimeSpan Horizon;

        private TimeSpan stop;

        private TimeSpan[] TestTime = new TimeSpan[30];

        private int TestTimeCnt;

        private Setting UserSetting;

        private UI myUI;

        private UITrayIcon myUITrayIcon;

        private AboutBox about;

        private Icon IconOff;

        private Icon IconActive;

        private byte Current_Status;

        private SoundPlayer Wavplayer;

        private string mask_picture;

        private Point CenterPoint;

        private Bitmap mask;

        private bool DevicePresent;

        private bool NoVideo = true;

        private int MoveHeight;

        private int MoveWidth;

        private Form_Fidget.STATUS CurrentAction;

        private int timerCnt;

        private Rectangle SrcRect;

        private Rectangle DesRect;

        private Bitmap FirePic0;

        private Bitmap FirePic1;

        private Bitmap FirePic2;

        private Bitmap FirePic3;

        private Bitmap Boom;

        private int VersionProfile;

        private bool UpPress;

        private bool DownPress;

        private int UpCnt;

        private int DownCnt;

        private Random Rand = new Random();

        private bool FireAniEn;

        private int SquareCnt;

        private int RedCnt;

        private int BlueCnt1;

        private int BlueCnt2;

        private int FireButtonCnt;

        private int RedRandCnt;

        private int BlueRandCnt1;

        private int BlueRandCnt2;

        private int SpeedCnt;

        private PictureBox pictureBox1;

        private ToolTip toolTip1;

        private System.Windows.Forms.Timer timer1;

        private NotifyIcon notifyIcon1;

        private ContextMenuStrip contextMenuStrip1;

        private UsbHidPort USB;

        public Panel panel1;

        private PictureBox pictureBox2;

        private PictureBox pictureBox3;

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

        public Form_Fidget(bool IsHide)
        {
            this.InitializeComponent();
            if (IsHide)
            {
                base.WindowState = FormWindowState.Minimized;
            }
        }

        private void ClearAni()
        {
            this.myUI.GetElementByName("Square1").isDisplayFore = true;
            this.myUI.GetElementByName("Square2").isDisplayFore = true;
            this.myUI.GetElementByName("Square3").isDisplayFore = true;
            this.myUI.GetElementByName("RedRect1").isDisplayFore = true;
            this.myUI.GetElementByName("BlueRect1").isDisplayFore = true;
            this.myUI.GetElementByName("BlueRect2").isDisplayFore = true;
            this.myUI.myPictureBox.Invalidate();
        }

        private void CMDHandle(byte[] Data)
        {
            byte num;
            num = (this.VersionProfile != 1 ? Data[1] : Data[2]);
            this.Current_Status = num;
            if (this.Firing && (num & 16) == 16)
            {
                this.DelayStop = true;
                this.timerCnt = 0;
            }
            if (this.Fired && (num & 16) == 0)
            {
                Form_Fidget fireTimeCnt = this;
                fireTimeCnt.FireTimeCnt = fireTimeCnt.FireTimeCnt + 1;
                if (this.FireTimeCnt > 25)
                {
                    this.myUI.GetElementByName("Fire").isDisplayFore = false;
                    this.myUI.myPictureBox.Invalidate();
                    this.Fired = false;
                }
            }
            if (this.ReSet)
            {
                if (this.ReSet_Status == 0)
                {
                    if ((num & 1) == 1)
                    {
                        this.ReSet_Status = 2;
                    }
                    else
                    {
                        this.SendUSBData(this.DOWN);
                        this.CurrentAction = Form_Fidget.STATUS.DOWN_LIMIT;
                        this.ReSet_Status = 1;
                        this.ResetTimeout = 0;
                    }
                }
                if (this.ReSet_Status == 1 && (num & 1) == 1)
                {
                    this.ReSet_Status = 2;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 2)
                {
                    if ((num & 4) == 4)
                    {
                        this.ReSet_Status = 4;
                    }
                    else
                    {
                        this.SendUSBData(this.LEFT);
                        this.CurrentAction = Form_Fidget.STATUS.LEFT_LIMIT;
                        this.ReSet_Status = 3;
                        this.ResetTimeout = 0;
                    }
                }
                if (this.ReSet_Status == 3 && (num & 4) == 4)
                {
                    this.ReSet_Status = 4;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 4)
                {
                    this.ReSet_Status = 5;
                    this.SendUSBData(this.RIGHT);
                    this.CurrentAction = Form_Fidget.STATUS.RIGHT_LIMIT;
                    this.start = DateTime.Now;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 5 && (num & 8) == 8)
                {
                    this.ReSet_Status = 6;
                    this.Horizon = DateTime.Now.Subtract(this.start);
                    this.Horizon = TimeSpan.FromMilliseconds(this.Horizon.TotalMilliseconds / 2 * 0.9);
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 6)
                {
                    this.ReSet_Status = 7;
                    this.SendUSBData(this.LEFT);
                    this.CurrentAction = Form_Fidget.STATUS.LEFT_LIMIT;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 7 && (num & 4) == 4)
                {
                    this.ReSet_Status = 8;
                    this.start = DateTime.Now;
                    this.SendUSBData(this.RIGHT);
                    this.CurrentAction = Form_Fidget.STATUS.RIGHT_LIMIT;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 8)
                {
                    this.stop = DateTime.Now.Subtract(this.start);
                    if (this.stop > this.Horizon)
                    {
                        this.ReSet_Status = 9;
                        this.SendUSBData(this.UP);
                        this.CurrentAction = Form_Fidget.STATUS.UP_LIMIT;
                        this.start = DateTime.Now;
                        this.ResetTimeout = 0;
                    }
                }
                if (this.ReSet_Status == 9 && (num & 2) == 2)
                {
                    this.ReSet_Status = 10;
                    this.Vertical = DateTime.Now.Subtract(this.start);
                    this.Vertical = TimeSpan.FromMilliseconds(this.Vertical.TotalMilliseconds / 2);
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 10)
                {
                    this.ReSet_Status = 11;
                    this.SendUSBData(this.DOWN);
                    this.CurrentAction = Form_Fidget.STATUS.DOWN_LIMIT;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 11 && (num & 1) == 1)
                {
                    this.ReSet_Status = 12;
                    this.start = DateTime.Now;
                    this.SendUSBData(this.UP);
                    this.CurrentAction = Form_Fidget.STATUS.UP_LIMIT;
                    this.ResetTimeout = 0;
                }
                if (this.ReSet_Status == 12)
                {
                    this.stop = DateTime.Now.Subtract(this.start);
                    if (this.stop > this.Vertical)
                    {
                        this.SendUSBData(this.STOP);
                        this.CurrentAction = Form_Fidget.STATUS.FIRED;
                        this.ResetTimeout = 0;
                        this.ReSet = false;
                        this.ReSeted = true;
                    }
                }
            }
        }

        private void Form_Fidget_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Firing || this.Fired || this.Action || this.ReSet)
            {
                this.SendUSBData(this.STOP);
            }
            if (this.VersionProfile == 1)
            {
                this.SendUSBData(this.LED_OFF);
            }
        }

        private void Form_Fidget_KeyDown(object sender, KeyEventArgs e)
        {
            this.KeyHandle(true, e);
        }

        private void Form_Fidget_KeyUp(object sender, KeyEventArgs e)
        {
            this.KeyHandle(false, e);
        }

        private void Form_Fidget_Load(object sender, EventArgs e)
        {
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandle.Application_ThreadException);
            if (File.Exists("debug"))
            {
                this.DebugMode = true;
            }
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folderPath = Path.Combine(folderPath, "Thunder");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            this.appPath = Application.StartupPath;
            this.uiPath = Path.Combine(this.appPath, "Skin");
            this.Wavplayer = new SoundPlayer();
            this.Vertical = TimeSpan.Parse("00:00:00.8720499");
            this.Horizon = TimeSpan.Parse("00:00:08.1919686");
            //this.about = new AboutBox();
            this.myUI = new UI(this, this.pictureBox1, this.toolTip1, this.uiPath, "Launcher.xml");
            this.myUITrayIcon = new UITrayIcon(this, this.about, this.notifyIcon1, this.contextMenuStrip1);
            UI.Element elementByName = this.myUI.GetElementByName("Fire");
            this.FirePic0 = new Bitmap(elementByName.ImageForeground);
            this.FirePic1 = new Bitmap(Path.Combine(this.uiPath, "fire1.png"));
            this.FirePic2 = new Bitmap(Path.Combine(this.uiPath, "fire2.png"));
            this.FirePic3 = new Bitmap(Path.Combine(this.uiPath, "fire3.png"));
            this.IconOff = new Icon(string.Concat(this.uiPath, "\\icon2.ico"));
            this.IconActive = new Icon(string.Concat(this.uiPath, "\\icon1.ico"));
            this.myUITrayIcon.myNotifyIcon.Text = "THUNDER";
            this.myUITrayIcon.myNotifyIcon.Icon = this.IconOff;
            this.myUI.AddEvent("Close", new UI.FunctionPointer(this.onClose));
            this.myUI.AddEvent("Minimize", new UI.FunctionPointer(this.onMinimize));
            this.myUI.AddEvent("Up", new UI.FunctionPointer(this.KeyHandle));
            this.myUI.AddEvent("Down", new UI.FunctionPointer(this.KeyHandle));
            this.myUI.AddEvent("Left", new UI.FunctionPointer(this.KeyHandle));
            this.myUI.AddEvent("Right", new UI.FunctionPointer(this.KeyHandle));
            this.myUI.AddEvent("Fire", new UI.FunctionPointer(this.KeyHandle));
            this.myUI.AddEvent("Exit", new UI.FunctionPointer(this.onClose));
            this.myUI.AddEvent("Sound", new UI.FunctionPointer(this.onSound));
            this.myUI.AddEvent("Reset", new UI.FunctionPointer(this.onReset));
            this.myUI.AddEvent("logo", new UI.FunctionPointer(this.onLogo));
            this.pictureBox1.MouseMove += new MouseEventHandler(this.myUI.MouseMove);
            this.pictureBox1.MouseDown += new MouseEventHandler(this.myUI.MouseDown);
            this.pictureBox1.MouseUp += new MouseEventHandler(this.myUI.MouseUp);
            this.pictureBox1.Paint += new PaintEventHandler(this.myUI.Paint);
            this.MoveHeight = 12;
            this.MoveWidth = 8;
            this.RedCnt = this.Rand.Next(0, 40);
            this.BlueCnt1 = this.Rand.Next(0, 40);
            this.BlueCnt2 = this.Rand.Next(0, 40);
            this.RedRandCnt = this.Rand.Next(40, 80);
            this.BlueRandCnt1 = this.Rand.Next(40, 80);
            this.BlueRandCnt2 = this.Rand.Next(40, 80);
            this.SpeedCnt = 5;
            this.USB.VID_List[0] = 2689;
            this.USB.PID_List[0] = 1793;
            this.USB.VID_List[1] = 8483;
            this.USB.PID_List[1] = 4112;
            this.USB.ID_List_Cnt = 2;
            this.USB.RegisterHandle(base.Handle);
            this.timer1.Start();
        }

        private void KeyHandle(string sender, string type)
        {
            if (this.ReSet || !this.DevicePresent)
            {
                if (sender == "Fire")
                {
                    this.myUI.GetElementByName("Fire").isDisplayFore = false;
                    this.myUI.myPictureBox.Invalidate();
                }
                return;
            }
            if (type == "MouseDown")
            {
                string str = sender;
                string str1 = str;
                if (str != null)
                {
                    if (str1 == "Up")
                    {
                        if ((this.Current_Status & 2) != 2 && !this.Firing && !this.Fired)
                        {
                            Thread.Sleep(150);
                            if (this.SoundOn)
                            {
                                this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                this.Wavplayer.PlayLooping();
                            }
                            this.SendUSBData(this.UP);
                            this.CurrentAction = Form_Fidget.STATUS.UP_LIMIT;
                        }
                        this.Action = true;
                        return;
                    }
                    if (str1 == "Down")
                    {
                        if ((this.Current_Status & 1) != 1 && !this.Firing && !this.Fired)
                        {
                            Thread.Sleep(150);
                            if (this.SoundOn)
                            {
                                this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                this.Wavplayer.PlayLooping();
                            }
                            this.SendUSBData(this.DOWN);
                            this.CurrentAction = Form_Fidget.STATUS.DOWN_LIMIT;
                        }
                        this.Action = true;
                        return;
                    }
                    if (str1 == "Left")
                    {
                        if ((this.Current_Status & 4) != 4 && !this.Firing && !this.Fired)
                        {
                            Thread.Sleep(150);
                            if (this.SoundOn)
                            {
                                this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                this.Wavplayer.PlayLooping();
                            }
                            this.SendUSBData(this.LEFT);
                            this.CurrentAction = Form_Fidget.STATUS.LEFT_LIMIT;
                        }
                        this.Action = true;
                        return;
                    }
                    if (str1 != "Right")
                    {
                        return;
                    }
                    if ((this.Current_Status & 8) != 8 && !this.Firing && !this.Fired)
                    {
                        Thread.Sleep(150);
                        if (this.SoundOn)
                        {
                            this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                            this.Wavplayer.PlayLooping();
                        }
                        this.SendUSBData(this.RIGHT);
                        this.CurrentAction = Form_Fidget.STATUS.RIGHT_LIMIT;
                    }
                    this.Action = true;
                    return;
                }
            }
            else if (type == "MouseUp")
            {
                this.Action = false;
                if (sender == "Fire" && !this.Firing && !this.Fired && !this.nFire)
                {
                    if (this.SoundOn)
                    {
                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "firing.wav");
                        this.Wavplayer.PlayLooping();
                    }
                    this.Firing = true;
                    this.FireCheck = false;
                    this.FireTimeout = 0;
                    this.SendUSBData(this.FIRE);
                    this.FireCnt = 0;
                    this.FireAniEn = true;
                    this.SpeedCnt = 2;
                    this.myUI.GetElementByName("Fire").isDisplayFore = true;
                    this.myUI.myPictureBox.Invalidate();
                }
                if (!this.Firing && !this.nFire && !this.ReSet && !this.Calibrate)
                {
                    if (this.SoundOn)
                    {
                        this.Wavplayer.Stop();
                    }
                    this.SendUSBData(this.STOP);
                    this.CurrentAction = Form_Fidget.STATUS.IDLE;
                    return;
                }
            }
            else if (type == "MouseEnter" && sender == "Fire")
            {
                if (!this.Firing)
                {
                    this.FireAniEn = true;
                    this.SpeedCnt = 5;
                    return;
                }
            }
            else if (type == "MouseLeave" && sender == "Fire" && !this.Firing)
            {
                this.FireAniEn = false;
                this.FireButtonCnt = 1000;
                this.SpeedCnt = 5;
            }
        }

        private void KeyHandle(bool IsDown, KeyEventArgs e)
        {
            UI.Element elementByName = null;
            if (!IsDown)
            {
                this.Action = false;
                this.CurrentAction = Form_Fidget.STATUS.IDLE;
                if (!this.Firing && !this.nFire && !this.ReSet && !this.Calibrate)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Left:
                            {
                                elementByName = this.myUI.GetElementByName("Left");
                                break;
                            }
                        case Keys.Up:
                            {
                                this.UpPress = false;
                                elementByName = this.myUI.GetElementByName("Up");
                                break;
                            }
                        case Keys.Right:
                            {
                                elementByName = this.myUI.GetElementByName("Right");
                                break;
                            }
                        case Keys.Down:
                            {
                                this.DownPress = false;
                                elementByName = this.myUI.GetElementByName("Down");
                                break;
                            }
                    }
                    if (elementByName != null)
                    {
                        if (this.SoundOn)
                        {
                            this.Wavplayer.Stop();
                        }
                        elementByName.isDisplayFore = false;
                        this.myUI.myPictureBox.Invalidate();
                    }
                    this.SendUSBData(this.STOP);
                    DateTime now = DateTime.Now;
                    this.TestTime[this.TestTimeCnt] = now.Subtract(this.start);
                    Form_Fidget testTimeCnt = this;
                    testTimeCnt.TestTimeCnt = testTimeCnt.TestTimeCnt + 1;
                    if (this.TestTimeCnt >= 10)
                    {
                        this.TestTimeCnt = 0;
                    }
                }
            }
            else
            {
                Keys keyCode = e.KeyCode;
                if (keyCode != Keys.Return)
                {
                    switch (keyCode)
                    {
                        case Keys.Space:
                            {
                                if (!this.Firing && !this.Fired && !this.nFire && !this.ReSet && this.DevicePresent)
                                {
                                    if (this.SoundOn)
                                    {
                                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "firing.wav");
                                        this.Wavplayer.PlayLooping();
                                    }
                                    elementByName = this.myUI.GetElementByName("Fire");
                                    this.Firing = true;
                                    this.FireCheck = false;
                                    this.FireTimeout = 0;
                                    this.SendUSBData(this.FIRE);
                                    this.FireCnt = 0;
                                    this.FireAniEn = true;
                                    this.SpeedCnt = 2;
                                }
                                this.Action = true;
                                break;
                            }
                        case Keys.Left:
                            {
                                if ((this.Current_Status & 4) != 4 && !this.Firing && !this.Fired && !this.Action && !this.ReSet && this.DevicePresent)
                                {
                                    Thread.Sleep(100);
                                    this.Action = true;
                                    elementByName = this.myUI.GetElementByName("Left");
                                    if (this.SoundOn)
                                    {
                                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                        this.Wavplayer.PlayLooping();
                                    }
                                    this.SendUSBData(this.LEFT);
                                    this.CurrentAction = Form_Fidget.STATUS.LEFT_LIMIT;
                                }
                                this.Action = true;
                                break;
                            }
                        case Keys.Up:
                            {
                                if ((this.Current_Status & 2) != 2 && !this.Firing && !this.Fired && !this.Action && !this.ReSet && this.DevicePresent)
                                {
                                    this.UpPress = true;
                                    this.UpCnt = 0;
                                    this.Action = true;
                                    elementByName = this.myUI.GetElementByName("Up");
                                    if (this.SoundOn)
                                    {
                                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                        this.Wavplayer.PlayLooping();
                                    }
                                    this.SendUSBData(this.UP);
                                    Thread.Sleep(40);
                                    this.SendUSBData(this.STOP);
                                    this.start = DateTime.Now;
                                    this.CurrentAction = Form_Fidget.STATUS.UP_LIMIT;
                                }
                                this.Action = true;
                                break;
                            }
                        case Keys.Right:
                            {
                                if ((this.Current_Status & 8) != 8 && !this.Firing && !this.Fired && !this.Action && !this.ReSet && this.DevicePresent)
                                {
                                    Thread.Sleep(100);
                                    this.Action = true;
                                    elementByName = this.myUI.GetElementByName("Right");
                                    if (this.SoundOn)
                                    {
                                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                        this.Wavplayer.PlayLooping();
                                    }
                                    this.SendUSBData(this.RIGHT);
                                    this.CurrentAction = Form_Fidget.STATUS.RIGHT_LIMIT;
                                }
                                this.Action = true;
                                break;
                            }
                        case Keys.Down:
                            {
                                if ((this.Current_Status & 1) != 1 && !this.Firing && !this.Fired && !this.Action && !this.ReSet && this.DevicePresent)
                                {
                                    this.DownPress = true;
                                    this.DownCnt = 0;
                                    this.Action = true;
                                    elementByName = this.myUI.GetElementByName("Down");
                                    if (this.SoundOn)
                                    {
                                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "move.wav");
                                        this.Wavplayer.PlayLooping();
                                    }
                                    this.SendUSBData(this.DOWN);
                                    Thread.Sleep(40);
                                    this.SendUSBData(this.STOP);
                                    this.start = DateTime.Now;
                                    this.CurrentAction = Form_Fidget.STATUS.DOWN_LIMIT;
                                }
                                this.Action = true;
                                break;
                            }
                    }
                }
                if (elementByName != null)
                {
                    elementByName.isDisplayFore = true;
                    this.myUI.myPictureBox.Invalidate();
                    return;
                }
            }
        }

        public void onClose(string sender, string type)
        {
            if (type == "MouseUp")
            {
                base.Close();
            }
        }

        public void onLogo(string sender, string type)
        {
            if (type != "MouseUp")
            {
                if (type == "MouseEnter")
                {
                    this.Cursor = Cursors.Hand;
                    return;
                }
                if (type == "MouseLeave")
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                try
                {
                    Process.Start("http://www.dreamcheeky.com");
                }
                catch (Exception exception)
                {
                }
            }
        }

        public void onMinimize(string sender, string type)
        {
            if (type == "MouseUp")
            {
                base.Visible = false;
            }
        }

        public void onReset(string sender, string type)
        {
            if (type == "MouseUp")
            {
                if (!this.ReSet && !this.Firing && !this.Action)
                {
                    this.ReSet = true;
                    this.ResetTimeout = 0;
                    this.ReSet_Status = 0;
                    this.myUI.GetElementByName("Reset").isDisplayFore = true;
                    this.myUI.myPictureBox.Invalidate();
                    if (this.SoundOn)
                    {
                        this.Wavplayer.Stop();
                        this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "reset.wav");
                        this.Wavplayer.Play();
                        return;
                    }
                }
                else if (this.ReSet)
                {
                    this.SendUSBData(this.STOP);
                    this.ReSet = false;
                    this.ReSeted = false;
                    this.myUI.GetElementByName("Reset").isDisplayFore = false;
                    this.myUI.myPictureBox.Invalidate();
                }
            }
        }

        public void onSound(string sender, string type)
        {
            if (type == "MouseUp")
            {
                this.SoundOn = !this.SoundOn;
                UI.Element elementByName = this.myUI.GetElementByName("Sound");
                elementByName.isDisplayFore = !elementByName.isDisplayFore;
                this.myUI.myPictureBox.Invalidate();
            }
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
                }
            }
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

        private void ThunderAnimation()
        {
            UI.Element elementByName = null;
            Form_Fidget squareCnt = this;
            squareCnt.SquareCnt = squareCnt.SquareCnt + 1;
            Form_Fidget redCnt = this;
            redCnt.RedCnt = redCnt.RedCnt + 1;
            Form_Fidget blueCnt1 = this;
            blueCnt1.BlueCnt1 = blueCnt1.BlueCnt1 + 1;
            Form_Fidget blueCnt2 = this;
            blueCnt2.BlueCnt2 = blueCnt2.BlueCnt2 + 1;
            if (this.FireAniEn)
            {
                Form_Fidget fireButtonCnt = this;
                fireButtonCnt.FireButtonCnt = fireButtonCnt.FireButtonCnt + 1;
            }
            if (this.SquareCnt == 8)
            {
                elementByName = this.myUI.GetElementByName("Square1");
                elementByName.isDisplayFore = false;
                elementByName = this.myUI.GetElementByName("Square2");
                elementByName.isDisplayFore = false;
                elementByName = this.myUI.GetElementByName("Square3");
                elementByName.isDisplayFore = false;
            }
            else if (this.SquareCnt == 16)
            {
                elementByName = this.myUI.GetElementByName("Square1");
                elementByName.isDisplayFore = false;
                elementByName = this.myUI.GetElementByName("Square2");
                elementByName.isDisplayFore = false;
                elementByName = this.myUI.GetElementByName("Square3");
                elementByName.isDisplayFore = true;
            }
            else if (this.SquareCnt == 24)
            {
                elementByName = this.myUI.GetElementByName("Square1");
                elementByName.isDisplayFore = false;
                elementByName = this.myUI.GetElementByName("Square2");
                elementByName.isDisplayFore = true;
                elementByName = this.myUI.GetElementByName("Square3");
                elementByName.isDisplayFore = true;
            }
            else if (this.SquareCnt >= 32)
            {
                elementByName = this.myUI.GetElementByName("Square1");
                elementByName.isDisplayFore = true;
                elementByName = this.myUI.GetElementByName("Square2");
                elementByName.isDisplayFore = true;
                elementByName = this.myUI.GetElementByName("Square3");
                elementByName.isDisplayFore = true;
                this.SquareCnt = 0;
            }
            if (this.FireButtonCnt == this.SpeedCnt)
            {
                elementByName = this.myUI.GetElementByName("Fire");
                elementByName.ImageForeground = this.FirePic1;
                elementByName.isDisplayFore = true;
            }
            else if (this.FireButtonCnt == this.SpeedCnt * 2)
            {
                elementByName = this.myUI.GetElementByName("Fire");
                elementByName.ImageForeground = this.FirePic2;
                elementByName.isDisplayFore = true;
            }
            else if (this.FireButtonCnt == this.SpeedCnt * 3)
            {
                elementByName = this.myUI.GetElementByName("Fire");
                elementByName.ImageForeground = this.FirePic3;
                elementByName.isDisplayFore = true;
            }
            else if (this.FireButtonCnt >= this.SpeedCnt * 4)
            {
                elementByName = this.myUI.GetElementByName("Fire");
                elementByName.ImageForeground = this.FirePic0;
                elementByName.isDisplayFore = false;
                this.FireButtonCnt = 0;
            }
            if (this.RedCnt > this.RedRandCnt)
            {
                elementByName = this.myUI.GetElementByName("RedRect1");
                elementByName.isDisplayFore = !elementByName.isDisplayFore;
                this.RedCnt = 0;
                if (elementByName.isDisplayFore)
                {
                    this.RedRandCnt = this.Rand.Next(20, 50);
                }
                else
                {
                    this.RedRandCnt = this.Rand.Next(10, 20);
                }
            }
            if (this.BlueCnt1 > this.BlueRandCnt1)
            {
                elementByName = this.myUI.GetElementByName("BlueRect1");
                elementByName.isDisplayFore = !elementByName.isDisplayFore;
                this.BlueCnt1 = 0;
                if (elementByName.isDisplayFore)
                {
                    this.BlueRandCnt1 = this.Rand.Next(30, 70);
                }
                else
                {
                    this.BlueRandCnt1 = this.Rand.Next(5, 20);
                }
            }
            if (this.BlueCnt2 > this.BlueRandCnt2)
            {
                elementByName = this.myUI.GetElementByName("BlueRect2");
                elementByName.isDisplayFore = !elementByName.isDisplayFore;
                this.BlueCnt2 = 0;
                if (elementByName.isDisplayFore)
                {
                    this.BlueRandCnt2 = this.Rand.Next(50, 120);
                }
                else
                {
                    this.BlueRandCnt2 = this.Rand.Next(5, 10);
                }
            }
            this.myUI.myPictureBox.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point position = Cursor.Position;
            position.X = position.X - base.Left;
            position.Y = position.Y - (base.Top - 20);
            this.myUI.ShowToolTip(position);
            Form_Fidget formFidget = this;
            formFidget.timerCnt = formFidget.timerCnt + 1;
            if (this.timerCnt > 1)
            {
                this.timerCnt = 0;
                if (this.DelayStop)
                {
                    this.DelayStop = false;
                    this.SendUSBData(this.STOP);
                    this.FireTimeCnt = 0;
                    this.Firing = false;
                    this.Fired = true;
                    this.FireCheck = false;
                    if (this.VersionProfile == 0)
                    {
                        if (this.SoundOn)
                        {
                            this.Wavplayer.Stop();
                            this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "fired.wav");
                            this.Wavplayer.Play();
                        }
                        this.FireAniEn = false;
                        this.FireButtonCnt = 1000;
                    }
                    else if (this.VersionProfile == 1 && this.SoundOn && (this.FireTimeout < 30 || this.FireTimeout > 125))
                    {
                        this.Wavplayer.Stop();
                    }
                }
                if (this.Firing)
                {
                    Form_Fidget fireTimeout = this;
                    fireTimeout.FireTimeout = fireTimeout.FireTimeout + 1;
                    if (this.FireTimeout < 15 && this.FireTimeout % 2 == 0)
                    {
                        this.SendUSBData(this.FIRE);
                    }
                    if (this.FireTimeout == 33 && this.VersionProfile == 1)
                    {
                        if (this.SoundOn)
                        {
                            this.Wavplayer.Stop();
                            this.Wavplayer.SoundLocation = Path.Combine(this.uiPath, "fired.wav");
                            this.Wavplayer.Play();
                        }
                        this.FireAniEn = false;
                        this.FireButtonCnt = 1000;
                    }
                    if (this.FireTimeout > 125)
                    {
                        this.DelayStop = true;
                    }
                }
                if (this.ReSet)
                {
                    Form_Fidget resetTimeout = this;
                    resetTimeout.ResetTimeout = resetTimeout.ResetTimeout + 1;
                    if (this.ResetTimeout > 1500)
                    {
                        this.CurrentAction = Form_Fidget.STATUS.FIRED;
                        this.ReSet = false;
                        this.ReSeted = true;
                    }
                }
                if (this.ReSeted)
                {
                    this.ReSeted = false;
                    this.myUI.GetElementByName("Reset").isDisplayFore = false;
                    this.myUI.myPictureBox.Invalidate();
                }
                if (this.Firing || this.Fired || this.Action || this.ReSet)
                {
                    this.SendUSBData(this.GET_STATUS);
                }
            }
            if (!this.Action)
            {
                bool reSet = this.ReSet;
            }
            bool firing = this.Firing;
            if (this.UpPress)
            {
                Form_Fidget upCnt = this;
                upCnt.UpCnt = upCnt.UpCnt + 1;
                if (this.UpCnt == 8)
                {
                    this.SendUSBData(this.UP);
                }
            }
            if (this.DownPress)
            {
                Form_Fidget downCnt = this;
                downCnt.DownCnt = downCnt.DownCnt + 1;
                if (this.DownCnt == 8)
                {
                    this.SendUSBData(this.DOWN);
                }
            }
            this.ThunderAnimation();
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
            this.myUITrayIcon.myNotifyIcon.Icon = this.IconActive;
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
                this.myUITrayIcon.myNotifyIcon.Icon = this.IconOff;
                return;
            }
            EventHandler eventHandler = new EventHandler(this.USB_OnSpecifiedDeviceRemoved);
            object[] objArray = new object[] { sender, e };
            base.Invoke(eventHandler, objArray);
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

        private enum STATUS
        {
            IDLE = 0,
            DOWN_LIMIT = 1,
            UP_LIMIT = 2,
            LEFT_LIMIT = 4,
            RIGHT_LIMIT = 8,
            FIRED = 16
        }
    }
}