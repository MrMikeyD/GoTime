using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GoUI.Util;
using GoLibrary;

namespace GoUI.Controls
{
    /// <summary>
    /// A control representing a grid game board of some type.
    /// </summary>
    public partial class GridBoardControl : UserControl, IUIControl
    {
        #region Constructors

        public GridBoardControl()
        {
            InitializeComponent();

            this.InitializeBoard();
        }

        #endregion End of Constructors

        #region Events

        /// <summary>
        /// Handles the event triggerd when the Board Type property changes
        /// </summary>
        private static void OnBoardTypeChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            GridBoardControl ctrl = depObj as GridBoardControl;

            if (ctrl != null)
            {
                ctrl.InitializeBoard();
            }
        }

        /// <summary>
        /// Handles the event triggered when the mouse is hovering over a point on the board
        /// </summary>
        private void OnPointMouseEnter(Object sender, MouseEventArgs e)
        {
            if (sender is GridPointControl)
            {
                // TODO: one day implement shadowing
            }
        }

        private void OnPointMouseLeave(Object sender, MouseEventArgs e)
        {
            if (sender is GridPointControl)
            {
              //  (sender as GridPointControl).Content = null;
            }
        }

        private void OnPointMouseUp(Object sender, MouseButtonEventArgs e)
        {
            if (sender is GridPointControl)
            {
                GridPointControl ctrl = sender as GridPointControl;

                if (this.kernelGame != null)
                {
                    ReturnCodes_LIB status = this.kernelGame.placeStone(ctrl.X, ctrl.Y);

                    this.ProcessStatus(status);
                }
            }
        }

        #endregion End of Events

        #region Methods

        /// <summary>
        /// Clears this control, returning it to a neutral state
        /// </summary>
        public void Clear()
        {
            if (this.BoardRoot.Children != null && this.BoardRoot.Children.Count > 0)
            {
                foreach (UIElement child in this.BoardRoot.Children)
                {
                    if (child is IUIControl)
                    {
                        (child as IUIControl).Clear();
                    }

                    if (child is GridBoardRow)
                    {
                        GridBoardRow row = child as GridBoardRow;
                        Boolean validBoardRow = (row.Tag is GridBoardType && (GridBoardType)row.Tag <= this.BoardType);

                        if (validBoardRow)
                        {
                            if (row.Children != null && row.Children.Count > 0)
                            {
                                if (child is GridPointControl)
                                {
                                    GridPointControl ctrl = child as GridPointControl;

                                    ctrl.MouseEnter -= this.OnPointMouseEnter;
                                    ctrl.MouseLeave -= this.OnPointMouseLeave;
                                    ctrl.MouseLeftButtonUp -= this.OnPointMouseUp;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Detaches this control, clearing and removing any references/bindings it has created
        /// </summary>
        public void Detach()
        {
            this.Clear();

            if (this.BoardRoot.Children != null && this.BoardRoot.Children.Count > 0)
            {
                foreach (UIElement child in this.BoardRoot.Children)
                {
                    if (child is IUIControl)
                    {
                        (child as IUIControl).Detach();
                    }
                }
            }

            if (this.kernelGame != null) this.kernelGame.Dispose();
        }

        /// <summary>
        /// Draw the initial Grid Lines with placers for each intersection point
        /// </summary>
        private void InitializeBoard()
        {
            this.Clear();

            // The board type enum value is the board's size
            int boardSize = (Int32)this.BoardType;
            int x = 0;

            // Run through each row of the board and add to it placers
            foreach (UIElement child in this.BoardRoot.Children)
            {
                if (child is GridBoardRow)
                {
                    GridBoardRow row = child as GridBoardRow;
                    row.Children.Clear();
                    Boolean validBoardRow = (row.Tag is GridBoardType && (GridBoardType)row.Tag <= this.BoardType);

                    if (validBoardRow)
                    {
                        int y;

                        for (y = 0; y < boardSize; y++)
                        {
                            GridPointControl ctrl = new GridPointControl(new Point(y, x));
                            ctrl.MouseEnter += this.OnPointMouseEnter;
                            ctrl.MouseLeave += this.OnPointMouseLeave;
                            ctrl.MouseLeftButtonUp += this.OnPointMouseUp;
                            ctrl.GamePiece = null; 

                            row.AddGridPoint(ctrl);
                        }

                        row.Columns = y;
                        row.Rows = 1;

                        x++;
                    }
                }
            }

            this.BoardRoot.Rows = boardSize;
            this.BoardRoot.Columns = 1;

            this.CreateInitialBoard();
        }

        /// <summary>
        /// Builds and initial board
        /// </summary>
        private void CreateInitialBoard()
        {
            if (this.kernelGame != null) this.kernelGame.Dispose();

            this.kernelGame = new Game_LIB((int)this.BoardType);
            this.Update();
        }

        /// <summary>
        /// Redraw all placers/pieces on the board with data from the Wrapped Kernel Board
        /// </summary>
        private void Update()
        {
            // At present, we are not smart enough to figure out the results of a play...so we just query every point on the board.
            // Will we ever know this kind of information? Who knows! Weeeeeeee!
            if (this.kernelGame != null)
            {
                // Run through each row of the board and redraw based on kernel query
                foreach (GridBoardRow row in this.BoardRoot.Children)
                {
                    foreach(UIElement child in row.Children)
                    {
                        if (child is GridPointControl)
                        {
                            GridPointControl ctrl = (child as GridPointControl);
                            System.Windows.Shapes.Ellipse gamePiece = null;

                            GoColor_LIB color = this.kernelGame.query(ctrl.X, ctrl.Y);

                            switch(color)
                            {
                                case GoColor_LIB.BLACK:
                                    {
                                        gamePiece = new System.Windows.Shapes.Ellipse();
                                        gamePiece.Fill = System.Windows.Media.Brushes.Black;
                                        gamePiece.Height = 40d;
                                        gamePiece.Width = 40d;
                                        break;
                                    }
                                case GoColor_LIB.WHITE:
                                    {
                                        gamePiece = new System.Windows.Shapes.Ellipse();
                                        gamePiece.Fill = System.Windows.Media.Brushes.White;
                                        gamePiece.Height = 40d;
                                        gamePiece.Width = 40d;
                                        break;
                                    }
                                case GoColor_LIB.NONE:
                                    {
                                        gamePiece = new System.Windows.Shapes.Ellipse();
                                        gamePiece.Fill = System.Windows.Media.Brushes.Transparent;
                                        gamePiece.Height = 40d;
                                        gamePiece.Width = 40d;
                                        break;
                                    }
                            }

                            (child as GridPointControl).Content = gamePiece;
                        }
                    }
                }
            }
        }

        private void ProcessStatus(ReturnCodes_LIB status)
        {
            switch(status)
            {
                case ReturnCodes_LIB.OK:
                    {
                        this.Update();
                        break;
                    }
                case ReturnCodes_LIB.ILLEGAL_KO:
                case ReturnCodes_LIB.OUT_OF_BOUNDS:
                case ReturnCodes_LIB.STONE_PRESENT:
                case ReturnCodes_LIB.SUICIDE:
                    {
                        MessageBox.Show(status.ToString());
                        break;
                    }
            }
        }

        #endregion End of Methods

        #region Properties

        /// <summary>
        /// Gets or sets the BoardType of this Board Control
        /// </summary>
        public GridBoardType BoardType
        {
            get { return (GridBoardType)this.GetValue(BoardTypeProperty); }
            set { this.SetValue(BoardTypeProperty, value); }
        }

        #endregion End of Properties

        #region Members

        public enum GridBoardType
        {
            e9x9 = 9,
            e13x13 = 13,
            e19x19 = 19
        };

        private Game_LIB kernelGame;

        /// <summary>
        /// Property for BoardType property
        /// </summary>
        public static readonly DependencyProperty BoardTypeProperty = DependencyProperty.Register("BoardType", 
            typeof(GridBoardType), typeof(GridBoardControl), new PropertyMetadata(GridBoardType.e9x9,
            new PropertyChangedCallback(OnBoardTypeChanged)));

        #endregion End of Members
    }
}
