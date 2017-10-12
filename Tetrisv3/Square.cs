using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{
    class Square
    {
        private Rectangle rectangle;
        private Color BackGroundColor;
        public Color Color { get; set; } = Color.Black;
        public const int Size = 25;

        public bool Occupied { get; set; } = false;

        public Square(int x, int y)
        {
            rectangle = new Rectangle(x * Size, y * Size, Size, Size);
            BackGroundColor = Color.White;
        }
        public Square(int x, int y, Color color)
        {
            rectangle = new Rectangle(x * Size, y * Size, Size, Size);
            this.BackGroundColor = color;
        }
        public void DrowNet(Graphics g, Color color)
        {
            SolidBrush Brush = new SolidBrush(color);
            g.FillRectangle(Brush, rectangle);
            g.DrawRectangle(Pens.White, rectangle);
            Occupied = true;
            Color = color;
        }
        public void ClearNet(Graphics g)
        {
            SolidBrush Brush = new SolidBrush(BackGroundColor);
            g.FillRectangle(Brush, rectangle);
            g.DrawRectangle(Pens.White, rectangle);
            Occupied = false;
        }
    }
}
