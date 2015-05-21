using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GoUI.Util;

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
                }
            }
        }

        /// <summary>
        /// Detaches this control, clearing and removing any references/bindings it has created
        /// </summary>
        public void Detach()
        {
            this.Clear();

            foreach (IUIControl child in this.BoardRoot.Children)
            {
                child.Detach();
            }
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

                    if (row.Tag is GridBoardType && ((GridBoardType)row.Tag <= this.BoardType))
                    {
                        int y;

                        for (y = 0; y < boardSize; y++)
                        {
                            GridPointControl ctrl = new GridPointControl(new Point(x, y));

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
        }

        /// <summary>
        /// Redraw all placers/pieces on the board with data from the Wrapped Kernel Board
        /// </summary>
        private void Update()
        {
            // Run through each row of the board and add to it  
            foreach (GridBoardRow row in this.BoardRoot.Children)
            {

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

        /// <summary>
        /// Property for BoardType property
        /// </summary>
        public static readonly DependencyProperty BoardTypeProperty = DependencyProperty.Register("BoardType", 
            typeof(GridBoardType), typeof(GridBoardControl), new PropertyMetadata(GridBoardType.e9x9,
            new PropertyChangedCallback(OnBoardTypeChanged)));

        #endregion End of Members
    }
}
