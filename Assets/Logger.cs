using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using PianoSongs.Assets;

namespace PianoSongs
{
    public static class Logger
    {
        public enum logLevel
        {
            Error,
            Warning,
            Information,
        };

        public static void LogMessage(string message, string fileName = Constants.constLoggerFileName, logLevel level = logLevel.Error, bool showDialog = false)
        {
            if (message.Length <= 0)
                return;
            if (fileName.Length <= 0 || !fileName.Contains("."))
                fileName = Constants.constLoggerFileName;
            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Logging a(n)" + level.ToString() + " on " + DateTime.Now.ToString());
                    sw.WriteLine("Message - " + message);
                    sw.WriteLine();
                    sw.WriteLine();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("Logging " + level.ToString() + " on " + DateTime.Now.ToString());
                    sw.WriteLine("Message - " + message);
                    sw.WriteLine();
                    sw.WriteLine();
                }
            }
            if (showDialog)
            {
                MessageBoxImage img;
                if (level == logLevel.Error)
                    img = MessageBoxImage.Error;
                else if (level == logLevel.Information)
                    img = MessageBoxImage.Information;
                else
                    img = MessageBoxImage.Warning;
                MessageBox.Show(message, "Message:", MessageBoxButton.OK, img);
            }
        }

        public static void LogError(Exception ex, string fileName = Constants.constLoggerFileName, bool showDialog = false)
        {
            if (ex.Message.Length <= 0)
                return;
            if (fileName.Length <= 0 || !fileName.Contains("."))
                fileName = Constants.constLoggerFileName;
            if (!File.Exists(fileName))
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Exception Date - " + DateTime.Now.ToString());
                    sw.WriteLine("Exception - " + ex.Message);
                    sw.WriteLine("Stack - " + ex.StackTrace.ToString());
                    sw.WriteLine();
                    sw.WriteLine();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("Exception Date - " + DateTime.Now.ToString());
                    sw.WriteLine("Exception - " + ex.Message);
                    sw.WriteLine("Stack - " + ex.StackTrace.ToString());
                    sw.WriteLine();
                    sw.WriteLine();
                }
            }
            if (showDialog)
            {
                MessageBox.Show(ex.Message.ToString(), "Exception:", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
