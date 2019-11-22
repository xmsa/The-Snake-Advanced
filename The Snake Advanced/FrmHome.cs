using System;
using System.Windows.Forms;

namespace The_Snake_Advanced
{
    public partial class FrmHome : Form
    {
        public bool playAgain { get; set; }
        public FrmHome()
        {
            InitializeComponent();
            this.Hide();
            playAgain = true;
            while (playAgain)
            {
                new FrmMain(this).ShowDialog();
            }
            timerExit.Enabled = true;
        }

        private void TimerExit_Tick(object sender, EventArgs e)
        {
            if (!playAgain)
            {
                Application.Exit();
            }
        }
    }
}
