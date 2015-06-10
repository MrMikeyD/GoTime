using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading.Tasks;

namespace GoUI.Util
{
    /// <summary>
    /// Base class for all pieces placed on a board
    /// </summary>
    public abstract class GamePiece : UIElement
    {
        #region Methods

        #endregion End of Methods

        #region Properties

        /// <summary>
        /// Gets the X-Coordinate of the game piece
        /// </summary>
        public int X
        {
            get { return x; }
            private set { this.x = value; }
        }

        /// <summary>
        /// Gets the Y-Coordinate of the game piece
        /// </summary>
        public int  Y
        {
            get { return y; }
            private set { this.y = value; }
        }

        #endregion End of Properties

        #region Members

        private int x;
        private int y;

        #endregion End of Members
    }
}
