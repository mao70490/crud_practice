using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudPractice1.Data;
using CrudPractice1.Models;
using System.Text;

namespace CrudPractice1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly CrudPractice1Context _context;

        public MoviesController(CrudPractice1Context context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string searchString)
        {
            // 保留使用者搜尋字串供 View 使用（讓輸入欄顯示原輸入值）
            ViewData["CurrentFilter"] = searchString;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }
        //return View(await _context.Movie.ToListAsync());

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, IFormFile PosterFile)
        {
            if (ModelState.IsValid)
            {
                if (PosterFile != null && PosterFile.Length > 0)
                {
                    // 產生唯一檔名，避免名稱重複
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PosterFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    try
                    {
                        // 確保資料夾存在
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                        // 儲存檔案
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await PosterFile.CopyToAsync(stream);
                        }

                        // 儲存路徑進資料庫
                        movie.PosterUrl = "/uploads/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        // 加入錯誤訊息
                        ModelState.AddModelError("", "圖片上傳失敗：" + ex.Message);
                        return View(movie);
                    }
                }

                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                // 輸出 Model 驗證錯誤
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"欄位 {modelState.Key} 錯誤: {error.ErrorMessage}");
                    }
                }

                return View(movie);
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }


        //下載成csv檔
        public async Task<IActionResult> DownloadAsCsv(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var sb = new StringBuilder();

            // 標題列
            sb.AppendLine("Title,ReleaseDate,Genre,Price,OriginalTitle,Director,Cast,DurationMinutes,Language,Country,Rating,ImdbScore,BoxOffice,Synopsis,PosterUrl,TrailerUrl,ProductionCompany,Budget,Awards,IsStreaming");

            // 資料列（處理可能有逗號、換行的欄位內容，使用引號包住）
            sb.AppendLine(string.Join(",", new[]
            {
                CsvEscape(movie.Title),
                movie.ReleaseDate.ToString("yyyy-MM-dd"),
                CsvEscape(movie.Genre),
                movie.Price.ToString(),
                CsvEscape(movie.OriginalTitle),
                CsvEscape(movie.Director),
                CsvEscape(movie.Cast),
                movie.DurationMinutes.ToString(),
                CsvEscape(movie.Language),
                CsvEscape(movie.Country),
                CsvEscape(movie.Rating),
                movie.ImdbScore.ToString(),
                movie.BoxOffice.ToString(),
                CsvEscape(movie.Synopsis),
                CsvEscape(movie.PosterUrl),
                CsvEscape(movie.TrailerUrl),
                CsvEscape(movie.ProductionCompany),
                movie.Budget.ToString(),
                CsvEscape(movie.Awards),
                //movie.IsStreaming.ToString()
            }));

            var fileName = $"{movie.Title}_Data.csv";
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", fileName);
        }
        private string CsvEscape(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return $"\"{input.Replace("\"", "\"\"")}\"";
        }
    }
}
