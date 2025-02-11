using System;
using System.Collections.Generic;

namespace MusicLibrary.Models;

public partial class Genre
{
    public int genre_id { get; set; }

    public string title { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
