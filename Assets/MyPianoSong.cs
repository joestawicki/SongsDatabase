using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PianoSongs
{
    public class MyPianoSong
    {
        #region Variables
        private string Title;
        private string Artist;
        private string Book;
        private int PageNumber;
        private int pianoSongID;
        #endregion        

        #region  Properties
        public int PageNum
        {
            get { return PageNumber; }
            set { PageNumber = value; }
        }

        public string SongArtist
        {
            get { return Artist; }
            set { Artist = value; }
        }

        public string SongBook
        {
            get { return Book; }
            set { Book = value; }
        }

        public string SongTitle
        {
            get { return Title; }
            set { Title = value; }
        }

        public int songID
        {
            get { return pianoSongID; }
            set { pianoSongID = value; }
        }
        #endregion

        #region Methods
        public bool songValid()
        {
            if (SongTitle == "")
                return false;
            if (SongBook == "")
                return false;
            if (SongArtist == "")
                return false;
            return true;
        }
        public string songValidMessage()
        {
            if (SongTitle == "")
                return "Song Title cannot be blank.";
            if (SongBook == "")
                return "Song book cannot be blank.";
            if (SongArtist == "")
                return "Song Artist cannot be blank.";
            return "";
        }
        #endregion
    }
}
