using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Models;
using System.Linq;

namespace MusicLibrary.Controllers
{
    public class SongController : Controller
    {
        private readonly MusicLibraryDbContext _context;
        private const int PageSize = 10;  // Set the page size to 10 songs per page

        public SongController(MusicLibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? ArtistId, int? GenreId, int? SongId, int page = 1)
        {
            var filteredSongs = _context.Songs.Include(s => s.Artist).Include(s => s.Genre).AsQueryable();

            if (ArtistId.HasValue && ArtistId != 0)
            {
                filteredSongs = filteredSongs.Where(s => s.artist_id == ArtistId);
            }
            if (GenreId.HasValue && GenreId != 0)
            {
                filteredSongs = filteredSongs.Where(s => s.genre_id == GenreId);
            }
            if (SongId.HasValue && SongId != 0)
            {
                filteredSongs = filteredSongs.Where(s => s.song_id == SongId);
            }

            // Get the total number of songs after filtering
            var totalSongs = filteredSongs.Count();

            // Apply pagination
            var songs = filteredSongs
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Prepare ViewBag for filters and pagination
            ViewBag.Artists = _context.Artists
                .Select(a => new SelectListItem { Value = a.artist_id.ToString(), Text = a.stagename })
                .ToList();

            ViewBag.Genres = _context.Genres
                .Select(g => new SelectListItem { Value = g.genre_id.ToString(), Text = g.title })
                .ToList();

            ViewBag.Songs = _context.Songs
                .Select(s => new SelectListItem { Value = s.song_id.ToString(), Text = s.title })
                .ToList();

            // Set up pagination
            var totalPages = (int)Math.Ceiling(totalSongs / (double)PageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(songs);
        }

        [HttpPost]
        public IActionResult Edit(int song_id, string title, int artist_id, int genre_id)
        {
            if (ModelState.IsValid)
            {
                var existingSong = _context.Songs
                    .Include(s => s.Genre)
                    .FirstOrDefault(s => s.song_id == song_id);

                if (existingSong != null)
                {
                    existingSong.title = title;
                    existingSong.artist_id = artist_id; // Update artist ID directly
                    existingSong.genre_id = genre_id; // Update genre ID


                    _context.SaveChanges();

                    return Json(new { success = true, id = existingSong.song_id });
                }

                return Json(new { success = false, message = "Song not found." });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Invalid data provided.", errors = errors });
        }

        [HttpPost]
        public IActionResult Delete(int song_id)
        {
            var song = _context.Songs.FirstOrDefault(s => s.song_id == song_id);

            if (song == null)
            {
                return Json(new { success = false, message = "Song not found." });
            }

            _context.Songs.Remove(song);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Create( int artist_id, int genre_id, string title)
        {
            if (string.IsNullOrWhiteSpace(title) || artist_id == 0 || genre_id == 0)
            {
                return Json(new { success = false, message = "Invalid data provided." });
            }

            var newSong = new Song
            {
                artist_id = artist_id,
                genre_id = genre_id,
                title = title

            };

            _context.Songs.Add(newSong);
            _context.SaveChanges();

            return Json(new { success = true, id = newSong.song_id });
        }


    }
}
