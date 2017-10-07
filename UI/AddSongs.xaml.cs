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
using System.Data;
using WPFAutoCompleteTextbox;

namespace PianoSongs
{
    /// <summary>
    /// Interaction logic for AddSongs.xaml
    /// </summary>
    public partial class AddSongs : Window
    {
        #region Variables
        private DatabaseCache cache = new DatabaseCache();
        #endregion

        #region Constructor
        public AddSongs()
        {
            InitializeComponent();
            WaitDialog.Show("Loading, please wait...");
            cache.ClearAllCache();
            tbArtist.SetSource(cache.getArtists(), "artist");
            tbBook.SetSource(cache.getBooks(), "book");
            WaitDialog.Hide();
        }
        #endregion

        #region Methods
        private bool IsNumeric(string str)
        {
            try
            {
                if (str.Trim() == string.Empty)
                    return false;
                int result = int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Event Handlers
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnSaveEntries_Click(object sender, RoutedEventArgs e)
        {
            MyPianoSong ps;
            int count = 0;
            string artist;
            string book;
            string song;
            if (tbSongs.Text.Trim() == String.Empty)
            {
                MessageBox.Show("No songs entered.");
                return;
            }
            if (tbArtist.Text.Trim() == string.Empty)
            {
                MessageBox.Show("You must enter an artist...");
                artist = "";
                return;
            }
            else
                artist = tbArtist.Text;
            if (tbArtist.Text.Trim() == string.Empty)
            {
                MessageBox.Show("No book entered, using NONE for book name");
                book = "NONE";
            }
            else
                book = tbBook.Text;

            WaitDialog.Show("Saving songs, please wait...");
            for (int i = 0; i < tbSongs.LineCount; i++)
            {
                try
                {
                    ps = new MyPianoSong();
                    ps.SongArtist = artist;
                    ps.SongBook = book;
                    song = tbSongs.GetLineText(i);
                    if (song != string.Empty)
                    {
                        string pageNum = "";
                        if (song.Contains("~"))
                        {
                            int index = song.IndexOf("~");
                            if (song.Substring(index + 1).Trim() != string.Empty)
                            {
                                pageNum = song.Substring(index + 1).Trim();
                            }
                            ps.SongTitle = song.Substring(0, index).Trim();
                            if (IsNumeric(pageNum))
                            {
                                ps.PageNum = int.Parse(pageNum);
                            }
                        }    
                        else
                        {
                            ps.SongTitle = song.Trim();
                            ps.PageNum = -1;
                        }                        
                        PianoSongDatabase.addSongEntry(ps);
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                }  
            }
            WaitDialog.Hide();
            MessageBox.Show("Successfully saved " + count + " songs!");
            cache.ClearAllCache();
            tbBook.Text = "";
            tbArtist.Text = "";
            tbSongs.Text = "";
            tbArtist.SetSource(cache.getArtists(), "artist");
            tbBook.SetSource(cache.getBooks(), "book");
            return;
        }
        #endregion        
    }
}
