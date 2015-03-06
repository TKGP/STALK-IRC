using STALK_IRC.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace STALK_IRC
{
    public partial class InstallForm : Form
    {
        public InstallForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if (Directory.Exists(path))
                if (File.Exists(path + @"\fsgame.ltx"))
                {
                    string configPath = path + @"\gamedata" + ((radioButton3.Checked || radioButton4.Checked) ? @"\config" : @"\configs");
                    string scriptPath = path + @"\gamedata\scripts";
<<<<<<< HEAD
                    string texturePath = path + @"\gamedata\textures\ui";
=======
>>>>>>> origin/master
                    if (!Directory.Exists(configPath + @"\ui"))
                        Directory.CreateDirectory(configPath + @"\ui");
                    if (!Directory.Exists(scriptPath))
                        Directory.CreateDirectory(scriptPath);
<<<<<<< HEAD
                    if (!Directory.Exists(texturePath))
                        Directory.CreateDirectory(texturePath);
                    File.WriteAllText(configPath + @"\ui\ui_chatinput.xml", Resources.chatInputUIXML);
                    File.WriteAllText(scriptPath + @"\stalk_irc.script", Resources.stalkIRCScript);
                    File.WriteAllText(scriptPath + @"\stalk_irc_icons.script", Resources.stalkIRCIconsScript);
                    File.WriteAllBytes(texturePath + @"\stalk_irc_icons.dds", Resources.stalkIRCIcons);

                    if (radioButton1.Checked || radioButton2.Checked)
                    {
                        if (!Directory.Exists(configPath + @"\ui\textures_descr"))
                            Directory.CreateDirectory(configPath + @"\ui\textures_descr");
                        File.WriteAllText(configPath + @"\ui\textures_descr\ui_stalk_irc_icons.xml", Resources.stalkIRCIconsXML);
                    }
=======
                    File.WriteAllText(configPath + @"\ui\ui_chatinput.xml", Resources.chatInputUIXML);
                    File.WriteAllText(scriptPath + @"\stalk_irc.script", Resources.stalkIRCScript);
>>>>>>> origin/master

                    // Patching ui_main_menu.script
                    if (File.Exists(scriptPath + @"\ui_main_menu.script"))
                    {
                        string script = File.ReadAllText(scriptPath + @"\ui_main_menu.script");
                        if (!Regex.Match(script, "stalk_irc").Success)
                        {
                            File.Copy(scriptPath + @"\ui_main_menu.script", scriptPath + @"\ui_main_menu.script.bak", true);
                            Match keypressMatch = Regex.Match(script, @"if\s+keyboard_action\s*==\s*ui_events\s*\.\s*WINDOW_KEY_PRESSED\s+then");
                            script = script.Insert(keypressMatch.Index + keypressMatch.Length, "\r\n\t\tstalk_irc.onKeypress( dik )");
                            File.WriteAllText(scriptPath + @"\ui_main_menu.script", script);
                        }
                    }
                    else
                        File.WriteAllText(scriptPath + @"\ui_main_menu.script",
                            radioButton4.Checked ? Resources.socMainMenu : (radioButton1.Checked ? Resources.csMainMenu : (radioButton2.Checked ? Resources.copMainMenu : Resources.laMainMenu)));

                    // Patching bind_stalker.script (SoC, CS, CoP) or bind_actor.script (LA)
                    string bindWhat = scriptPath + (radioButton3.Checked ? @"\bind_actor.script" : @"\bind_stalker.script");
                    if (File.Exists(bindWhat))
                    {
                        string script = File.ReadAllText(bindWhat);
                        if (!Regex.Match(script, "stalk_irc").Success)
                        {
                            File.Copy(bindWhat, bindWhat + ".bak", true);
                            Match callbackMatch = Regex.Match(script, @"callback\s*\.\s*death\s*,\s*self\s*\.\s*([^\s,]+)");
                            // Callback already exists
                            if (callbackMatch.Success)
                            {
                                string function = callbackMatch.Groups[1].Value;
                                Match functionMatch = Regex.Match(script, @"function\s+actor_binder\s*:\s*" + function + @"\(([^\)]+)\)");
                                string arguments = functionMatch.Groups[1].Value.Trim();
<<<<<<< HEAD
                                if (arguments.Contains(','))
                                {
                                    string argument = Regex.Match(arguments, @",\s*([^\s]+)\s*").Groups[1].Value;
=======
                                if(arguments.Contains(','))
                                {
                                    string argument = Regex.Match(arguments,@",\s*([^\s]+)\s*").Groups[1].Value;
>>>>>>> origin/master
                                    script = script.Insert(functionMatch.Index + functionMatch.Length, "\r\n\tstalk_irc.onDeath( " + argument + " )");
                                }
                                else
                                {
                                    script = script.Insert(functionMatch.Index + functionMatch.Length, "\r\n\tstalk_irc.onDeath( who )");
                                    script = script.Insert(functionMatch.Groups[1].Index + functionMatch.Groups[1].Length, ", who");
                                }
                            }
                            // Create new callback
                            else
                            {
                                Match takeItemMatch = Regex.Match(script, @"callback\s*\.\s*take_item_from_box\s*,\s*nil\s*\)");
                                script = script.Insert(takeItemMatch.Index + takeItemMatch.Length, "\r\n\tself.object:set_callback(callback.death, nil)");
                                Match takeItemMatch2 = Regex.Match(script, @"callback\s*\.\s*take_item_from_box\s*,\s*self\s*\.\s*([^\s,]+)[^\)]+\)");
                                script = script.Insert(takeItemMatch2.Index + takeItemMatch2.Length, "\r\n\tself.object:set_callback(callback.death, self.onDeath, self)");
                                string function = takeItemMatch2.Groups[1].Value;
                                Match takeItemMatch3 = Regex.Match(script, @"function\s+actor_binder\s*:\s*" + function);
                                script = script.Insert(takeItemMatch3.Index, "function actor_binder:onDeath( victim, who )\r\n\tstalk_irc.onDeath( who )\r\nend\r\n" +
                                    "----------------------------------------------------------------------------------------------------------------------\r\n");
                            }
                            Match updateMatch = Regex.Match(script, @"function\s+actor_binder\s*:\s*update\s*\([^\)]*\)");
                            script = script.Insert(updateMatch.Index + updateMatch.Length, "\r\n\tstalk_irc.onUpdate()");
                            File.WriteAllText(bindWhat, script);
                        }
                    }
                    else
                        File.WriteAllText(bindWhat,
                            radioButton4.Checked ? Resources.socBindStalker : (radioButton1.Checked ? Resources.csBindStalker :
                            (radioButton2.Checked ? Resources.copBindStalker : Resources.laBindActor)));

                    if (radioButton4.Checked)
                    {
                        if (!Directory.Exists(configPath + @"\misc"))
                            Directory.CreateDirectory(configPath + @"\misc");
                        File.WriteAllText(configPath + @"\misc\stalk_irc_clear.ltx", Resources.socInputClear);
                        File.WriteAllText(configPath + @"\misc\stalk_irc.ltx", Resources.socInputClear);
                    }

                    MessageBox.Show("Mod installed.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("fsgame.ltx not found in the specified directory.\nPlease select again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("The specified directory does not exist.\nPlease select again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox1.Text != "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
