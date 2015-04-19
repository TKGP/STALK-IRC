using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STALK_IRC
{
    public partial class UpdateForm : Form
    {
        // Prevents window stealing focus after startup
        private bool focus = false;
        protected override bool ShowWithoutActivation
        {
            get { return focus; }
        }

        public UpdateForm(string notes, bool setFocus, bool force)
        {
            InitializeComponent();
            webBrowser1.DocumentText = notes;
            focus = setFocus;
            if (force)
            {
                this.Text = "Mandatory Update";
                label1.Text = "A mandatory update is available!  Click Download to close STALK-IRC and open the link in your browser.";
                button2.Text = "Exit";
                toolTip1.SetToolTip(button2, "Shuts down STALK-IRC without downloading the update. You will be reminded on next startup.");
            }
            else
            {
                this.Text = "Optional Update";
                label1.Text = "An optional update is available!  Click Download to close STALK-IRC and open the link in your browser.";
                button2.Text = "Skip";
                toolTip1.SetToolTip(button2, "Skips the update temporarily. You will be reminded on next startup.");
            }
        }

        private void UpdateForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            new HelpForm(0).ShowDialog();
        }
    }
}
