using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using System.Windows;

namespace PianoSongs
{
    public class WaitDialog
    {
        #region Variables
        private static WaitDlg w;
        private static string waitText;
        #endregion

        #region Methods
        public static void Hide()
        {
            if (w != null)
            {
                w.Close();
            }
        }

        public static void Show(string message)
        {
            try
            {
                if (waitText == String.Empty)
                    waitText = "Loading...";
                else
                    waitText = message;

                if (w == null)
                    w = new WaitDlg();
                w.tbText.Text = waitText;
                w.Show();
            }
            catch (Exception ex)
            {
                #if (DEBUG)
                Logger.LogError(ex, showDialog: true);
                #else
                Logger.LogError(ex);
                #endif
            }
        }
        #endregion
    }
}
