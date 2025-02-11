using System;
using System.Collections.Generic;

namespace MusicLibrary.Models;

public partial class Song1
{
    public int SongId { get; set; }

    public string Title { get; set; } = null!;

    public int ArtistId { get; set; }

    public int GenreId { get; set; }

    public virtual Artist1 Artist { get; set; } = null!;

    public virtual Genre1 Genre { get; set; } = null!;
}
