#define DEBUG
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using GoLibrary;

namespace GoUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            // TODO: determine type of game we're playing (9x9 / 13x13 / 19x19)

            // TODO: setup Board control with configuration

            this.GoBoard.BoardType = Controls.GridBoardControl.GridBoardType.e19x19;
            
            this.GoBoard.Visibility = Visibility.Visible;

#if DEBUG
            this.DebugStack.Visibility = Visibility.Visible;
            this.DebugBtn.Click += this.OnDebugClicked;
#endif
        }

        #endregion End of Constructors

        #region Events

        private void OnDebugClicked(Object sender, EventArgs e)
        {
            Int32 size = 0;

            if (!Int32.TryParse(this.DebugTB.Text, out size))
            {
                size = 9;
            }

            DebugWindow debugWindow = new DebugWindow(size);

            debugWindow.ShowDialog();
        }

        #endregion End of Events

        #region Methods

        #endregion End of Methods

        #region Properties

        #endregion End of Properties

        #region Members

        #endregion End of Members
    }
}
