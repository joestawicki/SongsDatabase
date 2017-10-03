using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Data;
using PianoSongs;

namespace WPFAutoCompleteTextbox
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>    
    public partial class AutoCompleteTextBox : Canvas
    {
        #region Variables
        private VisualCollection controls;
        private TextBox textBox;
        private ComboBox comboBox;
        private ObservableCollection<AutoCompleteEntry> autoCompletionList;
        private delegate void TextChangedCallback();
        private bool insertText;
        private int searchThreshold;
        private string enteredString;
        #endregion

        #region Constructor
        public AutoCompleteTextBox()
        {
            controls = new VisualCollection(this);
            InitializeComponent();

            autoCompletionList = new ObservableCollection<AutoCompleteEntry>();
            searchThreshold = 1;        // default threshold to 1 char

            // set up the text box and the combo box
            comboBox = new ComboBox();
            comboBox.IsSynchronizedWithCurrentItem = true;
            comboBox.IsTabStop = false;
            //comboBox.SelectionChanged += new SelectionChangedEventHandler(comboBox_SelectionChanged);
            comboBox.PreviewKeyDown += new KeyEventHandler(comboBox_PreviewKeyDown);
            comboBox.FocusVisualStyle = null;

            textBox = new TextBox();
            textBox.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.PreviewKeyDown += new KeyEventHandler(textBox_PreviewKeyDown);
            textBox.FocusVisualStyle = null;

            controls.Add(comboBox);
            controls.Add(textBox);
        }
        #endregion

        #region Properties
        public string Text
        {
            get { return textBox.Text; }
            set
            {
                insertText = true;
                textBox.Text = value;
            }
        }

        public int Threshold
        {
            get { return searchThreshold; }
            set { searchThreshold = value; }
        }
        #endregion

        #region Methods
        public void SetSource(DataTable table, String column)
        {
            try
            {
                autoCompletionList = null;
                autoCompletionList = new ObservableCollection<AutoCompleteEntry>();
                string item;
                foreach (DataRow row in table.Rows)
                {
                    if (row[column] != null && row[column].ToString() != String.Empty)
                    {
                        item = row[column].ToString();
                        autoCompletionList.Add(new AutoCompleteEntry(item, item));
                    }
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

        private void TextChanged()
        {
            try
            {
                comboBox.Items.Clear();
                if (textBox.Text.Length >= searchThreshold)
                {
                    foreach (AutoCompleteEntry entry in autoCompletionList)
                    {
                        foreach (string word in entry.KeywordStrings)
                        {
                            if (word.StartsWith(textBox.Text, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ComboBoxItem cbItem = new ComboBoxItem();
                                cbItem.Content = entry.ToString();
                                comboBox.Items.Add(cbItem);
                                break;
                            }
                        }
                    }
                    comboBox.IsDropDownOpen = comboBox.HasItems;
                }
                else
                {
                    comboBox.IsDropDownOpen = false;
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
        #endregion

        #region Overridden Methods
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            textBox.Arrange(new Rect(arrangeSize));
            comboBox.Arrange(new Rect(arrangeSize));
            return base.ArrangeOverride(arrangeSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return controls[index];
        }

        protected override int VisualChildrenCount
        {
            get { return controls.Count; }
        }
        #endregion

        #region Event Handlers
        private void comboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                try
                {
                    if (comboBox.Items.Count > 0)
                    {
                        if (!comboBox.IsDropDownOpen)
                            comboBox.IsDropDownOpen = true;
                        if (comboBox.SelectedIndex == comboBox.Items.Count - 1)
                            ;//Do Nothing
                        else
                        {
                            comboBox.SelectedIndex++;
                            for (int i = 0; i < comboBox.Items.Count; i++)
                            {
                                ((ComboBoxItem)comboBox.Items[i]).Foreground = Brushes.Black;
                                ((ComboBoxItem)comboBox.Items[i]).FontWeight = FontWeights.Normal;
                                ((ComboBoxItem)comboBox.Items[i]).FocusVisualStyle = null;
                            }
                            ((ComboBoxItem)comboBox.Items[comboBox.SelectedIndex]).FontWeight = FontWeights.ExtraBold;
                            ((ComboBoxItem)comboBox.Items[comboBox.SelectedIndex]).Foreground = Brushes.DarkBlue;
                        }
                        insertText = true;
                        ComboBoxItem cbItem = (ComboBoxItem)comboBox.Items[comboBox.SelectedIndex];
                        textBox.Text = cbItem.Content.ToString();
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
            else if (e.Key == Key.Up)
            {
                 try
                {
                    if (comboBox.Items.Count > 0)
                    {
                        if (!comboBox.IsDropDownOpen)
                            comboBox.IsDropDownOpen = true;
                        if (comboBox.SelectedIndex < 0)
                            ;//do nothing
                        else if (comboBox.SelectedIndex == 0)
                        {
                            comboBox.SelectedIndex = -1;
                            for (int i = 0; i < comboBox.Items.Count; i++)
                            {
                                ((ComboBoxItem)comboBox.Items[i]).Foreground = Brushes.Black;
                                ((ComboBoxItem)comboBox.Items[i]).FontWeight = FontWeights.Normal;
                                ((ComboBoxItem)comboBox.Items[i]).FocusVisualStyle = null;
                            }
                            textBox.Text = enteredString;
                            textBox.Focus();
                            textBox.CaretIndex = enteredString.Length;
                        }
                        else
                        {
                            comboBox.SelectedIndex--;
                            for (int i = 0; i < comboBox.Items.Count; i++)
                            {
                                ((ComboBoxItem)comboBox.Items[i]).Foreground = Brushes.Black;
                                ((ComboBoxItem)comboBox.Items[i]).FontWeight = FontWeights.Normal;
                                ((ComboBoxItem)comboBox.Items[i]).FocusVisualStyle = null;
                            }
                            ((ComboBoxItem)comboBox.Items[comboBox.SelectedIndex]).FontWeight = FontWeights.ExtraBold;
                            ((ComboBoxItem)comboBox.Items[comboBox.SelectedIndex]).Foreground = Brushes.DarkBlue;
                            insertText = true;
                            ComboBoxItem cbItem = (ComboBoxItem)comboBox.Items[comboBox.SelectedIndex];
                            textBox.Text = cbItem.Content.ToString();
                        }
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
            else if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                for (int i = 0; i < comboBox.Items.Count; i++)
                {
                    if (((ComboBoxItem)comboBox.Items[i]).FontWeight == FontWeights.ExtraBold)
                    {
                        ComboBoxItem cbItem = (ComboBoxItem)comboBox.Items[i];
                        textBox.Text = cbItem.Content.ToString();
                        textBox.Focus();
                        textBox.CaretIndex = textBox.Text.Length;
                        break;
                    }
                }         
            }
        }

        void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Down)
                return;
            try
            {
                if (comboBox.Items.Count > 0)
                {
                    if (!comboBox.IsDropDownOpen)
                        comboBox.IsDropDownOpen = true;
                    if (comboBox.SelectedIndex < 0)
                    {
                        enteredString = textBox.Text;
                        comboBox.SelectedIndex = 0;
                        for (int i = 0; i < comboBox.Items.Count; i++)
                        {
                            ((ComboBoxItem)comboBox.Items[i]).Foreground = Brushes.Black;
                            ((ComboBoxItem)comboBox.Items[i]).FontWeight = FontWeights.Normal;
                            ((ComboBoxItem)comboBox.Items[i]).FocusVisualStyle = null;
                        }
                        ((ComboBoxItem)comboBox.Items[0]).FontWeight = FontWeights.ExtraBold;
                        ((ComboBoxItem)comboBox.Items[0]).Foreground = Brushes.DarkBlue;
                        insertText = true;
                        ComboBoxItem cbItem = (ComboBoxItem)comboBox.Items[comboBox.SelectedIndex];
                        textBox.Text = cbItem.Content.ToString();
                    }
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

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // text was not typed, do nothing and consume the flag
            if (insertText == true)
                insertText = false;
            else
                TextChanged();
        }
        #endregion
    }

}