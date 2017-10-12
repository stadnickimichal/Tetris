using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{
    class Tetris:Form
    {
        private BoardTetris board = new BoardTetris();
        private BoardTetris NextFigureBoard = new BoardTetris(new Point(600,100),4,4);
        private StartMenu StartM = new StartMenu();
        private FigureTetris figure = new FigureTetris();
        private FigureTetris NextFigure = new FigureTetris();

        private Timer t = new Timer();
        private Button btn1 = new Button();
        private Label points= new Label(), pointsLabel= new Label(), info= new Label();

        private bool FastMode = false;
        private bool Pause = false;
        private bool GameOn = false;

        public Tetris()
        {
            Width = 800;
            Height = 540;
            t.Interval = 500;
            KeyPreview = true;
            /*Inicjalizacja labeli*/
            string infoLabelText = "E - Start\n"
                                + "Esc - Koniec\n"
                                + "Spacja - Pauza\n"
                                + "<- -> - lewo / prawo\n"
                                + "strzalka w gore - obrot w prawo\n\n"
                                + "strzalka w dol - przyspieszenie\n"
                                + " (nacisnac raz zeby przyspieszyc\n"
                                + " i drugi raz zeby zwolnic)\n\n"
                                + "W / Q - obrót w prawo / lewo";
            InicializeLabel(points, "0", new Point(650, 250));
            InicializeLabel(pointsLabel, "Punkty :", new Point(600, 250));
            InicializeLabel(info, infoLabelText, new Point(600, 300));
            /*inicjalizacja paneli*/
            Controls.Add(StartM);
            Controls.Add(board);
            Controls.Add(NextFigureBoard);
            StartM.Visible = true;
            board.Visible = false;
            NextFigureBoard.Visible = false;
            /*Dolanczanie obslugi zdarzen*/
            StartM.btnStartHendler().Click += btnStart_Click;
            StartM.btnQuitHendler().Click += btnQuit_Click;
            Paint += Plansza_PaintBoard;
            t.Tick += t_Tick;
            KeyDown += Tetris_KeyDown;
        }
        private void InicializeLabel(Label Label, string text, Point UpperLeftCorner)
        {
            Label.Text = text;
            Label.Location = UpperLeftCorner;
            Label.AutoSize = true;
            Controls.Add(Label);
        }
        private void Plansza_PaintBoard(object sender, PaintEventArgs e)
        {
            if (board.Visible)
            {
                board.DrowFigure(figure);
            }
            if (NextFigureBoard.Visible)
            {
                NextFigureBoard.DrowFigure(NextFigure);
            }
        }
        private void t_Tick(object sender, EventArgs e)
        {
            if (!board.MoveDown(figure))
            {
                if (!board.GameLost())
                {
                    int NewPoints = board.CheckLines();
                    points.Text = (int.Parse(points.Text) + NewPoints).ToString();
                    board.NewFigure();
                    figure.NewFigure(NextFigure);
                    board.DrowFigure(figure);
                    NextFigureBoard.ClearFigure(NextFigure);
                    NextFigure.NewFigure();
                    NextFigureBoard.DrowFigure(NextFigure);
                    FastModeOff();
                }
                else
                {
                    EndGame();
                }
                
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartM.Visible = false;
            board.Visible = true;
            NextFigureBoard.Visible = true;
        }
        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Tetris_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E)
            {
                t.Start();
                GameOn = true;
            }
            else if ((e.KeyCode == Keys.Right) && (!FastMode) && (!Pause) && (GameOn)) board.MoveRight(figure);
            else if ((e.KeyCode == Keys.Left) && (!FastMode) && (!Pause) && (GameOn)) board.MoveLeft(figure);
            else if ((e.KeyCode == Keys.Down) && (!FastMode) && (!Pause) && (GameOn)) FastModeOn();
            else if ((e.KeyCode == Keys.Down) && (FastMode) && (!Pause) && (GameOn)) FastModeOff();
            else if ((e.KeyCode == Keys.Space) && (!Pause) && (GameOn)) PauseOn();
            else if ((e.KeyCode == Keys.Space) && (Pause) && (GameOn)) PauseOff();
            else if ((e.KeyCode == Keys.W) && (!FastMode) && (!Pause) && (GameOn)) figure = board.RotateFigure(figure, true);
            else if ((e.KeyCode == Keys.Q) && (!FastMode) && (!Pause) && (GameOn)) figure = board.RotateFigure(figure, false);
            else if ((e.KeyCode == Keys.Escape) && (GameOn)) EndGame();
        }
        private void FastModeOn()
        {
            FastMode = true;
            t.Interval = 100;
        }
        private void FastModeOff()
        {
            FastMode = false;
            t.Interval = 500;
        }
        private void PauseOn()
        {
            Pause = true;
            t.Stop();
        }
        private void PauseOff()
        {
            Pause = false;
            t.Start();
        }
        private void EndGame()
        {
            t.Stop();
            GameOn = false;
            MessageBox.Show("Gra śkończona!\nTwój wynik to: " + points.Text);
        }
    }
}
