using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GoUI.Util;

namespace GoUI.Controls
{
    public class GridBoardRow : UniformGrid, IUIControl
    {
        #region Constructors

        public GridBoardRow()
            :base()
        {

        }

        #endregion End of Constructors

        #region Events

        #endregion End of Events

        #region Methods
   
        /// <summary>
        /// Clears this control, returning it to a neutral state
        /// </summary>
        public void Clear()
        {
            base.Columns = 0;
            base.Rows = 0;

            if (this.Children != null && this.Children.Count > 0)
            {
                foreach (UIElement child in this.Children)
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

            if (this.Children != null && this.Children.Count > 0)
            {
                foreach (IUIControl ctrl in this.Children)
                {
                    ctrl.Detach();
                }
            }
        }

        /// <summary>
        /// Appends the given ctrl to the row control
        /// </summary>
        public void AddGridPoint(GridPointControl ctrl)
        {
            if (ctrl != null)
            {
                this.Children.Add(ctrl);
            }
        }

        #endregion End of Methods

        #region Properties

        #endregion End of Properties

        #region Members

        #endregion End of Members
    }
}
