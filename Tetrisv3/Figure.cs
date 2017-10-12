using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{
    class Figure
    {
        public Color color { get; set; }

        public int x { get; set; } = 5;
        public int y { get; set; } = 0;
        protected bool[,] figure = new bool[4, 4];
        protected int _FigureWidth = 0, _FigureHeight = 0;

        public bool this[int i, int j]
        {
            get
            {
                return figure[i, j];
            }
            set
            {
                figure[i, j] = value;
            }
        }
        public int FigureWidth
        {
            get
            {
                return _FigureWidth;
            }
        }
        public int FigureHeight
        {
            get
            {
                return _FigureHeight;
            }
        }
        protected void ClearFigure()
        {
            for (int i = 0; i < _FigureWidth; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (figure[i, j]) figure[i, j] = false;
                }
            }
        }
        protected virtual bool[,] CreateFigure()
        {
            return new bool[2, 2];
        }
        protected void CalculateFigureDementions(int maxWidth, int maxHeight)
        {
            _FigureHeight = 0;
            _FigureWidth = 0;
            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    if ((figure[i, j]) && (_FigureHeight < j + 1)) _FigureHeight = j + 1;
                    if ((figure[j, i]) && (_FigureWidth < j + 1)) _FigureWidth = j + 1;
                }
            }
        }
    }
}
