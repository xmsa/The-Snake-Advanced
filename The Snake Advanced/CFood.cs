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
        public Size size
        {
            get
            {
                return food.Size;
            }
            set
            {
                food.Size = value;
            }
        }
        public bool visible
        {
            get
            {
                return food.Visible;
            }
            set
            {
                food.Visible = value;
            }
        }
        public Point location { get { return food.Location; } }
        public bool sFood { get; set; }

        PictureBox food { get; set; }

        public CFood(FrmMain frmMain, Size size, bool sFood = false)
        {
            this.frmMain = frmMain;
            this.sFood = sFood;

            CreateFood(size);
        }

        void CreateFood(Size size)
        {
            food = new PictureBox();
            food.Size = size;
            food.BackgroundImageLayout = ImageLayout.Zoom;
            RandomLocation();
            frmMain.Controls.Add(food);
        }
        public void RandomLocation()
        {
            Random rnd = new Random();
            int width = rnd.Next(1, (frmMain.Width / size.Width) - 1);
            int height = rnd.Next(1, ((frmMain.Height - 24) / size.Height) - 1);
            food.BackColor = Color.Transparent;

            if (sFood)
            {
                food.BackgroundImage = Properties.Resources.FoodHeart;
            }
            else
            {
                switch (rnd.Next(1,8))
                {
                    case 1:
                        food.BackgroundImage = Properties.Resources.FoodA;
                        break;
                    case 2:
                        food.BackgroundImage = Properties.Resources.FoodB;
                        break;
                    case 3:
                        food.BackgroundImage = Properties.Resources.FoodC;
                        break;
                    case 4:
                        food.BackgroundImage = Properties.Resources.FoodD;
                        break;
                    case 5:
                        food.BackgroundImage = Properties.Resources.FoodE;
                        break;
                    case 6:
                        food.BackgroundImage = Properties.Resources.FoodF;
                        break;
                    case 7:
                        food.BackgroundImage = Properties.Resources.FoodG;
                        break;
                }
            }
            food.Location = new Point(width * size.Width, height * size.Height);
        }

    }
}
