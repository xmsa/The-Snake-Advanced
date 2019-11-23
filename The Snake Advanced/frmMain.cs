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
        CFood food;
        CFood sFood;
        Color bodyColor;
        enum number { zero, one, two, three, four, five, six, seven, eight, nine };
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
            bodyColor = Color.Green;
            this.BackColor = Color.DarkRed;

            playAgainToolStripMenuItem.Enabled = false;
            settingToolStripMenuItem.Enabled = false;


            timerShiftStarFood.Interval = 1000;
            counterStarFood = counterStarFood + 1;
            snake = new CSnake(this, new Point(300, 300),
                new Size((trBarSnakeSize.Value + 1) * 5, (trBarSnakeSize.Value + 1) * 5
                ), Color.Red, Keys.Right);
            food = new CFood(this, new Size((trBarSnakeSize.Value + 1) * 5, (trBarSnakeSize.Value + 1) * 5));
            sFood = new CFood(this, new Size(snake.size.Width * 2, snake.size.Height * 2), true);

        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHome.playAgain = false;
            Application.Exit();
        }

        private void PlayStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHome.playAgain = true;
            this.Close();
        }

        private void TimerMoveSnake_Tick(object sender, EventArgs e)
        {
            snake.MoveSnake(law, chBoxWall.Checked);
            int addLevel = 0;
            if (snake.EatFood(food.location))
            {
                snake.AddBody(law);
                food.RandomLocation();
                addLevel = progressBarLevel.Value + 5;
            }
            if (snake.EatFood(sFood.location, true))
            {
                snake.AddBody(law);
                snake.AddBody(law);
                sFood.RandomLocation();
                addLevel = progressBarLevel.Value + 10;

            }
            if (addLevel >= 100)
            {
                progressBarLevel.Value = 100;
                MessageBox.Show("Finishing");
                frmHome.playAgain = true;
                this.Close();
            }
            if (addLevel > 0 && addLevel < 100)
            {
                progressBarLevel.Value = addLevel;
            }
            if (gameOver)
            {
                timerMoveSnake.Enabled = false;
                if (MessageBox.Show("Play Again", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
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
            law = (laws)coBoxLaw.SelectedIndex;

            panelSetting.Visible = false;
            playAgainToolStripMenuItem.Enabled = true;
            settingToolStripMenuItem.Enabled = true;

            timerMoveSnake.Interval = 250 - (trBarSnakeSpeed.Value * 40);
            timerShiftFood.Interval = trBarSnakeSpeed.Value * 2000;

            timerShiftFood.Enabled = !chBoxFoodShiftSpeed.Checked;
            timerShiftStarFood.Enabled = true;
            timerMoveSnake.Enabled = true;
            this.Focus();
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

        private void CoBoxSnakeColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            bodyColor = Color.White;
            switch (coBoxSnakeColor.SelectedItem)
            {
                case "Red":
                    bodyColor = Color.Red;
                    break;
                case "Blue":
                    bodyColor = Color.Blue;
                    break;
                case "Green":
                    bodyColor = Color.Green;
                    break;
            }
            try
            {
                snake.color = bodyColor;
            }
            catch (Exception)
            {

            }
        }

        private void ChBoxFoodShiftSpeed_CheckedChanged(object sender, EventArgs e)
        {
            trBarFoodShiftSpeed.Enabled = !chBoxFoodShiftSpeed.Checked;
        }

        private void TrBarSnakeSize_ValueChanged(object sender, EventArgs e)
        {
            snake.size = new Size((trBarSnakeSize.Value + 1) * 5, (trBarSnakeSize.Value + 1) * 5);
            food.size = new Size((trBarSnakeSize.Value + 1) * 5, (trBarSnakeSize.Value + 1) * 5);
            sFood.size = new Size(snake.size.Width * 2, snake.size.Height * 2);
            sFood.RandomLocation();
            food.RandomLocation();

        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                case Keys.NumPad8:
                    snake.key = (snake.key != Keys.Down) ? Keys.Up : Keys.Down;
                    break;
                case Keys.Down:
                case Keys.S:
                case Keys.NumPad2:
                    snake.key = (snake.key != Keys.Up) ? Keys.Down : Keys.Up;
                    break;
                case Keys.Left:
                case Keys.A:
                case Keys.NumPad4:
                    snake.key = (snake.key != Keys.Right) ? Keys.Left : Keys.Right;
                    break;
                case Keys.Right:
                case Keys.D:
                case Keys.NumPad6:
                    snake.key = (snake.key != Keys.Left) ? Keys.Right : Keys.Left;
                    break;
            }
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelSetting.Visible = true;

            playAgainToolStripMenuItem.Enabled = false;
            settingToolStripMenuItem.Enabled = false;

            timerShiftFood.Enabled = false;
            timerShiftStarFood.Enabled = false;
            timerMoveSnake.Enabled = false;
            btnPlay.Focus();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag1 = timerMoveSnake.Enabled;
            bool flag2 = timerShiftFood.Enabled;
            bool flag3 = timerShiftStarFood.Enabled;


            timerMoveSnake.Stop();
            timerShiftFood.Stop();
            timerShiftStarFood.Stop();
            new FrmAbout().ShowDialog();
            if (flag1)
                timerMoveSnake.Start();
            if (flag2)
                timerShiftFood.Start();
            if (flag3)
                timerShiftStarFood.Start();
        }
    }
}
