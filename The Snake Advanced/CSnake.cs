using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace The_Snake_Advanced
{

    /// <summary>
    ///Add / Move Body and Check eatting Food and Check Law
    /// </summary>
    ///
    class CSnake
    {
        FrmMain frmMain { get; set; }
        Point _locationHead;
        public Point locationHead { get { return _locationHead; } }
        Size size { get; set; }
        Color color { get; set; }
        List<PictureBox> body { get; set; }
        Keys key;

        public CSnake(FrmMain frmMain, Point locationStart, Size size, Color color, Keys key)
        {
            this.frmMain = frmMain;
            this._locationHead = locationStart;
            this.size = size;
            this.color = color;
            this.key = key;
            body = new List<PictureBox>();
            AddBody();
        }

        private void AddBody()
        {

            _locationHead = SetLocationHead();

            LowWall(_locationHead);

            if (frmMain.gameOver)
            {
                return;
            }

            PictureBox tail = new PictureBox();
            tail.Size = size;
            tail.BackColor = Color.Red;
            tail.Location = locationHead;
            if (body.Count>0)
            {
                body[body.Count - 1].BackColor = color;
            }
            body.Add(tail);
            frmMain.Controls.Add(tail);
        }

        private Point SetLocationHead()
        {
            Point location = locationHead;

            switch (frmMain.law)
            {
                case FrmMain.laws.Overfly:
                    location = LowOverfly(location);
                    break;
                case FrmMain.laws.CuttingSnake:
                    location = LowCuttingSnake(location);
                    break;
                case FrmMain.laws.NoCuttingSnake:
                    location = LowNoCuttingSnake(location);
                    break;
                case FrmMain.laws.ReturnOnSnake:
                    location = LowReturnOnSnake(location);
                    break;
            }
            return location;
        }

        private Point LowReturnOnSnake(Point location)
        {
            location = LowOverfly(location);
            if ((location.X < 0) ||
                (location.Y < frmMain.Height) ||
                (location.X < frmMain.Width)||
                (location.Y < 0))
            {
                key = SwitchKey(key);
            }
            return LowOverfly(location);
        }

        private Keys SwitchKey(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    return Keys.Down;
                case Keys.Down:
                    return Keys.Up;
                case Keys.Left:
                    return Keys.Right;
                case Keys.Right:
                    return Keys.Left;
                default:
                    return key;
            }
        }

        private Point LowNoCuttingSnake(Point location)
        {
            location = LowOverfly(location);
            int indexBody= Colision(location);
            if (indexBody<0)
            {
                frmMain.gameOver = true;
            }
            return location;
        }

        private int Colision(Point location)
        {
            for (int i = body.Count - 2; i >= 0; i--)
            {
                if (location== body[i].Location)
                {
                    return i;
                }
            }
            return -1;
        }

        private void LowWall(Point location)
        {
            if ((location.X < 0) ||
                (location.Y > frmMain.Height) ||
                (location.X > frmMain.Width) ||
                (location.Y < 0))
            {
                frmMain.gameOver=true;
            }
        }

        private Point LowCuttingSnake(Point location)
        {
            location = LowOverfly(location);
            int indexBody = Colision(location);
            if (indexBody < 0)
            {
                for (int i = indexBody; i >=0; i++)
                {
                    frmMain.Controls.Remove(body[i]);
                    body.RemoveAt(i);
                }
            }
            return location;
        }

        private Point LowOverfly(Point location)
        {
            switch (key)
            {
                case Keys.Up:
                    location.Y -= size.Height;
                    break;
                case Keys.Down:
                    location.Y += size.Height;
                    break;
                case Keys.Left:
                    location.X -= size.Width;
                    break;
                case Keys.Right:
                    location.X += size.Width;
                    break;
            }
            return location;
        }
    }
}
