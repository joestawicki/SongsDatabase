using System;
using System.ComponentModel;
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
using PianoSongs.Data;
using PianoSongs.Assets;
using PianoSongs.UI;

namespace PianoSongs
{
    /// <summary>
    /// Interaction logic for ViewSongs.xaml
    /// </summary>
    public partial class ViewSongs : Window
    {
        private static AllSongsDatabaseEntities context = new AllSongsDatabaseEntities();
        private static System.Data.DataTable bindingTable = new System.Data.DataTable();
        private bool fromLookup = false;

        private int prvtSongFilter = -1;
        private int prvtArtistFilter = -1;
        private int prvtBookFilter = -1;
        private string prvtSongText = "";
        private string prvtArtistText = "";
        private string prvtBookText = "";

        public ViewSongs()
        {
            WaitDialog.Show("Loading, please wait...");
            InitializeComponent();
            WaitDialog.Hide();
        }

        public ViewSongs(int songFilter, int artistFilter, int bookFilter, string songText, string artistText, string bookText)
        {
            fromLookup = true;
            prvtSongFilter = songFilter;
            prvtArtistFilter = artistFilter;
            prvtBookFilter = bookFilter;
            prvtSongText = songText.Trim();
            prvtArtistText = artistText.Trim();
            prvtBookText = bookText.Trim();

            WaitDialog.Show("Searching, please wait...");
            InitializeComponent();
            searchSongs();
            WaitDialog.Hide();
        }

        public void searchSongs()
        {
            System.Windows.Data.CollectionViewSource pianoSongViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pianoSongViewSource")));
            bool displayAll = false;
            if (prvtSongFilter == -1 && prvtArtistFilter == -1 && prvtBookFilter == -1)
            {
                displayAll = true;
            }
            else if (prvtSongText.Trim() == "" && prvtArtistText.Trim() == "" && prvtBookText.Trim() == "")
            {
                displayAll = true;
            }
            if (displayAll)
            {
                bindingTable = PianoSongDatabase.getAllData();
            }
            else
            {
                bindingTable = PianoSongDatabase.searchData(prvtSongFilter, prvtArtistFilter, prvtBookFilter, prvtSongText.Trim(), prvtArtistText.Trim(), prvtBookText.Trim());
            }
            bindingTable.AcceptChanges();
            pianoSongViewSource.Source = bindingTable;
            lblCount.Content = "Count: " + bindingTable.Rows.Count.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!fromLookup)
            {
                searchSongs();
            }
        }

        private void ShowOptionsWindow()
        {
            MessageBox.Show("Still need to implement this...");
        }

        private void ShowPrintWindow()
        {
            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == false)
                return;

            string documentTitle = "Test Document";
            Size pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            SaveName printTitle = new SaveName();
            printTitle.ShowDialog();
            if (printTitle.DialogResult == true)
                documentTitle = printTitle.title;
            else return;

            CustomDataGridDocumentPaginator paginator = new CustomDataGridDocumentPaginator(pianoSongsDataGrid as DataGrid, documentTitle, pageSize, new Thickness(30, 20, 30, 20));
            Style titleStyle = new Style();
            titleStyle.Setters.Add(new Setter(TextBox.FontSizeProperty, 20.0));
            titleStyle.Setters.Add(new Setter(TextBox.FontWeightProperty, FontWeights.Bold));
            Style headerStyle = new Style();
            headerStyle.Setters.Add(new Setter(TextBox.FontWeightProperty, FontWeights.Bold));
            paginator.DocumentHeaderTextStyle = titleStyle;
            paginator.TableHeaderTextStyle = headerStyle;
            printDialog.PrintDocument(paginator, "Grid");
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            ShowOptionsWindow();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.P && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ShowPrintWindow();
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            ShowPrintWindow();
        }

        private void btnPrintPreview_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Still Need to Implement this...");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete all of the songs shown?", "Delete Songs?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int count = 0;
                    if (bindingTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No Songs to Delete.");
                        return;
                    }
                    foreach (System.Data.DataRow pianoSong in bindingTable.Rows)
                    {
                        MyPianoSong song = new MyPianoSong();
                        song.songID = Convert.ToInt32(pianoSong["PianoSongID", System.Data.DataRowVersion.Current].ToString().Trim());
                        PianoSongDatabase.deleteSongEntry(song);
                        count++;
                    }
                    bindingTable.AcceptChanges();
                    searchSongs();
                    if (count == 0)
                        MessageBox.Show("No songs were deleted.");
                    else
                        MessageBox.Show(String.Format("Successfully deleted {0} songs!", count));

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

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = 0;
                foreach (System.Data.DataRow pianoSong in bindingTable.Rows)
                {
                    if (pianoSong.RowState == System.Data.DataRowState.Added)
                    {
                        if (pianoSong["Title"].ToString().Trim() == "" && pianoSong["Artist"].ToString().Trim() == "" &&
                            pianoSong["Book"].ToString().Trim() == "" && pianoSong["PageNumber"].ToString().Trim() == "")
                            continue;
                        MyPianoSong song = new MyPianoSong();
                        count++;
                        try
                        {
                            song.SongTitle = pianoSong["Title"].ToString().Trim();
                            song.SongArtist = pianoSong["Artist"].ToString().Trim();
                            song.SongBook = pianoSong["Book"].ToString().Trim();
                            if (pianoSong["PageNumber"].ToString().Trim() == "")
                                song.PageNum = -1;
                            else
                                song.PageNum = Convert.ToInt32(pianoSong["PageNumber"].ToString().Trim());
                        }
                        catch (Exception exc)
                        {
                            Logger.LogMessage("Song title, artist, or book is not valid, unable to save.", level: Logger.logLevel.Error, showDialog: true);
                            return;
                        }
                        string valid = song.songValidMessage();
                        if (valid.Trim() != "")
                            throw new Exception(valid);
                        PianoSongDatabase.addSongEntry(song);
                    }
                    else if (pianoSong.RowState == System.Data.DataRowState.Deleted)
                    {
                        count++;
                        MyPianoSong song = new MyPianoSong();
                        song.songID = Convert.ToInt32(pianoSong["PianoSongID", System.Data.DataRowVersion.Original].ToString().Trim());
                        PianoSongDatabase.deleteSongEntry(song);
                    }
                    else if (pianoSong.RowState == System.Data.DataRowState.Modified)
                    {
                        count++;
                        MyPianoSong song = new MyPianoSong();
                        try
                        {
                            song.songID = Convert.ToInt32(pianoSong["PianoSongID"].ToString().Trim());
                            song.SongTitle = pianoSong["Title"].ToString().Trim();
                            song.SongArtist = pianoSong["Artist"].ToString().Trim();
                            song.SongBook = pianoSong["Book"].ToString().Trim();
                            if (pianoSong["PageNumber"].ToString().Trim() == "")
                                song.PageNum = -1;
                            else
                                song.PageNum = Convert.ToInt32(pianoSong["PageNumber"].ToString().Trim());
                        }
                        catch (Exception exc)
                        {
                            Logger.LogMessage("Song title, artist, or book is not valid, unable to save.", level: Logger.logLevel.Error, showDialog: true);
                            return;
                        }
                        string valid = song.songValidMessage();
                        if (valid.Trim() != "")
                            throw new Exception(valid);
                        PianoSongDatabase.updateSongEntry(song);
                    }
                }
                bindingTable.AcceptChanges();
                searchSongs();
                if (count == 0)
                    MessageBox.Show("No changes were made");
                else
                    MessageBox.Show(String.Format("Successfully saved {0} changes!", count));
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context.Dispose();
        }
    }
    
}
