using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonComponent
{
    public partial class FormSetting : Form
    {
        public bool SettingClose;

        public CheckBox checkBox_Start;

        public CheckBox checkBox_Mute;

        public Button button_Res;

        public Button button_OK;

        public Button button_Cancel;

        public FormSetting()
        {
            this.InitializeComponent();
        }

    }
}