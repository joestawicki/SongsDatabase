//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PianoSongs.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Artist
    {
        public Artist()
        {
            this.PianoSongs = new HashSet<PianoSong>();
        }
    
        public int ArtistID { get; set; }
        public string ArtistName { get; set; }
    
        public virtual ICollection<PianoSong> PianoSongs { get; set; }
    }
}
