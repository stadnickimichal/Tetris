using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tetrisv3
{
    class StartMenu : Panel
    {
        Button btnStart = new Button();
        Button btnQuit = new Button();

        public StartMenu()
        {
            Width = 300;
            Height = 300;
            Location = new Point(175, 100);
            BorderStyle = BorderStyle.FixedSingle;
            /*Ustawianie przyciskow*/
            SetButton(btnStart, "Start Game", new Point((300 - btnStart.Width) / 2, 100));
            SetButton(btnQuit, "Quit", new Point((300 - btnStart.Width) / 2, 150));
            Controls.Add(btnStart);
            Controls.Add(btnQuit);
        }
        private void SetButton(Button btn, String text, Point UpperLeftCorner)
        {
            btn.Text = text;
            btn.Location = UpperLeftCorner;
        }
        public Button btnStartHendler() => btnStart;
        public Button btnQuitHendler() => btnQuit;
    }
}
