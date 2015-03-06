﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Meebey.SmartIrc4net;

namespace STALK_IRC
{
    public partial class ClientForm : Form
    {
        // Constants
        const string VERSION = "Beta 11";
        const string SERVER = "irc.slashnet.org";
        const string CHANNEL = "#STALK-IRC_Testing";
        const string INPUT = @"\STALK-IRC_input.txt";
        const string OUTPUT = @"\STALK-IRC_output.txt";
        const string SOCINPUT = @"\gamedata\config\misc\stalk_irc.ltx";
        const string SOCINPUTCLEAR = @"\gamedata\config\misc\stalk_irc_clear.ltx";
        public const string REGISTRY = @"HKEY_CURRENT_USER\Software\STALK-IRC";
        const string INPUTREGEX = "([^/]+)/([^/]+)/(.+)";
        const char MAGIC = '☺';

        // Options
        public string name = (string)Registry.GetValue(REGISTRY, "Name", null);
        public string faction = Regex.Replace((string)Registry.GetValue(REGISTRY, "Faction", "Loners"), " ", "");
        public string timeout = (string)Registry.GetValue(REGISTRY, "Timeout", "5000");
        public string chatKey = (string)Registry.GetValue(REGISTRY, "ChatKey", "DIK_APOSTROPHE");
        public bool sendDeaths = (string)Registry.GetValue(REGISTRY, "SendDeaths", "True") == "True";
        public bool receiveDeaths = (string)Registry.GetValue(REGISTRY, "ReceiveDeaths", "True") == "True";
        public bool muhAtmospheres = (string)Registry.GetValue(REGISTRY, "MuhAtmospheres", "False") == "True";

        // Other stuff!
        public IrcClient irc = new IrcClient();
        Thread ircListen;
        List<string> games = new List<string>();
        Dictionary<string, SocData> socData = new Dictionary<string, SocData>();
        string newVersionUrl = "";
        bool doClose = false; // Close()ing at arbitrary positions seems to be a bad idea so this defers it to one of the timers

        public ClientForm()
        {
            InitializeComponent();
        }


        // Form events

        private void Form1_Load(object sender, EventArgs e)
        {
            string lastVersion = (string)Registry.GetValue(REGISTRY, "Version", null);
            if (lastVersion != VERSION)
            {
                if (lastVersion == null)
                    MessageBox.Show("Thank you for downloading STALK-IRC " + VERSION + ".\nPlease read the readme, if you haven't already.", "Welcome!");
                else
                    MessageBox.Show("Thank you for downloading STALK-IRC " + VERSION + ".\nDue to the update, remember to re-install the mod component for each of your games before use.",
                        "Update!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Registry.SetValue(REGISTRY, "Version", VERSION);
            }

            STALKIRCStrings.Populate();
            if (name == null)
            {
                name = STALKIRCStrings.GenerateName();
                Registry.SetValue(REGISTRY, "Name", name);
            }

            irc.Encoding = Encoding.UTF8;
            irc.SendDelay = 200;
            irc.ActiveChannelSyncing = true;
            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
            irc.OnJoin += new JoinEventHandler(OnJoin);
            irc.OnNickChange += new NickChangeEventHandler(OnNickChange);
            irc.OnTopic += new TopicEventHandler(OnTopic);
            irc.OnTopicChange += new TopicChangeEventHandler(OnTopicChange);
            irc.Connect(SERVER, 6667);
            irc.Login(name, "STALK-IRC Client");
            irc.RfcJoin(CHANNEL);
            ircListen = new Thread(irc.Listen);
            ircListen.Start();
            timer1_Tick(null, null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (irc.IsConnected)
            {
                irc.RfcQuit();
                irc.Disconnect();
            }
            ircListen.Join();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new OptionsForm(this).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new InstallForm().ShowDialog();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox4.Text == "")
                    textBox4.BackColor = Color.LightPink;
                else
                {
                    textBox4.BackColor = Color.White;
                    irc.SendMessage(SendType.Message, CHANNEL, faction + "/soc☺" + textBox4.Text);
                    SendMessage(irc.Nickname, faction + "/soc/" + textBox4.Text);
                    textBox4.Text = "";
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(newVersionUrl);
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "XR_3DA" || process.ProcessName == "xrEngine")
                {
                    string path = process.Modules[0].FileName;
                    path = Regex.Match(path, @"(.+)\\[^\\]+?\\[^\\]+").Groups[1].Value;
                    if (!games.Contains(path))
                    {
                        if (File.Exists(path + OUTPUT))
                        {
                            try
                            {
                                File.AppendAllText(path + INPUT, "");
                            }
                            catch (Exception ex)
                            {
                                Crash("STALK-IRC was unable to open the input file and will now shut down.\nTry running the client as Administrator.\n" +
                                    "Specific error: " + ex.Message);
                                return;
                            }
                            games.Add(path);
                        }
                        // Checking if SoC
                        else if (process.ProcessName == "XR_3DA")
                        {
                            string logPath = "";
                            string fsGame = "";
                            FileTryLoop(() => fsGame = File.ReadAllText(path + @"\fsgame.ltx"));
                            // Strip comments
                            fsGame = Regex.Replace(fsGame, ";.+", "");
                            Match appDataMatch = Regex.Match(fsGame, @"\$app_data_root\$.+");
                            if (!appDataMatch.Success)
                            {
                                Crash("Appdata root match failed. Please include the contents of fsgame.ltx with your report.");
                                return;
                            }
                            Match appDataPath = Regex.Match(appDataMatch.Value, @"\$app_data_root\$\s*=\s*(true|false)\s*\|\s*(true|false)\s*\|(.+)");
                            if (!appDataPath.Success)
                            {
                                Crash("Appdata path match failed. Please include the contents of fsgame.ltx with your report.");
                                return;
                            }
                            string pathSection = appDataPath.Groups[3].Value.Trim();
                            if (pathSection.Contains("|"))
                            {
                                Match match = Regex.Match(pathSection, @"([^\|]+?)\s*\|\s*(.+)");
                                if (match.Groups[1].Value == "$fs_root$")
                                    logPath = path + @"\" + match.Groups[2].Value;
                                else
                                    logPath = match.Groups[1].Value + match.Groups[2].Value;
                            }
                            else
                            {
                                logPath = pathSection;
                                // If relative path
                                if (logPath[1] != ':')
                                    logPath = path + @"\" + logPath;
                            }
                            if (logPath[logPath.Length - 1] != '\\')
                                logPath += @"\";
                            logPath += @"logs\xray_" + Environment.UserName.ToLower() + ".log";
                            if (!File.Exists(logPath))
                            {
                                Crash("Log file could not be found at " + logPath + ". Please include the contents of fsgame.ltx with your report.");
                                return;
                            }

                            string logText = "";
                            FileTryLoop(() => logText = File.ReadAllText(logPath));
                            // Yay, it's SoC
                            if (logText.Contains("~#stalk-irc "))
                            {
                                int lines = 0;
                                FileTryLoop(() => lines = File.ReadAllLines(logPath).Length);
                                games.Add(path);
                                socData.Add(path, new SocData(logPath, lines));
                                SendCommand(path, 1, "timeout", timeout);
                                SendCommand(path, 1, "chatkey", chatKey);
                                SendCommand(path, 1, "atmospheres", muhAtmospheres.ToString());
                            }
                        }
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (doClose)
                Close();
            foreach (string path in games)
            {
                List<string> lines = new List<string>();
                if (socData.ContainsKey(path))
                {
                    string[] logLines = { };
                    FileTryLoop(() => logLines = File.ReadAllLines(socData[path].logPath));
                    if (logLines.Length < socData[path].lastLines)
                        socData[path].lastLines = 0;
                    for (int index = socData[path].lastLines; index != logLines.Length; index++)
                    {
                        Match match = Regex.Match(logLines[index], "~#stalk-irc (.+)");
                        if (match.Success)
                            lines.Add(match.Groups[1].Value);
                    }
                    socData[path].lastLines = logLines.Length;
                }
                else
                {
                    string[] lineArray = { };
                    FileTryLoop(() =>
                    {
                        lineArray = File.ReadAllLines(path + OUTPUT);
                        File.WriteAllText(path + OUTPUT, "");
                    });
                    foreach (string line in lineArray)
                        lines.Add(line);
                }
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        Match match = Regex.Match(line, INPUTREGEX);
                        string action = match.Groups[1].Value;
                        string arg1 = match.Groups[2].Value;
                        string arg2 = match.Groups[3].Value;
                        string message = "";
                        switch (action)
                        {
                            case "1": // Settings request
                                SendCommand(path, 1, "timeout", timeout);
                                SendCommand(path, 1, "chatkey", chatKey);
                                SendCommand(path, 1, "atmospheres", muhAtmospheres.ToString());
                                break;
                            case "2": // Normal message
                                message = faction + "/" + arg2;
                                match = Regex.Match(message, "([^/]+/[^/]+)/(.+)");
                                irc.SendMessage(SendType.Message, CHANNEL, match.Groups[1].Value + '☺' + match.Groups[2].Value);
                                SendMessage(irc.Nickname, message);
                                break;
                            case "3": // Death message
                                if (sendDeaths)
                                {
                                    match = Regex.Match(arg2, "([^/]+)/(.+)");
                                    string randName = STALKIRCStrings.GenerateName();
                                    message = "Loners/" + match.Groups[2].Value + MAGIC + STALKIRCStrings.BuildSentence(irc.Nickname, arg1, match.Groups[1].Value);
                                    irc.SendMessage(SendType.Message, CHANNEL, randName + "☻" + message);
                                    SendMessage(randName, message.Replace(MAGIC, '/'));
                                }
                                break;
                        }
                    }
                }
            }
        }


        // IRC events

        private void OnTopic(object sender, TopicEventArgs e)
        {
            Match match = Regex.Match(e.Topic, @"Latest version: ([^\|]+) \| Download: ([^ ]+)");
            if (match.Success && VERSION != match.Groups[1].Value)
            {
                irc.RfcQuit();
                irc.Disconnect();
                newVersionUrl = match.Groups[2].Value;
                Invoke(new Action(() =>
                {
                    textBox4.Enabled = false;
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    label1.Hide();
                    linkLabel1.Text = "New version: " + match.Groups[1].Value + " - Click to open in browser!";
                    linkLabel1.Show();
                }));
                SendMessage("Information", "_/_/A new version of STALK-IRC is available!");
            }
            else
            {
                Invoke(new Action(() => label1.Text = "Latest version: " + match.Groups[1].Value + " - Up to date!"));
            }
        }

        private void OnTopicChange(object sender, TopicChangeEventArgs e)
        {
            TopicEventArgs topic = new TopicEventArgs(null, null, e.NewTopic);
            OnTopic(null, topic);
        }

        private void OnRawMessage(object sender, IrcEventArgs e)
        {
            Invoke(new Action(() => textBox1.AppendText((textBox1.Lines.Length != 0 ? "\r\n" : "") + e.Data.RawMessage)));
        }

        private void OnChannelMessage(object sender, IrcEventArgs e)
        {
            string name = "", message = "";
            Match match = Regex.Match(e.Data.Message, "(.+)☻(.+)");
            if (match.Success)
            {
                if (!receiveDeaths)
                    return;
                name = match.Groups[1].Value;
                message = match.Groups[2].Value;
            }
            else
            {
                name = e.Data.Nick;
                message = e.Data.Message;
            }
            if (message.Contains(MAGIC))
                SendMessage(name, message.Replace(MAGIC, '/'));
            else
                SendMessage(name, "Loners/SoC/" + message);
        }

        private void OnJoin(object sender, JoinEventArgs e)
        {
            SendMessage("Information", "_/_/" + e.Who + " has logged on to the network.");
        }

        private void OnNickChange(object sender, NickChangeEventArgs e)
        {
            SendMessage("Information", "_/_/" + e.NewNickname + " has logged on to the network.");
        }


        // Utility functions

        public void Crash(string message)
        {
            timer1.Enabled = false;
            doClose = true;
            MessageBox.Show(message, "Fatal error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SendMessage(string name, string message)
        {
            Match match = Regex.Match(message, "[^/]+/[^/]+/(.+)");
            Invoke(new Action(() => textBox3.AppendText((textBox3.Lines.Length != 0 ? "\r\n" : "") + name.Replace('_', ' ') + "> " + match.Groups[1].Value)));
            string line = "2/" + name + "/" + message;
            foreach (string path in games)
            {
                string newPath = path;
                string newLine = line;
                if (socData.ContainsKey(path))
                {
                    newPath += SOCINPUT;
                    newLine = socData[path].sentMessages++ + " = \"" + line + "\"";
                }
                else
                    newPath += INPUT;
                newLine += "\r\n";
                FileTryLoop(() => File.AppendAllText(newPath, newLine));
            }
        }

        private void SendCommand(string path, int action, string arg1, string arg2)
        {
            string line;
            string newPath = path;
            if (socData.ContainsKey(path))
            {
                newPath += SOCINPUT;
                line = socData[path].sentMessages++ + " = \"" + action + "/" + arg1 + "/" + arg2 + "\"";
            }
            else
            {
                newPath += INPUT;
                line = action + "/" + arg1 + "/" + arg2;
            }
            line += "\r\n";
            FileTryLoop(() => File.AppendAllText(newPath, line));
        }

        public void SendCommandAll(int action, string arg1, string arg2)
        {
            foreach (string path in games)
                SendCommand(path, action, arg1, arg2);
        }

        // I can't decide if this is clever or just wank
        private void FileTryLoop(Action lambda)
        {
            while (true)
            {
                try
                {
                    lambda();
                }
                catch
                {
                    Thread.Sleep(100);
                    continue;
                }
                break;
            }
        }
    }
}
