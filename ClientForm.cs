using System;
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
using GitHubUpdate;
using System.Media;

namespace STALK_IRC
{
    public partial class ClientForm : Form
    {
        // Constants
        private const string SERVER = "irc.slashnet.org";
        private const string CHANNEL = "#STALK-IRC";
        private const string INPUT = @"\STALK-IRC_input.txt";
        private const string OUTPUT = @"\STALK-IRC_output.txt";
        private const string SOCINPUT = @"\gamedata\config\misc\stalk_irc.ltx";
        private const string SOCINPUTCLEAR = @"\gamedata\config\misc\stalk_irc_clear.ltx";
        public static readonly RegistryKey REGISTRY = Registry.CurrentUser.CreateSubKey(@"Software\STALK-IRC");
        private const char MAGIC = '☺';
        private readonly Encoding RUENCODING = Encoding.GetEncoding(1251);

        // Options
        public string name, faction, timeout, chatKey;
        public bool sendDeaths, receiveDeaths, muhAtmospheres;

        // Other stuff!
        public IrcClient irc = new IrcClient();
        private Thread ircListen;
        private Dictionary<int, string> games = new Dictionary<int, string>();
        private Dictionary<string, SocData> socData = new Dictionary<string, SocData>();
        private string lastGame = "SoC";
        private bool updateSkipped = false;
        private bool doClose = false; // Close()ing at arbitrary positions seems to be a bad idea so this defers it to one of the timers

        public ClientForm()
        {
            InitializeComponent();
        }


        // Form events

        private async void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "STALK-IRC Client " + Application.ProductVersion;

            if (await CheckUpdate(false))
                return;

            name = (string)REGISTRY.GetValue("Name", null);
            faction = Regex.Replace((string)REGISTRY.GetValue("Faction", "Loners"), " ", "");
            timeout = (string)REGISTRY.GetValue("Timeout", "5000");
            chatKey = (string)REGISTRY.GetValue("ChatKey", "DIK_APOSTROPHE");
            sendDeaths = (string)REGISTRY.GetValue("SendDeaths", "True") == "True";
            receiveDeaths = (string)REGISTRY.GetValue("ReceiveDeaths", "True") == "True";
            muhAtmospheres = (string)REGISTRY.GetValue("MuhAtmospheres", "False") == "True";

            STALKIRCStrings.Populate();
            if (name == null)
            {
                name = STALKIRCStrings.GenerateName();
                REGISTRY.SetValue("Name", name);
            }

            string lastVersion = (string)REGISTRY.GetValue("Version", null);
            if (lastVersion != Application.ProductVersion)
            {
                string previousGames = (string)REGISTRY.GetValue("GameList", "");
                if (previousGames != "")
                    new ReinstallForm(previousGames).ShowDialog();
                else
                    MessageBox.Show("Thank you for downloading STALK-IRC " + Application.ProductVersion + ".\nDue to the update, remember to re-install the mod component for each of your games before use.",
                        "Update!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            REGISTRY.SetValue("Version", Application.ProductVersion);

            irc.Encoding = Encoding.UTF8;
            irc.SendDelay = 200;
            irc.ActiveChannelSyncing = true;
            irc.OnRawMessage += new IrcEventHandler(OnRawMessage);
            irc.OnChannelMessage += new IrcEventHandler(OnChannelMessage);
            irc.OnJoin += new JoinEventHandler(OnJoin);
            irc.OnNickChange += new NickChangeEventHandler(OnNickChange);
            irc.OnQueryMessage += new IrcEventHandler(OnChannelMessage);
            irc.Connect(SERVER, 6667);
            irc.Login(name, "STALK-IRC Client " + Application.ProductVersion);
            irc.RfcJoin(CHANNEL);
            ircListen = new Thread(irc.Listen);
            ircListen.Start();
            timer1_Tick(null, null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (irc.IsConnected)
            {
                irc.RfcQuit();
                irc.Disconnect();
            }
            if (ircListen != null)
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

        private void ClientForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            new HelpForm(0).ShowDialog();
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
                    Match match = Regex.Match(textBox4.Text, "^/([^ ]+) ?(.*)");
                    if (false && match.Success)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "w":
                            case "who":
                            case "list":

                                textBox4.Text = "";
                                break;
                            case "msg":
                            case "q":
                            case "query":
                            case "pm":

                                textBox4.Text = "";
                                break;
                            case "help":
                            case "?":
                                //SendClientText("Available commands: ?/help - displays this message; w/who/list - lists all online users; q/query/msg/pm [username] [message] - sends a private message to the specified user");
                                textBox4.Text = "";
                                break;
                            default:
                                textBox4.Text = "Unknown command \"" + match.Groups[1].Value + "\"";
                                textBox4.BackColor = Color.LightPink;
                                return;
                        }
                    }
                    else
                    {
                        irc.SendMessage(SendType.Message, CHANNEL, faction + "/" + lastGame + "☺" + textBox4.Text);
                        SendMessage(irc.Nickname, faction + "/" + lastGame + "/" + textBox4.Text);
                        textBox4.Text = "";
                    }
                }
            }
        }

        // Checking for new games every 10 seconds
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Prune closed games
            List<int> toPrune = new List<int>();
            foreach (int id in games.Keys)
            {
                try
                {
                    Process.GetProcessById(id);
                }
                catch (ArgumentException)
                {
                    toPrune.Add(id);
                }
            }
            foreach (int id in toPrune)
            {
                if (socData.ContainsKey(games[id]))
                    socData.Remove(games[id]);
                games.Remove(id);
            }

            foreach (Process process in Process.GetProcesses())
            {
                if ((process.ProcessName == "XR_3DA" || process.ProcessName == "xrEngine") && !games.ContainsKey(process.Id))
                {
                    string path = process.Modules[0].FileName;
                    path = Regex.Match(path, @"(.+)\\[^\\]+?\\[^\\]+").Groups[1].Value;
                    if (File.Exists(path + OUTPUT))
                    {
                        try
                        {
                            File.AppendAllText(path + INPUT, "", RUENCODING);
                        }
                        catch (Exception ex)
                        {
                            Crash("STALK-IRC was unable to open the input file and will now shut down.\nTry running the client as Administrator.\n" +
                                "Specific error: " + ex.Message);
                            return;
                        }
                        games[process.Id] = path;
                    }
                    // Checking if SoC
                    else if (process.ProcessName == "XR_3DA")
                    {
                        string logPath = "";
                        string fsGame = "";
                        FileTryLoop(() => fsGame = File.ReadAllText(path + @"\fsgame.ltx", RUENCODING));
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
                        FileTryLoop(() => logText = File.ReadAllText(logPath, RUENCODING));
                        // Yay, it's SoC
                        if (logText.Contains("~#stalk-irc "))
                        {
                            int lines = 0;
                            FileTryLoop(() => lines = File.ReadAllLines(logPath, RUENCODING).Length);
                            socData.Add(path, new SocData(logPath, lines));
                            games[process.Id] = path;
                            SendCommand(path, 1, "timeout", timeout);
                            SendCommand(path, 1, "chatkey", chatKey);
                            SendCommand(path, 1, "atmospheres", muhAtmospheres.ToString());
                        }
                    }
                }
            }
        }

        // Checking for input from games every second
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (doClose)
                Close();
            foreach (string path in games.Values)
            {
                List<string> lines = new List<string>();
                if (socData.ContainsKey(path))
                {
                    string[] logLines = { };
                    FileTryLoop(() => logLines = File.ReadAllLines(socData[path].logPath, RUENCODING));
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
                        lineArray = File.ReadAllLines(path + OUTPUT, RUENCODING);
                        File.WriteAllText(path + OUTPUT, "", RUENCODING);
                    });
                    foreach (string line in lineArray)
                        lines.Add(line);
                }
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        Match match = Regex.Match(line, "([^/]+)/?(.*)");
                        string command = match.Groups[1].Value;
                        string arguments = match.Groups[2].Value;
                        string game, message;
                        switch (command)
                        {
                            case "1": // Settings request
                                SendCommand(path, 1, "timeout", timeout);
                                SendCommand(path, 1, "chatkey", chatKey);
                                SendCommand(path, 1, "atmospheres", muhAtmospheres.ToString());
                                break;
                            case "2": // Normal message
                                Match messageMatch = Regex.Match(arguments, "([^/]+)/(.+)");
                                game = messageMatch.Groups[1].Value;
                                lastGame = game;
                                message = messageMatch.Groups[2].Value;
                                irc.SendMessage(SendType.Message, CHANNEL, faction + "/" + game + MAGIC + message);
                                SendMessage(irc.Nickname, faction + "/" + game + "/" + message);
                                break;
                            case "3": // Death message
                                if (sendDeaths)
                                {
                                    Match deathMatch = Regex.Match(arguments, "([^/]+)/([^/]+)/([^/]+)/(.+)");
                                    game = deathMatch.Groups[1].Value;
                                    lastGame = game;
                                    string level = deathMatch.Groups[2].Value;
                                    string section = deathMatch.Groups[3].Value;
                                    string classType = deathMatch.Groups[4].Value;
                                    string randName = STALKIRCStrings.GenerateName();
                                    message = "Loners/" + game + MAGIC + STALKIRCStrings.BuildSentence(irc.Nickname, level, section, classType);
                                    irc.SendMessage(SendType.Message, CHANNEL, randName + "☻" + message);
                                    SendMessage(randName, message.Replace(MAGIC, '/'), Color.DarkRed);
                                }
                                break;
                            default:
                                SendMessage("Error: Client got bad command.", "_/_/Path: " + path + " | Line: " + line, Color.LimeGreen);
                                break;
                        }
                    }
                }
            }
        }

        // Checking for updates every minute
        private async void timer3_Tick(object sender, EventArgs e)
        {
            await CheckUpdate(true);
        }


        // IRC events

        private void OnRawMessage(object sender, IrcEventArgs e)
        {
            Invoke(new Action(() => textBox1.AppendText((textBox1.Lines.Length != 0 ? "\r\n" : "") + e.Data.RawMessage)));
        }

        private void OnChannelMessage(object sender, IrcEventArgs e)
        {
            string name = "", message = "";
            Color color = e.Data.Type == ReceiveType.QueryMessage ? Color.DeepPink : Color.Black;
            Match match = Regex.Match(e.Data.Message, "(.*)☻(.+)");
            if (match.Success)
            {
                if (!receiveDeaths || match.Groups[1].Length == 0)
                    return;
                name = match.Groups[1].Value;
                message = match.Groups[2].Value;
                color = Color.DarkRed;
            }
            else
            {
                name = e.Data.Nick;
                message = e.Data.Message;
            }
            if (message.Contains(MAGIC))
            {
                match = Regex.Match(message, "([^/]+)/(.+)" + MAGIC);
                if (!match.Success || !STALKIRCStrings.validFactions.Contains(match.Groups[1].Value) || !STALKIRCStrings.validGames.Contains(match.Groups[2].Value.ToUpper()))
                    return;
                SendMessage(name, message.Replace(MAGIC, '/'), color);
            }
            else
                SendMessage(name, "Loners/SoC/" + message, color);
        }

        private void OnJoin(object sender, JoinEventArgs e)
        {
            SendMessage("Information", "_/_/" + e.Who.Replace('_', ' ') + " has logged on to the network.", Color.DarkBlue);
        }

        private void OnNickChange(object sender, NickChangeEventArgs e)
        {
            SendMessage("Information", "_/_/" + e.NewNickname.Replace('_', ' ') + " has logged on to the network.", Color.DarkBlue);
        }


        // Utility functions

        public void Crash(string message)
        {
            timer1.Enabled = false;
            doClose = true;
            MessageBox.Show(message, "Fatal error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async Task<bool> CheckUpdate(bool silent)
        {
            try
            {
                UpdateChecker checker = new UpdateChecker("TKGP", "STALK-IRC");
                UpdateType update = await checker.CheckUpdate();
                if (update == UpdateType.Major || update == UpdateType.Minor)
                {
                    this.Hide();
                    timer1.Enabled = false;
                    timer3.Enabled = false;
                    SendMessage("Information", "_/_/A mandatory update to STALK-IRC is available! STALK-IRC is now disabled; check the client to update.", Color.DarkBlue);
                    SystemSounds.Asterisk.Play();
                    string notes = await checker.RenderReleaseNotes();
                    DialogResult result = new UpdateForm(notes, silent, true).ShowDialog();
                    if (result == DialogResult.Yes)
                        checker.DownloadAsset("STALK-IRC.exe");
                    doClose = true;
                    return true;
                }
                else if (!updateSkipped && update == UpdateType.Patch)
                {
                    SendMessage("Information", "_/_/An optional update to STALK-IRC is available! Check the client to update.", Color.DarkBlue);
                    SystemSounds.Asterisk.Play();
                    string notes = await checker.RenderReleaseNotes();
                    DialogResult result = new UpdateForm(notes, silent, false).ShowDialog();
                    if (result == DialogResult.Yes)
                    {
                        this.Hide();
                        timer1.Enabled = false;
                        timer3.Enabled = false;
                        checker.DownloadAsset("STALK-IRC.exe");
                        doClose = true;
                    }
                    else
                        updateSkipped = true;
                }
            }
            catch (Octokit.RateLimitExceededException)
            {
                // Woops
            }
            return false;
        }

        private void SendMessage(string name, string message)
        {
            SendMessage(name, message, Color.Black);
        }

        private void SendMessage(string name, string message, Color color)
        {
            Match match = Regex.Match(message, "[^/]+/[^/]+/(.+)");
            Invoke(new Action(() =>
            {
                richTextBox1.AppendText((richTextBox1.Lines.Length != 0 ? "\r\n" : ""));
                richTextBox1.SelectionColor = color;
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);
                richTextBox1.AppendText(name.Replace('_', ' ') + "> ");
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                richTextBox1.AppendText(match.Groups[1].Value);
            }));
            string line = "2/" + name + "/" + message;
            foreach (string path in games.Values)
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
                FileTryLoop(() => File.AppendAllText(newPath, newLine, RUENCODING));
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
            FileTryLoop(() => File.AppendAllText(newPath, line, RUENCODING));
        }

        public void SendCommandAll(int action, string arg1, string arg2)
        {
            foreach (string path in games.Values)
                SendCommand(path, action, arg1, arg2);
        }

        // I can't decide if this is clever or just wank
        private bool FileTryLoop(Action lambda)
        {
            int count = 0;
            while (true)
            {
                try
                {
                    lambda();
                }
                catch
                {
                    if (count > 10)
                        return false;
                    count++;
                    Thread.Sleep(100);
                    continue;
                }
                break;
            }
            return true;
        }
    }
}
