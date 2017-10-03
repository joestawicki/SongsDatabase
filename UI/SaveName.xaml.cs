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

namespace PianoSongs.UI
{
    public partial class SaveName : Window
    {
        public string title = "";

        public SaveName()
        {
            InitializeComponent();
            textBoxTitle.Focus();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxTitle.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a title with at least one character");
                textBoxTitle.Text = "";
                return;
            }
            title = textBoxTitle.Text.Trim();
            DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
