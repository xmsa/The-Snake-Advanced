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
        CSnake snake;
        //Constructor Form frmMain
        public FrmMain(FrmHome frmHome)
        {
            InitializeComponent();
            panelSetting.Visible = false;
            this.Focus();
            frmHome.playAgain = false;
            this.frmHome = frmHome;
            gameOver = false;
            snake= new CSnake(this, new Point(50, 50), new Size(10, 10), Color.Green, Keys.Right);
            timerMoveSnake.Enabled = true;
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

        private void TimerMoveSnake_Tick(object sender, EventArgs e)
        {
            snake.MoveSnake(law);
            if (gameOver)
            {
                timerMoveSnake.Enabled = false;
                if (MessageBox.Show("Play Again","Game Over" ,MessageBoxButtons.YesNo , MessageBoxIcon.Error)==DialogResult.Yes)
                {
                    frmHome.playAgain = true;
                    this.Close();
                }
                else
                {
                    frmHome.playAgain = false;
                    Application.Exit();
                }
            }
        }
    }
}
