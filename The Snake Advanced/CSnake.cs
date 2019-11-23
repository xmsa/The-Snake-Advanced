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
        Size _size;
        public Size size
        {
            get
            {
                return _size;
            }
            set
            {
                this._size = value;
                int len = body.Count;
                if (len != 0)
                {
                    _locationHead = new Point(300, 300);
                    foreach (var item in body)
                    {
                        frmMain.Controls.Remove(item);
                    }
                    body = new List<PictureBox>();
                    for (int i = 0; i < len; i++)
                    {
                        AddBody(FrmMain.laws.Overfly);
                    }
                }
            }
        }
        Color _color;
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                this._color = value;
                for (int i = 0; i < body.Count - 1; i++)
                {
                    body[i].BackColor = color;
                }
            }
        }
        List<PictureBox> body { get; set; }
        public Keys key { get; set; }

        public CSnake(FrmMain frmMain, Point locationStart, Size size, Color color, Keys key)
        {
            body = new List<PictureBox>();
            this.frmMain = frmMain;
            this._locationHead = locationStart;
            this.size = size;
            this.color = color;
            this.key = key;
            AddBody(FrmMain.laws.Overfly);
        }

        public void MoveSnake(FrmMain.laws law, bool wall = false)
        {
            AddBody(law, wall);

            if (frmMain.gameOver)
                return;

            frmMain.Controls.Remove(body[0]);

            body.RemoveAt(0);

            if (body.Count != 0)
            {
                body[body.Count - 1].BackColor = Color.Gray;
            }
        }

        public void AddBody(FrmMain.laws law, bool wall = false)
        {

            _locationHead = SetLocationHead(law);
            if (wall)
            {
                LowWall(_locationHead);
            }

            if (frmMain.gameOver)
            {
                return;
            }

            PictureBox tail = new PictureBox();
            tail.Size = size;
            tail.BackColor = Color.Gray;
            tail.Location = locationHead;
            if (body.Count != 0)
            {
                body[body.Count - 1].BackColor = color;
            }
            body.Add(tail);
            frmMain.Controls.Add(tail);
        }

        private Point SetLocationHead(FrmMain.laws law)
        {
            Point location = NextLocation(locationHead);

            switch (law)
            {
                case FrmMain.laws.Overfly:
                    location = LowOverfly(location);
                    break;
                case FrmMain.laws.CuttingSnake:
                    location = LowCuttingSnake(location);
                    location = LowOverfly(location);
                    break;
                case FrmMain.laws.NoCuttingSnake:
                    location = LowNoCuttingSnake(location);
                    location = LowOverfly(location);
                    break;
                case FrmMain.laws.ReturnOnSnake:
                    location = LowReturnOnSnake(location);
                    location = NextLocation(locationHead);
                    break;
            }
            return location;
        }

        private Point LowReturnOnSnake(Point location)
        {
            if ((location.X < 0) ||
                (location.Y > frmMain.Height) ||
                (location.X > frmMain.Width) ||
                (location.Y < 0))
            {
                key = SwitchKey(key);
            }
            return NextLocation(location);
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
            int indexBody = Colision(location);
            if (indexBody > 0)
            {
                frmMain.gameOver = true;
            }
            return location;
        }

        private int Colision(Point location)
        {
            for (int i = body.Count - 2; i >= 0; i--)
            {
                if (location == body[i].Location)
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
                frmMain.gameOver = true;
            }
        }

        private Point LowCuttingSnake(Point location)
        {
            int indexBody = Colision(location);
            if (indexBody > 0)
            {
                for (int i = indexBody; i >= 0; i--)
                {
                    frmMain.Controls.Remove(body[i]);
                    body.RemoveAt(i);
                }
                frmMain.progressBarLevel.Value = 0;
            }
            return location;
        }

        private Point NextLocation(Point location)
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

        private Point LowOverfly(Point location)
        {
            if (location.X < 0)
            {
                location.X = frmMain.Width - size.Width;
            }
            else if (location.Y > frmMain.Height)
            {
                location.Y = 0;
            }
            else if (location.X > frmMain.Width)
            {
                location.X = 0;
            }
            else if (location.Y < 0)
            {
                location.Y = frmMain.Height - size.Height;
            }
            return location;
        }

        public bool EatFood(Point foodLocation, bool sFood=false)
        {
            Point _pointHead1 = new Point(_locationHead.X,_locationHead.Y);
            Point _pointHead2 = new Point(_locationHead.X-size.Width,_locationHead.Y);
            Point _pointHead3 = new Point(_locationHead.X, _locationHead.Y - (size.Height));
            Point _pointHead4 = new Point(_locationHead.X - (size.Width),_locationHead.Y-size.Height);
            

            if (foodLocation == _pointHead1)
            {
                return true;
            }
            if (sFood&&(foodLocation == _pointHead1|| foodLocation == _pointHead3 || foodLocation == _pointHead4))
            {
                return true;
            }
            return false;
        }
    }
}
