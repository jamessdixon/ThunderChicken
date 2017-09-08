using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace CommonComponent
{
    public partial class AboutBox : Form
    {
        public string WebLink = "http://www.dreamcheeky.com/";

        public int ItemCnt = 2;

        public bool AboutClose;

        public Rectangle[] ItemRect = new Rectangle[2];

        private bool[] ItemEnter = new bool[2];

        private Label label_ProgramName;

        private Label label_ProgramVersion;

        private Label label_CopyRight;

        private Label label_TM;

        public AboutBox()
        {
            this.InitializeComponent();
            this.ItemRect[0].Location = new Point(6, 18);
            this.ItemRect[0].Size = new Size(335, 51);
            this.ItemRect[1].Location = new Point(120, 225);
            this.ItemRect[1].Size = new Size(108, 12);
            this.Text = string.Concat("About THUNDER", '\u2122');
            this.label_ProgramName.Text = "THUNDER";
            this.label_TM.Text = string.Concat("", '\u2122');
            this.label_ProgramVersion.Text = "v1.0 Build 2";
            this.label_CopyRight.Text = "Copyright(c) 2010 Dream Cheeky Ltd. All rights reserved.";
            Point location = this.label_ProgramName.Location;
            location.X = (base.Width - this.label_ProgramName.Width) / 2;
            this.label_ProgramName.Location = location;
            location = this.label_ProgramVersion.Location;
            location.X = (base.Width - this.label_ProgramVersion.Width) / 2;
            this.label_ProgramVersion.Location = location;
            location = this.label_TM.Location;
            Point point = this.label_ProgramName.Location;
            location.X = point.X + this.label_ProgramName.Width - 12;
            this.label_TM.Location = location;
            location = this.label_CopyRight.Location;
            location.X = (base.Width - this.label_CopyRight.Width) / 2;
            this.label_CopyRight.Location = location;
        }

        private void AboutBox_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.ItemCnt; i++)
            {
                if (this.ItemEnter[i])
                {
                    Process.Start(this.WebLink);
                }
            }
        }

        private void AboutBox_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.ItemCnt; i++)
            {
                if (this.ItemRect[i].Contains(e.Location) && !this.ItemEnter[i])
                {
                    this.Cursor = Cursors.Hand;
                    this.ItemEnter[i] = true;
                }
                else if (!this.ItemRect[i].Contains(e.Location) && this.ItemEnter[i])
                {
                    this.Cursor = Cursors.Default;
                    this.ItemEnter[i] = false;
                }
            }
        }

    }
}