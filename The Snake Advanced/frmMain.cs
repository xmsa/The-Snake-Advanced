using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Snake_Advanced
{
    public partial class FrmMain : Form
    {
        FrmHome frmHome;
        //Constructor Form frmMain
        public FrmMain(FrmHome frmHome)
        {
            InitializeComponent();
            panelSetting.Visible = false;
            this.Focus();
            frmHome.playAgain = false;
            this.frmHome = frmHome;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHome.playAgain = false;
            Application.Exit();
        }

        private void PlayStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHome.playAgain = true;
        }
    }
}
