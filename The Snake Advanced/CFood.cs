using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Snake_Advanced
{
    /// <summary>
    /// Create food and shift location
    /// </summary>
    /// 
    class CFood
    {
        FrmMain frmMain;
        Size size { get; set; }
        public bool sFood { get; set; }
        public PictureBox food { get; set; }

        public CFood(FrmMain frmMain, Size size, bool sFood = false)
        {
            this.frmMain = frmMain;
            this.size = size;
            this.sFood = sFood;

            CreateFood();
        }

        void CreateFood()
        {
            food = new PictureBox();
            food.Size = size;

            food.BackColor = Color.Red;
            RandomLocation();
            frmMain.Controls.Add(food);
        }
        public void RandomLocation()
        {
            Random rnd = new Random();
            int width = rnd.Next(1, (frmMain.Width / size.Width) - 1);
            int height = rnd.Next(1, ((frmMain.Height - 24) / size.Height) - 1);
            food.Location = new Point(width * size.Width, height * size.Height);
        }
    }
}
