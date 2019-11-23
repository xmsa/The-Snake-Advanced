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
        Color bodyColor;
        enum number { zero, one , two ,three ,four, five , six , seven , eight , nine };
        number counterStarFood = number.zero;
        //Constructor Form frmMain
        public FrmMain(FrmHome frmHome)
        {
            InitializeComponent();
            //panelSetting.Visible = false;
            this.Focus();
            frmHome.playAgain = false;
            this.frmHome = frmHome;
            gameOver = false;
            coBoxLaw.SelectedIndex = 0;
            coBoxSnakeColor.SelectedIndex = 0;
            coBoxBackGroundColor.SelectedIndex = 0;
            bodyColor=Color.Green;
            this.BackColor=Color.DarkRed;
            menuStrip.Enabled = false;
            timerShiftStarFood.Interval = 1000;
            counterStarFood = counterStarFood + 1;
            snake = new CSnake(this, new Point(50, 50), new Size(10, 10), Color.Green, Keys.Right);
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

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            law =(laws) coBoxLaw.SelectedIndex;

            panelSetting.Visible = false;
            menuStrip.Enabled = true;

            timerMoveSnake.Interval = 250-(trBarSnakeSpeed.Value*40);
            timerShiftFood.Interval = trBarSnakeSpeed.Value*2000;

            timerShiftFood.Enabled = !chBoxWall.Checked;
            timerShiftStarFood.Enabled = true;
            timerMoveSnake.Enabled = true;
        }

        private void CoBoxBackGroundColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (coBoxBackGroundColor.SelectedItem)
            {
                case "Red":
                    BackColor = Color.DarkRed;
                    break;
                case "Blue":
                    BackColor = Color.DarkBlue;
                    break;
                case "Green":
                    BackColor = Color.DarkGreen;
                    break;
            }
        }
    }
}
