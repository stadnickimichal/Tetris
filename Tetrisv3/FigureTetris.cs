using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{

    public enum shapes { triangle, square, Lshape, reverseLshape, Zshape, reversZshape, Bar }

    class FigureTetris : Figure
    {
        public shapes FigureShape { get; set; }

        public FigureTetris()
        {
            NewFigure();
        }
        public void NewFigure(FigureTetris fig)
        {
            ClearFigure();
            color = fig.color;
            FigureShape = fig.FigureShape;
            figure = CreateFigure();
            CalculateFigureDementions(4, 4);
        }
        public bool RotateClockwise(bool clockwise)
        {
            bool[,] PrevFig = new bool[4, 4];
            PrevFig = (bool[,])figure.Clone();
            ClearFigure();
            /*zamieniamy Height na Width w pętlach for*/
            for (int i = 0; i < _FigureHeight; i++)
            {
                for (int j = 0; j < _FigureWidth; j++)
                {
                    figure[i, j] = (clockwise) ? PrevFig[j, _FigureHeight - 1 - i] : PrevFig[_FigureWidth - 1 - j, i];
                }
            }
            CalculateFigureDementions(4, 4);
            return true;
        }
        public void NewFigure()
        {
            ClearFigure();
            color = RandomColor();
            FigureShape = RandomShape();
            figure = CreateFigure();
            CalculateFigureDementions(4, 4);
        }
        protected override bool[,] CreateFigure()
        {
            bool[,] tab = new bool[4, 4];
            switch (FigureShape)
            {
                case shapes.triangle:
                    tab[0, 2] = true; tab[1, 2] = true; tab[2, 2] = true; tab[1, 1] = true;
                    break;
                case shapes.square:
                    tab[0, 0] = true; tab[1, 0] = true; tab[0, 1] = true; tab[1, 1] = true;
                    break;
                case shapes.Lshape:
                    tab[0, 0] = true; tab[0, 1] = true; tab[0, 2] = true; tab[1, 2] = true;
                    break;
                case shapes.reverseLshape:
                    tab[1, 0] = true; tab[1, 1] = true; tab[1, 2] = true; tab[0, 2] = true;
                    break;
                case shapes.Zshape:
                    tab[0, 0] = true; tab[1, 0] = true; tab[1, 1] = true; tab[2, 1] = true;
                    break;
                case shapes.reversZshape:
                    tab[0, 1] = true; tab[1, 1] = true; tab[1, 0] = true; tab[2, 0] = true;
                    break;
                case shapes.Bar:
                    tab[0, 0] = true; tab[0, 1] = true; tab[0, 2] = true; tab[0, 3] = true;
                    break;
            }
            return tab;
        }
        private shapes RandomShape()
        {
            Random random = new Random();
            int RandomNumber = random.Next(7);
            shapes shape;
            switch (RandomNumber)
            {
                case 0:
                    shape = shapes.Bar;
                    break;
                case 1:
                    shape = shapes.Lshape;
                    break;
                case 2:
                    shape = shapes.reverseLshape;
                    break;
                case 3:
                    shape = shapes.reversZshape;
                    break;
                case 4:
                    shape = shapes.square;
                    break;
                case 5:
                    shape = shapes.triangle;
                    break;
                case 6:
                    shape = shapes.Zshape;
                    break;
                default:
                    shape = shapes.Bar;
                    break;
            }
            return shape;
        }
        private Color RandomColor()
        {
            Random random = new Random();
            int RandomNumber = random.Next(7);
            Color color;
            switch (RandomNumber)
            {
                case 0:
                    color = Color.Black;
                    break;
                case 1:
                    color = Color.Green;
                    break;
                case 2:
                    color = Color.Yellow;
                    break;
                case 3:
                    color = Color.Blue;
                    break;
                case 4:
                    color = Color.Red;
                    break;
                case 5:
                    color = Color.Brown;
                    break;
                case 6:
                    color = Color.Orange;
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            return color;
        }
        
    }
}
