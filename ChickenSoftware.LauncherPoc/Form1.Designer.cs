
namespace ChickenSoftware.LauncherPoc
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.leftButton = new System.Windows.Forms.Button();
            this.fireButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.USB = new UsbLibrary.UsbHidPort(this.components);
            this.SuspendLayout();
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(32, 111);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(78, 88);
            this.leftButton.TabIndex = 0;
            this.leftButton.Text = "LEFT";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.leftButton_Click);
            // 
            // fireButton
            // 
            this.fireButton.Location = new System.Drawing.Point(116, 115);
            this.fireButton.Name = "fireButton";
            this.fireButton.Size = new System.Drawing.Size(78, 88);
            this.fireButton.TabIndex = 1;
            this.fireButton.Text = "FIRE";
            this.fireButton.UseVisualStyleBackColor = true;
            this.fireButton.Click += new System.EventHandler(this.fireButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(116, 21);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(78, 88);
            this.upButton.TabIndex = 2;
            this.upButton.Text = "UP";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(116, 209);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(78, 88);
            this.downButton.TabIndex = 3;
            this.downButton.Text = "DOWN";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(200, 111);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(78, 88);
            this.rightButton.TabIndex = 4;
            this.rightButton.Text = "RIGHT";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // USB
            // 
            this.USB.ProductId = 0;
            this.USB.SpecifiedDevice = null;
            this.USB.VendorId = 0;
            this.USB.OnDeviceArrived += new System.EventHandler(this.USB_OnSpecifiedDeviceArrived);
            this.USB.OnDeviceRemoved += new System.EventHandler(this.USB_OnSpecifiedDeviceRemoved);
            this.USB.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(this.USB_OnDataRecieved);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 369);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.fireButton);
            this.Controls.Add(this.leftButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button fireButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button rightButton;
    }
}

