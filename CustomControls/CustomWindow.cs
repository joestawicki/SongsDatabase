using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace PianoSongs
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CustomWindow : ResourceDictionary
    {
        private int windowWidth;
        private int windowHeight;

        private void CMCloseClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Window window = Window.GetWindow(menuItem);
            if (window != null)
                window.Close();  
        }

        private void CMMaximizeClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Window window = Window.GetWindow(menuItem);
            if (window != null)
            {
                if (windowHeight <= 0)
                    windowHeight = 500;
                if (windowWidth <= 0)
                    windowWidth = 700;
                if (window.WindowState != WindowState.Maximized)
                {
                    windowWidth = Convert.ToInt32(window.Width);
                    windowHeight = Convert.ToInt32(window.Height);
                    window.WindowState = WindowState.Maximized;
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                    window.Width = windowWidth;
                    window.Height = windowHeight;
                }
            }
        }

        private void CMMinimizeClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Window window = Window.GetWindow(menuItem);
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            Window window = Window.GetWindow(grid);
            if (window != null)
                window.DragMove();
        }

        private void textBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            Window window = Window.GetWindow(textBlock);
            if (window != null)
                window.Close();      
        }

        private void RestoreButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Canvas mycanvas = (Canvas)sender;
            Window window = Window.GetWindow(mycanvas);
            if (window != null)
            {
                if (windowHeight <= 0)
                    windowHeight = 500;
                if (windowWidth <= 0)
                    windowWidth = 700;
                window.WindowState = WindowState.Normal;
                window.Width = windowWidth;
                window.Height = windowHeight;
            }
        }

        private void MaximizeButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;
            Window window = Window.GetWindow(rect);
            if (window != null)
            {
                if (windowHeight <= 0)
                    windowHeight = 500;
                if (windowWidth <= 0)
                    windowWidth = 700;
                if (window.WindowState != WindowState.Maximized)
                {
                    windowWidth = Convert.ToInt32(window.Width);
                    windowHeight = Convert.ToInt32(window.Height);
                    window.WindowState = WindowState.Maximized;
                }
                else
                {
                    window.WindowState = WindowState.Normal;
                    window.Width = windowWidth;
                    window.Height = windowHeight;
                }
            }
        }

        private void MinimizeButton_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Path path = (Path)sender;
            Window window = Window.GetWindow(path);
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void Rect_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) //double click
            {
                Rectangle rect = (Rectangle)sender;
                Window window = Window.GetWindow(rect);
                if (window != null)
                {
                    if (windowHeight <= 0)
                        windowHeight = 500;
                    if (windowWidth <= 0)
                        windowWidth = 700;
                    if (window.WindowState != WindowState.Maximized)
                    {
                        windowWidth = Convert.ToInt32(window.Width);
                        windowHeight = Convert.ToInt32(window.Height);
                        window.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        window.WindowState = WindowState.Normal;
                        window.Width = windowWidth;
                        window.Height = windowHeight;
                    }
                }
            }
        }

        private void Title_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) //double click
            {
                TextBlock textBlock = (TextBlock)sender;
                Window window = Window.GetWindow(textBlock);
                if (window != null)
                {
                    if (windowHeight <= 0)
                        windowHeight = 500;
                    if (windowWidth <= 0)
                        windowWidth = 700;
                    if (window.WindowState != WindowState.Maximized)
                    {
                        windowWidth = Convert.ToInt32(window.Width);
                        windowHeight = Convert.ToInt32(window.Height);
                        window.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        window.WindowState = WindowState.Normal;
                        window.Width = windowWidth;
                        window.Height = windowHeight;
                    }
                }
            }
        }

        private void WindowSizeChanged(object sender, System.EventArgs e)
        {
            Grid grid = (Grid)sender;
            Window window = Window.GetWindow(grid);
            if (window.WindowState == WindowState.Maximized)
            {
                Rectangle rectangle = (Rectangle)window.Template.FindName("MaximizeButton", window);
                rectangle.Visibility = Visibility.Collapsed;
                Canvas canvas = (Canvas)window.Template.FindName("RestoreButton", window);
                canvas.Visibility = Visibility.Visible;
                MenuItem menuItem = (MenuItem)window.Template.FindName("MenuItemMaximize", window);
                menuItem.Header = "Restore";
                menuItem.ToolTip = "Restore the Window";
            }
            else
            {
                Canvas canvas = (Canvas)window.Template.FindName("RestoreButton", window);
                canvas.Visibility = Visibility.Collapsed;
                Rectangle rectangle = (Rectangle)window.Template.FindName("MaximizeButton", window);
                rectangle.Visibility = Visibility.Visible;
                MenuItem menuItem = (MenuItem)window.Template.FindName("MenuItemMaximize", window);
                menuItem.Header = "Maximize";
                menuItem.ToolTip = "Maximize the Window";
            }

        }

        private void Top_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            DependencyObject depObject = (DependencyObject)sender;
            Window window = Window.GetWindow(depObject);
            if (window.Height > window.MinHeight && window.Height - e.VerticalChange > 0)
            {
                window.Height -= e.VerticalChange;
                window.Top += e.VerticalChange;
            }
            else
            {
                window.Height = window.MinHeight + 4;
                e.Handled = true;
            }
        }

        private void Btm_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            DependencyObject depObject = (DependencyObject)sender;
            Window window = Window.GetWindow(depObject);
            if (window.Height > window.MinHeight && window.Height + e.VerticalChange > 0)
            {
                window.Height += e.VerticalChange;
            }
            else
            {
                window.Height = window.MinHeight + 4;
                e.Handled = true;
            }
        }

        private void Rgt_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            DependencyObject depObject = (DependencyObject)sender;
            Window window = Window.GetWindow(depObject);
            if (window.Width > window.MinWidth && window.Width + e.HorizontalChange > 0)
            {
                window.Width += e.HorizontalChange;
            }
            else
            {
                window.Width = window.MinWidth + 4;
                e.Handled = true;
            }
        }

        private void Lft_DragDelta(Object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            DependencyObject depObject = (DependencyObject)sender;
            Window window = Window.GetWindow(depObject);
            if (window.Width > window.MinWidth && window.Width - e.HorizontalChange > 0)
            {
                window.Width -= e.HorizontalChange;
                window.Left += e.HorizontalChange;
            }
            else
            {
                window.Width = window.MinWidth + 4;
                e.Handled = true;
            }
        }
    }
}