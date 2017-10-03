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

namespace PianoSongs
{
    /// <summary>
    /// Interaction logic for LookupSongs.xaml
    /// </summary>
    public partial class LookupSongs : Window
    {
        public LookupSongs()
        {
            InitializeComponent();
        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxArtistNameSearch.SelectedIndex = -1;
            ComboBoxBookNameSearch.SelectedIndex = -1;
            ComboBoxSongNameSearch.SelectedIndex = -1;
            TextBoxArtistNameSearch.Text = "";
            TextBoxBookNameSearch.Text = "";
            TextBoxSongNameSearch.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ViewSongs viewSongs = new ViewSongs(Convert.ToInt32(ComboBoxSongNameSearch.SelectedValue), Convert.ToInt32(ComboBoxArtistNameSearch.SelectedValue), Convert.ToInt32(ComboBoxBookNameSearch.SelectedValue),
                                                TextBoxSongNameSearch.Text.Trim(), TextBoxArtistNameSearch.Text.Trim(), TextBoxBookNameSearch.Text.Trim());
            //this.Hide();
            viewSongs.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
