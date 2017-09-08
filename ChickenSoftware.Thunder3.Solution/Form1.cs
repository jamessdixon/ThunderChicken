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
using ChickenSoftware.WeaponSystems;


namespace ChickenSoftware.Thunder3.Solution
{
    public partial class Form1 : Form
    {
        MissileLauncher _launcher = new MissileLauncher();
        public Form1()
        {
            InitializeComponent();
            _launcher.Activate();
            _launcher.Reset();
            _launcher.SwitchLed(true);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            _launcher.Up(1000);
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            _launcher.Fire();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            _launcher.Down(1000);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            _launcher.Right(1000);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            _launcher.Left(1000);
        }


    }
}
