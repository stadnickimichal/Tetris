using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{
    class Board : Panel
    {
        protected Square[,] Net;
        protected Graphics g;
        protected Point LeftUpperCorner = new Point(150, 30);
        /*Szerokość i wysokość planszy w ilości kratek*/
        protected int GridWidth = 10, GridHeight = 18;
        /*X, Y lewego górnego rogu sterowanej figóry*/
        public int x_Figure { get; set; }
        public int y_Figure { get; set; }

        public Board()
        {
            Net = new Square[GridWidth, GridHeight];
            for (int i = 0; i < GridWidth; i++)
            {
                for (int j = 0; j < GridHeight; j++)
                {
                    Net[i, j] = new Square(i, j);
                }
            }
            Location = LeftUpperCorner;
            Size = new Size(GridWidth * Square.Size, GridHeight * Square.Size);
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;
            x_Figure = 5;
            y_Figure = 0;
            g = CreateGraphics();
        }
        public Board(Point point) : this()
        {
            Location = point;
        }
        public Board(Point point, int width, int height) : this(point)
        {
            GridWidth = width;
            GridHeight = height;
            Net = new Square[GridWidth, GridHeight];
            for (int i = 0; i < GridWidth; i++)
            {
                for (int j = 0; j < GridHeight; j++)
                {
                    Net[i, j] = new Square(i, j);
                }
            }
            Size = new Size(width * Square.Size, height * Square.Size);
            x_Figure = 0;
            y_Figure = 0;
        }

        public void ClearFigure(FigureTetris fig)
        {
            for (int i = 0; i < fig.FigureWidth; i++)
            {
                for (int j = 0; j < fig.FigureHeight; j++)
                {
                    if (fig[i, j])
                    {
                        Net[x_Figure + i, y_Figure + j].ClearNet(g);
                        Net[x_Figure + i, y_Figure + j].Occupied = false;
                    }
                }
            }
        }
        public void DrowFigure(FigureTetris fig)
        {
            for (int i = 0; i < fig.FigureWidth; i++)
            {
                for (int j = 0; j < fig.FigureHeight; j++)
                {
                    if (fig[i, j])
                    {
                        Net[x_Figure + i, y_Figure + j].DrowNet(g, fig.color);
                        Net[x_Figure + i, y_Figure + j].Occupied = true;
                    }
                }
            }
        }
        protected bool Y_MaxIndexNotCrossed(int y) => ((y >= 0) && (y < GridHeight));
        protected bool X_MinIndexNotCrossed(int x) => ((x >= 0));
        protected bool X_MaxIndexNotCrossed(int x) => ((x < GridWidth));
    }
}
