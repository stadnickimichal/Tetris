using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{
    public delegate bool IndexNotCrossed(int x);

    class BoardTetris : Board
    {
        IndexNotCrossed IndexNotCrossed = null;

        public BoardTetris() :base()
        {
        }
        public BoardTetris(Point point) : base(point)
        {
        }
        public BoardTetris(Point point, int width, int height) : base(point,width,height)
        {
        }
        
        public void NewFigure()
        {
            x_Figure = GridWidth / 2;
            y_Figure = 0;
        }
        public bool MoveDown(FigureTetris fig)
        {
            bool MovePerformed = true;
            ClearFigure(fig);
            y_Figure++;
            IndexNotCrossed += Y_MaxIndexNotCrossed;
            if (!MovePassible(y_Figure + fig.FigureHeight - 1, fig))
            {
                MovePerformed = false;
                y_Figure--;
            }
            DrowFigure(fig);
            return MovePerformed;
        }
        public bool MoveRight(FigureTetris fig)
        {
            bool MovePerformed = true;
            ClearFigure(fig);
            x_Figure++;
            IndexNotCrossed += X_MaxIndexNotCrossed;
            if (!MovePassible(x_Figure + fig.FigureWidth-1, fig))// -1 poniewaz bierzemy wysokosc figury jako indeks tablicy a nie faktycznś wysokość (indeksowanie zaczyna się od 0)
            {
                MovePerformed = false;
                x_Figure--;
            }
            DrowFigure(fig);
            return MovePerformed;
        }
        public bool MoveLeft(FigureTetris fig)
        {
            bool MovePerformed = true;
            ClearFigure(fig);
            x_Figure--;
            IndexNotCrossed += X_MinIndexNotCrossed;
            if (!MovePassible(x_Figure, fig))
            {
                MovePerformed = false;
                x_Figure++;
            }
            DrowFigure(fig);
            return MovePerformed;
        }
        public FigureTetris RotateFigure(FigureTetris fig, bool Clacwise)
        {
            ClearFigure(fig);
            bool Rotated = (Clacwise) ? fig.RotateClockwise(true) : fig.RotateClockwise(false);
            if ((!Y_MaxIndexNotCrossed(y_Figure + fig.FigureHeight - 1)) || (!X_MaxIndexNotCrossed(x_Figure + fig.FigureWidth - 1)) || !X_MinIndexNotCrossed(x_Figure))
            {
                Rotated = (Clacwise) ? fig.RotateClockwise(false) : fig.RotateClockwise(true);
            }
            else if (FieldOccupied(x_Figure, y_Figure, fig))
            {
                Rotated = (Clacwise) ? fig.RotateClockwise(false) : fig.RotateClockwise(true);
            }
            DrowFigure(fig);
            return fig;
        }
        public int CheckLines()
        {
            int points =0;
            for(int i= 0; i< GridHeight; i++)
            {
                if (LineFull(i))
                {
                    DeleteLine(i);
                    MoveLinesDown(i-1);
                    points++;
                }
            }
            return points;
        }
        public bool GameLost()
        {
            for (int i = 0; i < GridWidth; i++)
            {
                if (Net[i, 0].Occupied) return true;
            }
            return false;
        }
        private bool MovePassible(int index, FigureTetris fig)
        {
            bool output=true;
            if (!IndexNotCrossed(index))
            {
                output = false;
            }
            else if (FieldOccupied(x_Figure, y_Figure, fig))
            {
                output = false;
            }
            IndexNotCrossed = null;
            return output;
        }
        private bool FieldOccupied(int x, int y, FigureTetris fig)
        {
            for (int i = 0; i < fig.FigureWidth; i++)
            {
                for (int j = 0; j < fig.FigureHeight; j++)
                {
                    if ((Net[x_Figure + i, y_Figure + j].Occupied) && (fig[i, j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool LineFull(int LineIndex)
        {
            for(int i=0; i<GridWidth; i++)
            {
                if (!Net[i, LineIndex].Occupied) return false;
            }
            return true;
        }
        private void MoveLinesDown(int BeginingLineIndex)
        {
            for(int i= BeginingLineIndex; i > 0; i--)
            {
                for (int j= 0; j < GridWidth; j++)
                {
                    if (Net[j, i].Occupied)
                    {
                        Net[j, i].ClearNet(g);
                        Net[j, i + 1].DrowNet(g, Net[j, i].Color);
                    }
                }
            }
        }
        private void DeleteLine(int LineIndex)
        {
            for (int j = 0; j < GridWidth; j++)
            {
                Net[j, LineIndex].ClearNet(g);
            }
        }
    }
}