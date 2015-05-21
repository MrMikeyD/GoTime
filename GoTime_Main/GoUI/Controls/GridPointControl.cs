using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GoUI.Util;

namespace GoUI.Controls
{
    public class GridPointControl : UserControl, IUIControl, IBoardPresenter
    {
        #region Constructors

        public GridPointControl(Point coordinate)
        {
            this.coordinate = coordinate;
        }

        #endregion End of Constructors

        #region Events

        /// <summary>
        /// Handles the event triggered when the GamePiece property changes
        /// </summary>
        private static void OnGamePieceChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            GridPointControl ctrl = depObj as GridPointControl;

            // Set the Content property to the GamePiece. ControlTemplate knows what to do with  this.
            if (ctrl != null)
            {
                ctrl.Content = ctrl.GamePiece;
            }
        }

        #endregion End of Events

        #region Methods

        /// <summary>
        /// Clears this control, returning it to a neutral state
        /// </summary>
        public void Clear()
        {
            this.coordinate = null;
        }

        /// <summary>
        /// Detaches this control, clearing and removing any references/bindings it has created
        /// </summary>
        public void Detach()
        {
            this.Clear();
        }

        #endregion End of Methods

        #region Properties

        /// <summary>
        /// Gets the X coordinate of this grid point. -1 if no coordinate was specified
        /// </summary>
        public Int32 X
        {
            get { return this.coordinate != null && this.coordinate.HasValue ? (Int32)this.coordinate.Value.X : -1; }
            set { if (this.coordinate != null && this.coordinate.HasValue) this.coordinate = new Point(value, this.Y); }
        }

        /// <summary>
        /// Gets the Y coordinate of this grid point. -1 if no coordinate was specified
        /// </summary>
        public Int32 Y
        {
            get { return this.coordinate != null && this.coordinate.HasValue ? (Int32)this.coordinate.Value.Y : -1; }
            set { if (this.coordinate != null && this.coordinate.HasValue) this.coordinate = new Point(this.X, value); }
        }

        public IBoardPlacer GamePiece { get; set; }

        #endregion End of Properties

        #region Members

        private Nullable<Point> coordinate;

        public static readonly DependencyProperty GamePieceProperty = DependencyProperty.Register("GamePiece",
            typeof(IBoardPlacer), typeof(GridPointControl), new PropertyMetadata(null ,new PropertyChangedCallback(
                OnGamePieceChanged)));

        #endregion End of Members
    }
}
