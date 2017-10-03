using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PianoSongs.Data;

namespace PianoSongs
{
    public class DatabaseCache
    {
        #region Variables
        private DataTable m_Artists = null;
        private DataTable m_Books = null;
        private DataTable m_PianoSongs = null;
        private AllSongsDatabaseEntities context = new AllSongsDatabaseEntities();
        #endregion

        #region Methods
        public void ClearAllCache()
        {
            m_Artists = null;
            m_Books = null;
            m_PianoSongs = null;
        }

        public DataTable getArtists()
        {
            if (m_Artists == null)
                m_Artists = PianoSongDatabase.getAllArtists();
            return m_Artists;
        }

        public DataTable getBooks()
        {
            if (m_Books == null)
                m_Books = PianoSongDatabase.getAllBooks();
            return m_Books;
        }

        public DataTable getAllData()
        {
            if (m_PianoSongs == null)
                m_PianoSongs = PianoSongDatabase.getAllData();
            return m_PianoSongs;
        }
        #endregion
    }
}
