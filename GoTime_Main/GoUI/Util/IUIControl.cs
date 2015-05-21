using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoUI.Util
{
    /// <summary>
    /// Interface defining some commonly used methods for UI Controls
    /// </summary>
    public interface IUIControl
    {
        /// <summary>
        /// Clears this control, returning it to a neutral state
        /// </summary>
        void Clear();

        /// <summary>
        /// Detaches this control, clearing and removing any references/bindings it has created
        /// </summary>
        void Detach();
    }
}
