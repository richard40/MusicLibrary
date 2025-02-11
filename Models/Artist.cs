using System;
using System.Collections.Generic;

namespace MusicLibrary.Models;

public partial class Artist
{
    public int artist_id { get; set; }

    public string stagename { get; set; } = null!;

    public string? label { get; set; }

    public string? origin { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
