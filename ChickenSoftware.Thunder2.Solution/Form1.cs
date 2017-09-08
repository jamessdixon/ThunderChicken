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


namespace ChickenSoftware.Thunder2.Solution
{
    public partial class Form1 : Form
    {

        MissileLauncher _launcher = new MissileLauncher();
        public Form1()
        {
            InitializeComponent();
            _launcher.command_reset();
            _launcher.command_switchLED(true);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            _launcher.command_Up(2000);
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            _launcher.command_Fire();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            _launcher.command_Down(1000);
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            _launcher.command_Right(3000);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            _launcher.command_Left(3000);
        }


    }
}
