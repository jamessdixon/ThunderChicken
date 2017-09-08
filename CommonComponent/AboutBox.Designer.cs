using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CommonComponent
{
    partial class AboutBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (!this.AboutClose)
            {
                base.Visible = false;
                return;
            }
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(AboutBox));
            this.label_ProgramName = new Label();
            this.label_CopyRight = new Label();
            this.label_ProgramVersion = new Label();
            this.label_TM = new Label();
            base.SuspendLayout();
            this.label_ProgramName.AutoSize = true;
            this.label_ProgramName.BackColor = Color.Transparent;
            this.label_ProgramName.Font = new Font("Century Gothic", 20.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label_ProgramName.ForeColor = Color.White;
            this.label_ProgramName.Location = new Point(117, 95);
            this.label_ProgramName.Margin = new Padding(0);
            this.label_ProgramName.Name = "label_ProgramName";
            this.label_ProgramName.Size = new Size(61, 33);
            this.label_ProgramName.TabIndex = 1;
            this.label_ProgramName.Text = "USB";
            this.label_CopyRight.AutoSize = true;
            this.label_CopyRight.BackColor = Color.Transparent;
            this.label_CopyRight.Font = new Font("Century Gothic", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label_CopyRight.ForeColor = Color.White;
            this.label_CopyRight.Location = new Point(14, 158);
            this.label_CopyRight.Margin = new Padding(0);
            this.label_CopyRight.Name = "label_CopyRight";
            this.label_CopyRight.Size = new Size(306, 16);
            this.label_CopyRight.TabIndex = 2;
            this.label_CopyRight.Text = "Copyright(c) 2009 Dream Cheeky Ltd. All rights reserved.";
            this.label_ProgramVersion.AutoSize = true;
            this.label_ProgramVersion.BackColor = Color.Transparent;
            this.label_ProgramVersion.Font = new Font("Century Gothic", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.label_ProgramVersion.ForeColor = Color.White;
            this.label_ProgramVersion.Location = new Point(140, 134);
            this.label_ProgramVersion.Margin = new Padding(0);
            this.label_ProgramVersion.Name = "label_ProgramVersion";
            this.label_ProgramVersion.Size = new Size(68, 16);
            this.label_ProgramVersion.TabIndex = 3;
            this.label_ProgramVersion.Text = "v1.0 Build 1";
            this.label_TM.AutoSize = true;
            this.label_TM.BackColor = Color.Transparent;
            this.label_TM.ForeColor = Color.White;
            this.label_TM.Location = new Point(181, 100);
            this.label_TM.Name = "label_TM";
            this.label_TM.Size = new Size(23, 13);
            this.label_TM.TabIndex = 4;
            this.label_TM.Text = "TM";
            this.BackgroundImage = (Image)componentResourceManager.GetObject("$this.BackgroundImage");
            base.ClientSize = new Size(348, 245);
            base.Controls.Add(this.label_TM);
            base.Controls.Add(this.label_ProgramVersion);
            base.Controls.Add(this.label_ProgramName);
            base.Controls.Add(this.label_CopyRight);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "AboutBox";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "About";
            base.Click += new EventHandler(this.AboutBox_Click);
            base.MouseMove += new MouseEventHandler(this.AboutBox_MouseMove);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

    }
}