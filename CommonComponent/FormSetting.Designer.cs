using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonComponent
{
    partial class FormSetting
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (!this.SettingClose)
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
            this.checkBox_Start = new CheckBox();
            this.checkBox_Mute = new CheckBox();
            this.button_Res = new Button();
            this.button_OK = new Button();
            this.button_Cancel = new Button();
            base.SuspendLayout();
            this.checkBox_Start.AutoSize = true;
            this.checkBox_Start.Location = new Point(12, 12);
            this.checkBox_Start.Name = "checkBox_Start";
            this.checkBox_Start.Size = new Size(239, 17);
            this.checkBox_Start.TabIndex = 0;
            this.checkBox_Start.Text = "Run USB Soccer Fidget when Windows start";
            this.checkBox_Start.UseVisualStyleBackColor = true;
            this.checkBox_Mute.AutoSize = true;
            this.checkBox_Mute.Location = new Point(12, 35);
            this.checkBox_Mute.Name = "checkBox_Mute";
            this.checkBox_Mute.Size = new Size(82, 17);
            this.checkBox_Mute.TabIndex = 1;
            this.checkBox_Mute.Text = "Mute sound";
            this.checkBox_Mute.UseVisualStyleBackColor = true;
            this.button_Res.Location = new Point(12, 58);
            this.button_Res.Name = "button_Res";
            this.button_Res.Size = new Size(118, 23);
            this.button_Res.TabIndex = 2;
            this.button_Res.Text = "Reset highest score";
            this.button_Res.UseVisualStyleBackColor = true;
            this.button_OK.Location = new Point(154, 58);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new Size(75, 23);
            this.button_OK.TabIndex = 3;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_Cancel.Location = new Point(235, 58);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new Size(75, 23);
            this.button_Cancel.TabIndex = 4;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            //TODO
            //base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(319, 95);
            base.Controls.Add(this.button_Cancel);
            base.Controls.Add(this.button_OK);
            base.Controls.Add(this.button_Res);
            base.Controls.Add(this.checkBox_Mute);
            base.Controls.Add(this.checkBox_Start);
            //TODO
            //base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormSetting";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            this.Text = "Setting";
            base.ResumeLayout(false);
            base.PerformLayout();
        }


    }
}