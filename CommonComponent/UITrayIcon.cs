using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CommonComponent
{
    public class UITrayIcon
    {
        private Form myForm;

        public AboutBox myAboutBox;

        public NotifyIcon myNotifyIcon;

        public ContextMenuStrip myContextMenuStrip;

        private FormWindowState m_previousWindowState;

        public UITrayIcon(Form aForm, AboutBox aAboutBox, NotifyIcon aNotifyIcon, ContextMenuStrip aContextMenuStrip)
        {
            this.myForm = aForm;
            this.myAboutBox = aAboutBox;
            this.myNotifyIcon = aNotifyIcon;
            this.myContextMenuStrip = aContextMenuStrip;
            this.myForm.Resize += new EventHandler(this.Form_Resize);
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem size = new ToolStripMenuItem();
            ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
            ToolStripMenuItem size1 = new ToolStripMenuItem();
            ToolStripSeparator toolStripSeparator = new ToolStripSeparator();
            ToolStripSeparator toolStripSeparator1 = new ToolStripSeparator();
            this.myNotifyIcon.ContextMenuStrip = this.myContextMenuStrip;
            this.myNotifyIcon.Visible = true;
            this.myNotifyIcon.MouseDoubleClick += new MouseEventHandler(this.notifyIcon_tray_MouseDoubleClick);
            ToolStripItemCollection items = this.myContextMenuStrip.Items;
            ToolStripItem[] toolStripItemArray = new ToolStripItem[] { size, toolStripSeparator, toolStripMenuItem1, size1, toolStripSeparator1, toolStripMenuItem };
            items.AddRange(toolStripItemArray);
            this.myContextMenuStrip.Name = "contextMenuStrip_tray";
            this.myContextMenuStrip.Size = new Size(104, 104);
            size.Name = "toolStripMenuItem_Show";
            size.Size = new Size(103, 22);
            size.Text = "Open THUNDER";
            size.Click += new EventHandler(this.toolStripMenuItem_Show_Click);
            toolStripSeparator.Name = "toolStripSeparator2";
            toolStripSeparator.Size = new Size(100, 6);
            toolStripMenuItem1.Name = "toolStripMenuItem_Help";
            toolStripMenuItem1.Size = new Size(103, 22);
            toolStripMenuItem1.Text = "Help";
            toolStripMenuItem1.Click += new EventHandler(this.toolStripMenuItem_Help_Click);
            size1.Name = "toolStripMenuItem_About";
            size1.Size = new Size(103, 22);
            size1.Text = "About";
            size1.Click += new EventHandler(this.toolStripMenuItem_About_Click);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(100, 6);
            toolStripMenuItem.Name = "toolStripMenuItem_Close";
            toolStripMenuItem.Size = new Size(103, 22);
            toolStripMenuItem.Text = "Exit";
            toolStripMenuItem.Click += new EventHandler(this.toolStripMenuItem_Close_Click);
        }

        public void Form_Resize(object sender, EventArgs e)
        {
            if (this.myForm.WindowState != FormWindowState.Minimized)
            {
                this.m_previousWindowState = this.myForm.WindowState;
                this.myForm.Visible = true;
                return;
            }
            if (this.myForm.WindowState == FormWindowState.Minimized)
            {
                this.myForm.Visible = false;
                this.myForm.ShowInTaskbar = false;
            }
        }

        public void notifyIcon_tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.myForm.Visible = true;
            this.myForm.WindowState = this.m_previousWindowState;
            this.myForm.Activate();
        }

        private void toolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            this.myAboutBox.Show();
            this.myAboutBox.Focus();
        }

        public void toolStripMenuItem_Close_Click(object sender, EventArgs e)
        {
            this.myForm.Close();
        }

        public void toolStripMenuItem_Help_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://www.dreamcheeky.com/download-support");
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                MessageBox.Show(exception.Message);
                ExceptionHandle.Exception_Log(exception);
            }
        }

        public void toolStripMenuItem_Show_Click(object sender, EventArgs e)
        {
            this.myForm.Visible = true;
            this.myForm.WindowState = this.m_previousWindowState;
            this.myForm.Activate();
        }
    }
}