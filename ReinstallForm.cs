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
    public partial class ReinstallForm : Form
    {
        Dictionary<string, string> games = new Dictionary<string, string>();
        Dictionary<string, string> gameNames = new Dictionary<string, string>();
        public ReinstallForm(string previousGames)
        {
            foreach (Match match in Regex.Matches(previousGames, @"([^\|]+)\|([^\|]+)"))
            {
                string game = match.Groups[1].Value;
                string path = match.Groups[2].Value;
                games[path] = game;
            }
            InitializeComponent();
        }

        private void ReinstallForm_Load(object sender, EventArgs e)
        {
            gameNames["SoC"] = "Shadow of Chernobyl\t";
            gameNames["CS"] = "Clear Sky\t\t";
            gameNames["CoP"] = "Call of Pripyat\t\t";
            gameNames["LA"] = "Lost Alpha\t\t";

            foreach (string path in games.Keys)
            {
                checkedListBox1.Items.Add(gameNames[games[path]] + path, true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newRegistry = "";
            foreach (string item in checkedListBox1.CheckedItems)
            {
                string path = Regex.Match(item, "\t+(.+)").Groups[1].Value;
                if (InstallForm.Install(games[path], path, true))
                    newRegistry += games[path] + "|" + path + "|";
                else
                    MessageBox.Show("Error!", "Installation failed for " + path + "\nVerify that it still exists and try again from the manual install menu.");
            }
            Registry.SetValue(ClientForm.REGISTRY, "GameList", newRegistry);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReinstallForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            new HelpForm(0).ShowDialog();
        }
    }
}
