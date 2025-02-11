using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicLibrary.Models;

public partial class Song
{
    public int song_id { get; set; }

    [Required] // Check if you have this attribute
    public string title { get; set; } = null!;

    public int artist_id { get; set; }

    public int genre_id { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;
}

