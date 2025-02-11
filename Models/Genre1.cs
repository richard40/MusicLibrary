using System;
using System.Collections.Generic;

namespace MusicLibrary.Models;

public partial class Genre1
{
    public int GenreId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Song1> Song1s { get; set; } = new List<Song1>();
}
