using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GoLibrary;

namespace GoUI
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow(int boardSize)
        {
            InitializeComponent();

            this.SubmitBtn.Click += this.OnSubmitClicked;

            this.boardSize = boardSize;
            game = new Game_LIB(this.boardSize);

            this.Title = String.Format("{0} x {1}", this.boardSize, this.boardSize);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
             if (e.IsDown && e.Key == Key.Enter)
             {
                 this.MakePlay();
             }

 	         base.OnKeyDown(e);
        }

        private void MakePlay()
        {
            int x, y;

            if (!Int32.TryParse(this.XCoorTB.Text, out x))
            {
                x = -1;
                this.XCoorTB.Text = "Bad Number!";
            }

            if (!Int32.TryParse(this.YCoorTB.Text, out y))
            {
                y = -1;
                this.YCoorTB.Text = "Bad Number!";
            }

            try
            {         
                GoLibrary.ReturnCodes_LIB code = this.game.placeStone(x, y);
                MessageBox.Show(code.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                MessageBox.Show(this.GetBoardDisplay());
            }
        }

        private void OnSubmitClicked(Object sender, EventArgs e)
        {
            this.MakePlay();
        }

        private String GetBoardDisplay()
        {
            String display = String.Empty;
            Char s;

            if (this.game != null)
            {
                for (int x = 0; x < this.boardSize; x++)
                {
                    for (int y = 0; y < this.boardSize; y++)
                    {
                        GoLibrary.GoColor_LIB color = this.game.query(x, y);

                        if (color == GoColor_LIB.BLACK)
                        {
                            s = 'B';
                        }
                        else if (color == GoColor_LIB.WHITE)
                        {
                            s = 'W';
                        }
                        else
                        {
                            s = '-';
                        }

                        display += s;
                        display += '\t';
                    }

                    display += '\n';
                }
           
            }

            return display;
        }

        private int boardSize;
        private Game_LIB game;

    }
}

                       