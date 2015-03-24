using STALK_IRC.Properties;
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

namespace STALK_IRC
{
    public partial class HelpForm : Form
    {
        public HelpForm(int tab)
        {
            InitializeComponent();
            richTextBox1.Rtf = Resources.helpMain;
            richTextBox2.Rtf = Resources.helpInstall;
            richTextBox3.Rtf = Resources.helpOptions;
            richTextBox4.Rtf = Resources.helpUsage;
            tabControl1.SelectedIndex = tab;
        }

        private void HelpForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            MessageBox.Show("You're already on the Help page!","Help, Help!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
