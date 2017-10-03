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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using PianoSongs.Assets;

namespace PianoSongs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables

        #endregion

        #region Constructor
        public MainWindow()
        {
            try
            {
                InitializeComponent();
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

        #region Methods

        #endregion

        #region Event Handlers
        private void btnEnterData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddSongs newEntry = new AddSongs();
                this.Hide();
                newEntry.ShowDialog();
                this.Show();
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

        private void WindowMainScreen_ContentRendered(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void btnReviewData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewSongs viewSongs = new ViewSongs();
                this.Hide();
                viewSongs.ShowDialog();
                this.Show();
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

        private void btnLookupData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LookupSongs lookupSongs = new LookupSongs();
                this.Hide();
                lookupSongs.ShowDialog();
                if (!lookupSongs.IsVisible)
                {
                    this.Show();
                }
                    
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

        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
                
                string backupPath = "";
                string sourcePath;
                string sourceFile;
                string destFile;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    backupPath = dlg.SelectedPath;
                else
                    return;
#if (DEBUG)
                sourcePath = System.Windows.Forms.Application.StartupPath;
#else
                sourcePath = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.DataDirectory;
#endif
                sourceFile = System.IO.Path.Combine(sourcePath, Constants.constDatabaseFileName);
                destFile = System.IO.Path.Combine(backupPath, Constants.constDatabaseFileName);

                System.IO.File.Copy(sourceFile, destFile, true);

                MessageBox.Show("Backup was successful!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with backup, please look at log");
#if (DEBUG)
                Logger.LogError(ex, showDialog: true);
#else
                Logger.LogError(ex);
#endif
            }
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
                string destPath;
                string sourceFile;
                string destFile;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    sourceFile = dlg.FileName;
                else
                    return;
#if (DEBUG)
                destPath = System.Windows.Forms.Application.StartupPath;
#else
                destPath = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.DataDirectory;
#endif
                destFile = System.IO.Path.Combine(destPath, Constants.constDatabaseFileName);

                System.IO.File.Copy(sourceFile, destFile, true);

                MessageBox.Show("Restore was successful!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with restore, please look at log");
#if (DEBUG)
                Logger.LogError(ex, showDialog: true);
#else
                Logger.LogError(ex);
#endif
            }
        }

        private void btnViewLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string directory = System.Windows.Forms.Application.StartupPath;
                var file = System.IO.Path.Combine(directory, Constants.constLoggerFileName);
                Process.Start(file);
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
