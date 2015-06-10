using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoUI.Util
{
    /// <summary>
    /// Interface definining what qualifies an object to present an IBoardPlacer
    /// </summary>
    interface IBoardPresenter
    {
        #region Methods

        #endregion End of Methods

        #region Properties

        GamePiece GamePiece { get; set;}
        Int32 X { get; set; }
        Int32 Y { get; set;}

        #endregion End of Properties
    }
}
