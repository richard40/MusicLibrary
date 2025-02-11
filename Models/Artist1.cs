using System;
using System.Collections.Generic;

namespace MusicLibrary.Models;

public partial class Artist1
{
    public int artist_id { get; set; }

    public string atagename { get; set; } = null!;

    public string label { get; set; } = null!;

    public string origin { get; set; } = null!;

    public virtual ICollection<Song1> Song1s { get; set; } = new List<Song1>();
}
