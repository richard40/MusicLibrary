using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicLibrary.Models;
using System.Linq;

namespace MusicLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicLibraryDbContext _context;

        public HomeController(MusicLibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // ✅ Ensure data is retrieved correctly before Select()
            var artists = _context.Artists.ToList();
            var genres = _context.Genres.ToList();
            var songs = _context.Songs.ToList();

            ViewBag.ArtistId = artists.Select(a => new SelectListItem
            {
                Value = a.artist_id.ToString(),
                Text = a.stagename
            }).ToList();

            ViewBag.GenreId = genres.Select(g => new SelectListItem
            {
                Value = g.genre_id.ToString(),
                Text = g.title
            }).ToList();

            ViewBag.SongId = songs.Select(s => new SelectListItem
            {
                Value = s.song_id.ToString(),
                Text = s.title
            }).ToList();

            return View();
        }
    }
}
