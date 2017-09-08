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
    partial class Form_Fidget
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.about.AboutClose = true;
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form_Fidget));
            this.pictureBox1 = new PictureBox();
            this.toolTip1 = new ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.panel1 = new Panel();
            this.pictureBox2 = new PictureBox();
            this.pictureBox3 = new PictureBox();
            this.USB = new UsbHidPort(this.components);
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.panel1.SuspendLayout();
            ((ISupportInitialize)this.pictureBox2).BeginInit();
            ((ISupportInitialize)this.pictureBox3).BeginInit();
            base.SuspendLayout();
            this.pictureBox1.Dock = DockStyle.Fill;
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(267, 575);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.timer1.Interval = 20;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(61, 4);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Enabled = false;
            this.panel1.Location = new Point(26, 290);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(215, 171);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            this.pictureBox2.Dock = DockStyle.Fill;
            this.pictureBox2.Location = new Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new Size(215, 171);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            this.pictureBox3.Location = new Point(241, 290);
            this.pictureBox3.Margin = new Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new Size(26, 171);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            this.USB.ProductId = 0;
            this.USB.SpecifiedDevice = null;
            this.USB.VendorId = 0;
            this.USB.OnSpecifiedDeviceRemoved += new EventHandler(this.USB_OnSpecifiedDeviceRemoved);
            this.USB.OnDataRecieved += new DataRecievedEventHandler(this.USB_OnDataRecieved);
            this.USB.OnSpecifiedDeviceArrived += new EventHandler(this.USB_OnSpecifiedDeviceArrived);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            //TODO
            //base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(267, 575);
            base.Controls.Add(this.pictureBox3);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.pictureBox1);
            //TODO
            //base.FormBorderStyle = FormBorderStyle.None;
            //TODO
            //base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Form_Fidget";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Missile Launcher";
            base.Load += new EventHandler(this.Form_Fidget_Load);
            base.KeyUp += new KeyEventHandler(this.Form_Fidget_KeyUp);
            base.FormClosing += new FormClosingEventHandler(this.Form_Fidget_FormClosing);
            base.KeyDown += new KeyEventHandler(this.Form_Fidget_KeyDown);
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.panel1.ResumeLayout(false);
            ((ISupportInitialize)this.pictureBox2).EndInit();
            ((ISupportInitialize)this.pictureBox3).EndInit();
            base.ResumeLayout(false);
        }

    }
}