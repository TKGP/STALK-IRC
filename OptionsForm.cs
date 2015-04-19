using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STALK_IRC
{
    public partial class OptionsForm : Form
    {
        ClientForm parent;
        public OptionsForm(ClientForm parentForm)
        {
            parent = parentForm;
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            textBox1.Text = parent.name;
            comboBox1.SelectedItem = (string)ClientForm.REGISTRY.GetValue("Faction", "Loners");
            textBox2.Text = parent.timeout;
            comboBox2.SelectedItem = parent.chatKey;
            checkBox1.Checked = parent.sendDeaths;
            checkBox2.Checked = parent.receiveDeaths;
            checkBox3.Checked = parent.muhAtmospheres;
        }

        // Name - Random
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = STALKIRCStrings.GenerateName();
        }

        // Confirm
        private void button2_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (textBox1.Text == "")
            {
                error = true;
                textBox1.BackColor = Color.LightPink;
            }
            if (textBox2.Text == "")
            {
                error = true;
                textBox2.BackColor = Color.LightPink;
            }
            try
            {
                Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                error = true;
                textBox2.BackColor = Color.LightPink;
            }

            if (!error)
            {
                string name = textBox1.Text.Replace(' ', '_');
                parent.name = name;
                parent.irc.RfcNick(name);
                parent.faction = Regex.Replace(comboBox1.Text, " ", "");
                parent.timeout = textBox2.Text;
                parent.SendCommandAll(1, "timeout", textBox2.Text);
                parent.chatKey = comboBox2.Text;
                parent.SendCommandAll(1, "chatkey", comboBox2.Text);
                parent.sendDeaths = checkBox1.Checked;
                parent.receiveDeaths = checkBox2.Checked;
                parent.muhAtmospheres = checkBox3.Checked;
                parent.SendCommandAll(1, "atmospheres", checkBox3.Checked.ToString());

                RegistryKey registry = ClientForm.REGISTRY;
                registry.SetValue("Name", name);
                registry.SetValue("Faction", comboBox1.Text);
                registry.SetValue("Timeout", textBox2.Text);
                registry.SetValue("ChatKey", comboBox2.Text);
                registry.SetValue("SendDeaths", checkBox1.Checked);
                registry.SetValue("ReceiveDeaths", checkBox2.Checked);
                registry.SetValue("MuhAtmospheres", checkBox3.Checked);

                Close();
            }
        }

        // Cancel
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OptionsForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            new HelpForm(3).ShowDialog();
        }
    }
}
