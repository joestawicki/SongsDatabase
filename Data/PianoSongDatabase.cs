using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using PianoSongs;
using PianoSongs.Data;
using PianoSongs.Assets;

namespace PianoSongs
{
    public static class PianoSongDatabase
    {
        #region Variables
        private static AllSongsDatabaseEntities context = new AllSongsDatabaseEntities();
        #endregion
        
        #region GetMethods
        public static DataTable getAllArtists()
        {
            try
            {
                DataTable newDT = new DataTable();
                DataRow newRow;
                newDT.Columns.Add("Artist");
                var newQuery = (from a in context.Artists
                                               select a.ArtistName).Distinct();
                foreach (String s in (IQueryable<String>)newQuery)
                {
                    newRow = newDT.NewRow();
                    newRow["Artist"] = s;
                    newDT.Rows.Add(newRow);
                }

                return newDT;
            }
            catch (Exception ex)
            {
                #if (DEBUG)
                Logger.LogError(ex, showDialog: true);
                #else
                Logger.LogError(ex);
                #endif
                return null;
            }
            
        }

        public static DataTable getAllBooks()
        {
            try
            {
                DataTable newDT = new DataTable();
                DataRow newRow;
                newDT.Columns.Add("Book");
                var newQuery = (from b in context.Books
                                               select b.BookName).Distinct();
                foreach (String s in (IQueryable<String>)newQuery)
                {
                    newRow = newDT.NewRow();
                    newRow["Book"] = s;
                    newDT.Rows.Add(newRow);
                }

                return newDT;
            }
            catch (Exception ex)
            {
                #if (DEBUG)
                Logger.LogError(ex, showDialog: true);
                #else
                Logger.LogError(ex);
                #endif
                return null;
            }
           
        }

        public static DataTable searchData(int songFilter, int artistFilter, int bookFilter, string songText, string artistText, string bookText)
        {
            try
            {
                string song1 = string.Empty;
                string song2 = string.Empty;
                string artist1 = string.Empty;
                string artist2 = string.Empty;
                string book1 = string.Empty;
                string book2 = string.Empty;
                string[] songSplit = new string[2];
                string[] artistSplit = new string[2];
                string[] bookSplit = new string[2];
                if (songFilter == Constants.constBetweenKey)
                {
                    if (!songText.Contains('*'))
                    { MessageBox.Show("Song must contain *"); }
                    songSplit = songText.Split('*');
                    song1 = songSplit[0];
                    song2 = songSplit[1];
                }
                if (artistFilter == Constants.constBetweenKey)
                {
                    if (!artistText.Contains('*'))
                    { MessageBox.Show("Artist must contain *"); }
                    artistSplit = artistText.Split('*');
                    artist1 = artistSplit[0];
                    artist2 = artistSplit[1];
                }
                if (bookFilter == Constants.constBetweenKey)
                {
                    if (!bookText.Contains('*'))
                    { MessageBox.Show("Book must contain *"); }
                    bookSplit = bookText.Split('*');
                    book1 = bookSplit[0];
                    book2 = bookSplit[1];
                }
                DataTable newDT = new DataTable();
                newDT.Columns.Add("PianoSongID");
                newDT.Columns.Add("Title");
                newDT.Columns.Add("Artist");
                newDT.Columns.Add("Book");
                newDT.Columns.Add("PageNumber");
                DataRow newRow;
                var newQuery = from ps in context.PianoSongs
                               join a in context.Artists on ps.ArtistID equals a.ArtistID
                               join s in context.Songs on ps.SongID equals s.SongID
                               join b in context.Books on ps.BookID equals b.BookID
                               where ((a.ArtistName != null && (artistFilter <= 0 || artistText.Trim() == "")) ||
                                    (a.ArtistName == artistText && artistFilter == Constants.constEqualsKey) ||
                                    (a.ArtistName.Contains(artistText) && artistFilter == Constants.constContainsKey) ||
                                    (!a.ArtistName.Contains(artistText) && artistFilter == Constants.constDoesNotContainKey) ||
                                    (a.ArtistName != artistText && artistFilter == Constants.constDoesNotEqualKey) ||
                                    (a.ArtistName.CompareTo(artist1) >= 0 && a.ArtistName.CompareTo(artist2) <= 0 && artistFilter == Constants.constBetweenKey)) &&
                                    //BOOK NAME
                                    ((b.BookName != null && (bookFilter <= 0 || bookText.Trim() == "")) ||
                                    (b.BookName == bookText && bookFilter == Constants.constEqualsKey) ||
                                    (b.BookName.Contains(bookText) && bookFilter == Constants.constContainsKey) ||
                                    (!b.BookName.Contains(bookText) && bookFilter == Constants.constDoesNotContainKey) ||
                                    (b.BookName != bookText && bookFilter == Constants.constDoesNotEqualKey) ||
                                    (b.BookName.CompareTo(book1) >= 0 && b.BookName.CompareTo(book2) <= 0 && bookFilter == Constants.constBetweenKey)) &&
                                    //SONG NAME
                                    ((s.SongName != null && (songFilter <= 0 || songText.Trim() == "")) ||
                                    (s.SongName == songText && songFilter == Constants.constEqualsKey) ||
                                    (s.SongName.Contains(songText) && songFilter == Constants.constContainsKey) ||
                                    (!s.SongName.Contains(songText) && songFilter == Constants.constDoesNotContainKey) ||
                                    (s.SongName != songText && songFilter == Constants.constDoesNotEqualKey) ||
                                    (s.SongName.CompareTo(song1) >= 0 && s.SongName.CompareTo(song2) <= 0 && songFilter == Constants.constBetweenKey))
                               select new
                               {
                                   songName = s.SongName,
                                   artistName = a.ArtistName,
                                   bookName = b.BookName,
                                   pageNum = ps.PageNumber == -1 ? null : ps.PageNumber,
                                   pianoSongID = ps.PianoSongID
                               };

                foreach (var p in newQuery)
                {
                    newRow = newDT.NewRow();
                    newRow["PianoSongID"] = p.pianoSongID;
                    newRow["Title"] = p.songName;
                    newRow["Artist"] = p.artistName;
                    newRow["Book"] = p.bookName;
                    newRow["PageNumber"] = p.pageNum;
                    newDT.Rows.Add(newRow);
                }
                return newDT;
            }
            catch (Exception ex)
            {
                #if (DEBUG)
                Logger.LogError(ex, showDialog: true);
                #else
                Logger.LogError(ex);
                #endif
                return null;
            }
        }

        public static DataTable getAllData()
        {
            try
            {
                DataTable newDT = new DataTable();
                newDT.Columns.Add("PianoSongID");
                newDT.Columns.Add("Title");
                newDT.Columns.Add("Artist");
                newDT.Columns.Add("Book");
                newDT.Columns.Add("PageNumber");
                DataRow newRow;
                var newQuery = from ps in context.PianoSongs
                               join a in context.Artists on ps.ArtistID equals a.ArtistID
                               join s in context.Songs on ps.SongID equals s.SongID
                               join b in context.Books on ps.BookID equals b.BookID
                               where a.ArtistName != null && b.BookName != null && s.SongName != null
                               select new { songName = s.SongName, 
                                            artistName = a.ArtistName, 
                                            bookName = b.BookName, 
                                            pageNum = ps.PageNumber == -1? null : ps.PageNumber,
                                            pianoSongID = ps.PianoSongID };

                foreach (var p in newQuery)
                {
                    newRow = newDT.NewRow();
                    newRow["PianoSongID"] = p.pianoSongID;
                    newRow["Title"] = p.songName;
                    newRow["Artist"] = p.artistName;
                    newRow["Book"] = p.bookName;
                    newRow["PageNumber"] = p.pageNum;
                    newDT.Rows.Add(newRow);
                }
                return newDT;                
            }
            catch (Exception ex)
            {
                #if (DEBUG)
                Logger.LogError(ex, showDialog: true);
                #else
                Logger.LogError(ex);
                #endif
                return null;
            }
        }
        #endregion
        
        #region UpdateMethods
        public static int addSongEntry(MyPianoSong pianoSong)
        {
            try
            {
                Song s = new Song();
                Artist a = new Artist();
                Book b = new Book();
                PianoSong pSong = new PianoSong();
                s.SongName = pianoSong.SongTitle;
                a.ArtistName = pianoSong.SongArtist;
                b.BookName = pianoSong.SongBook;
                pSong.Song = s;
                pSong.Artist = a;
                pSong.Book = b;
                pSong.PageNumber = pianoSong.PageNum;
                context.PianoSongs.Add(pSong);
                context.SaveChanges();
                return pSong.PianoSongID;
            }
            catch (Exception ex)
            {
                #if (DEBUG)
                Logger.LogError(ex, showDialog: true);
                #else
                Logger.LogError(ex);
                #endif
                return -1;
            }
        }
        
        public static int updateSongEntry(MyPianoSong pianoSong)
        {
            if (pianoSong.songID <= 0)
            {
                throw new Exception("Unable to update song, no song ID provided");
            }
            try
            {
                var newQuery = (from p in context.PianoSongs
                                where p.PianoSongID == pianoSong.songID
                                select p);
                PianoSong ps = newQuery.FirstOrDefault();
                Song s = ps.Song;
                Artist a = ps.Artist;
                Book b = ps.Book;
                s.SongName = pianoSong.SongTitle;
                a.ArtistName = pianoSong.SongArtist;
                b.BookName = pianoSong.SongBook;
                ps.PageNumber = pianoSong.PageNum;
                context.SaveChanges();
                return ps.PianoSongID;
            }
            catch (Exception ex)
            {
#if (DEBUG)
                Logger.LogError(ex, showDialog: true);
#else
                Logger.LogError(ex);
#endif
                return -1;
            }
        }

        public static int deleteSongEntry(MyPianoSong pianoSong)
        {
            if (pianoSong.songID <= 0)
            {
                throw new Exception("Unable to delete song, no song ID provided");
            }
            try
            {
                var newQuery = (from p in context.PianoSongs
                                where p.PianoSongID == pianoSong.songID
                                select p);
                PianoSong ps = newQuery.FirstOrDefault();
                Song s = ps.Song;
                Artist a = ps.Artist;
                Book b = ps.Book;
                context.PianoSongs.Remove(ps);
                context.SaveChanges();
                return ps.PianoSongID;
            }
            catch (Exception ex)
            {
#if (DEBUG)
                Logger.LogError(ex, showDialog: true);
#else
                Logger.LogError(ex);
#endif
                return -1;
            }
        }
        #endregion      
    }
}
