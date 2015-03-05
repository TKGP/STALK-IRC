namespace STALK_IRC
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            this.toolTip1.SetToolTip(this.label1, "Your display name on the network; most special characters not allowed");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Random";
            this.toolTip1.SetToolTip(this.button1, "Generates a new random name");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(110, 64);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(185, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Message duration";
            this.toolTip1.SetToolTip(this.label2, "Length of time before in-game messages fade out (in milliseconds)");
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Loner",
            "Bandit",
            "Duty",
            "Freedom",
            "Ecologist",
            "Clear Sky",
            "Mercenary",
            "Military",
            "Monolith"});
            this.comboBox1.Location = new System.Drawing.Point(110, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(185, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "This doesn\'t do anything yet :^)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Faction";
            this.toolTip1.SetToolTip(this.label3, "Your in-game icon will be a character from this faction");
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 118);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(131, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Send death messages";
            this.toolTip1.SetToolTip(this.checkBox1, "Reports deaths to the network when checked");
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(149, 118);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(146, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Receive death messages";
            this.toolTip1.SetToolTip(this.checkBox2, "Displays death reports from other players when checked");
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(220, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Confirm";
            this.toolTip1.SetToolTip(this.button2, "Save all settings and close the window");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(301, 164);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Cancel";
            this.toolTip1.SetToolTip(this.button3, "Discard changes and close the window");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Chat key";
            this.toolTip1.SetToolTip(this.label4, "Key to press to open the chat box in-game");
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "DIK_0",
            "DIK_1",
            "DIK_2",
            "DIK_3",
            "DIK_4",
            "DIK_5",
            "DIK_6",
            "DIK_7",
            "DIK_8",
            "DIK_9",
            "DIK_A",
            "DIK_ADD",
            "DIK_APOSTROPHE",
            "DIK_APPS",
            "DIK_AT",
            "DIK_AX",
            "DIK_B",
            "DIK_BACK",
            "DIK_BACKSLASH",
            "DIK_C",
            "DIK_CAPITAL",
            "DIK_CIRCUMFLEX",
            "DIK_COLON",
            "DIK_COMMA",
            "DIK_CONVERT",
            "DIK_D",
            "DIK_DECIMAL",
            "DIK_DELETE",
            "DIK_DIVIDE",
            "DIK_DOWN",
            "DIK_E",
            "DIK_END",
            "DIK_EQUALS",
            "DIK_ESCAPE",
            "DIK_F",
            "DIK_F1",
            "DIK_F10",
            "DIK_F11",
            "DIK_F12",
            "DIK_F13",
            "DIK_F14",
            "DIK_F15",
            "DIK_F2",
            "DIK_F3",
            "DIK_F4",
            "DIK_F5",
            "DIK_F6",
            "DIK_F7",
            "DIK_F8",
            "DIK_F9",
            "DIK_G",
            "DIK_GRAVE",
            "DIK_H",
            "DIK_HOME",
            "DIK_I",
            "DIK_INSERT",
            "DIK_J",
            "DIK_K",
            "DIK_KANA",
            "DIK_KANJI",
            "DIK_L",
            "DIK_LBRACKET",
            "DIK_LCONTROL",
            "DIK_LEFT",
            "DIK_LMENU",
            "DIK_LSHIFT",
            "DIK_LWIN",
            "DIK_M",
            "DIK_MINUS",
            "DIK_MULTIPLY",
            "DIK_N",
            "DIK_NEXT",
            "DIK_NOCONVERT",
            "DIK_NUMLOCK",
            "DIK_NUMPAD0",
            "DIK_NUMPAD1",
            "DIK_NUMPAD2",
            "DIK_NUMPAD3",
            "DIK_NUMPAD4",
            "DIK_NUMPAD5",
            "DIK_NUMPAD6",
            "DIK_NUMPAD7",
            "DIK_NUMPAD8",
            "DIK_NUMPAD9",
            "DIK_NUMPADCOMMA",
            "DIK_NUMPADENTER",
            "DIK_NUMPADEQUALS",
            "DIK_O",
            "DIK_P",
            "DIK_PAUSE",
            "DIK_PERIOD",
            "DIK_PRIOR",
            "DIK_Q",
            "DIK_R",
            "DIK_RBRACKET",
            "DIK_RCONTROL",
            "DIK_RETURN",
            "DIK_RIGHT",
            "DIK_RMENU",
            "DIK_RSHIFT",
            "DIK_RWIN",
            "DIK_S",
            "DIK_SCROLL",
            "DIK_SEMICOLON",
            "DIK_SLASH",
            "DIK_SPACE",
            "DIK_STOP",
            "DIK_SUBTRACT",
            "DIK_SYSRQ",
            "DIK_T",
            "DIK_TAB",
            "DIK_U",
            "DIK_UNDERLINE",
            "DIK_UNLABELED",
            "DIK_UP",
            "DIK_V",
            "DIK_W",
            "DIK_X",
            "DIK_Y",
            "DIK_YEN",
            "DIK_Z"});
            this.comboBox2.Location = new System.Drawing.Point(110, 91);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(185, 21);
            this.comboBox2.TabIndex = 12;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(12, 141);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(165, 17);
            this.checkBox3.TabIndex = 13;
            this.checkBox3.Text = "MUH ATMOSPHERES mode";
            this.toolTip1.SetToolTip(this.checkBox3, "Disables IRC functionality on interior maps when checked");
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(385, 197);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}