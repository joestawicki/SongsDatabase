using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace PianoSongs
{
    /// <summary>
    /// Interaction logic for WaitDlg.xaml
    /// </summary>
    public partial class WaitDlg : Window
    {
        #region Variables
        
        #endregion

        #region Constructor
        public WaitDlg()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        #endregion
    }
}
