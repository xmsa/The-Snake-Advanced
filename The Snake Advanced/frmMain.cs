using System;
using System.Drawing;
using System.Windows.Forms;

namespace The_Snake_Advanced
{
    public partial class FrmMain : Form
    {
        FrmHome frmHome;
        public enum laws { Overfly, CuttingSnake, NoCuttingSnake, ReturnOnSnake }
        laws law { get; set; }
        public bool gameOver { get; set; }

        //Constructor Form frmMain
        public FrmMain(FrmHome frmHome)
        {
            InitializeComponent();
            panelSetting.Visible = false;
            this.Focus();
            frmHome.playAgain = false;
            this.frmHome = frmHome;
            gameOver = false;
            CSnake n = new CSnake(this, new Point(50, 50), new Size(10, 10), Color.Green, Keys.Right);
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
