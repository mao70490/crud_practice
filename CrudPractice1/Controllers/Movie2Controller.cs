 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudPractice1.Data;
using CrudPractice1.Models;
using CrudPractice1.Enums;
using System.ComponentModel.DataAnnotations;
using CrudPractice1.Extensions; 




namespace CrudPractice1.Controllers
{
    public class Movie2Controller : Controller
    {
        private readonly CrudPractice1Context _context;

        public Movie2Controller(CrudPractice1Context context)
        {
            _context = context;
        }

        // GET: Movie2
        public async Task<IActionResult> Index(string searchString)
        {
            // 保留使用者搜尋字串供 View 使用（讓輸入欄顯示原輸入值）
            ViewData["CurrentFilter"] = searchString;

            var movie2List = await _context.Movie2.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearch = searchString.ToLower();

                movie2List = movie2List.Where(s =>
                    (!string.IsNullOrEmpty(s.Title) && s.Title.ToLower().Contains(lowerSearch)) ||
                    (s.Genre != null && s.Genre.Value.GetDisplayName().Contains(searchString))
                ).ToList();
            }

            return View(movie2List);
        }

        // GET: Movie2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie2 = await _context.Movie2
                .Include(m => m.MovieLanguages)
                .ThenInclude(ml => ml.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie2 == null)
            {
                return NotFound();
            }

            return View(movie2);
        }

        // GET: Movie2/Create
        public async Task<IActionResult> Create()
        {
            // Genre 下拉選單
            ViewBag.Genres = Enum.GetValues(typeof(GenreType))
                .Cast<GenreType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.GetDisplayName()
                }).ToList();

            // 語言清單從資料庫抓
            ViewBag.AllLanguages = await _context.Languages
                .Select(l => l.Code)
                .ToListAsync();

            return View(new Movie2());
        }

        // POST: Movie2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie2 movie2, List<string> SupportedLanguages, IFormFile PosterFile)
        {
            // Genre 下拉選單
            ViewBag.Genres = Enum.GetValues(typeof(GenreType))
                         .Cast<GenreType>()
                         .Select(e => new SelectListItem
                         {
                             Value = ((int)e).ToString(),
                             Text = e.GetDisplayName()
                         }).ToList();

            // 語言清單從資料庫抓
            ViewBag.AllLanguages = await _context.Languages
                .Select(l => l.Code)
                .ToListAsync();

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
                        movie2.PosterUrl = "/uploads/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        // 加入錯誤訊息
                        ModelState.AddModelError("", "圖片上傳失敗：" + ex.Message);
                        return View(movie2);
                    }
                }

                if (SupportedLanguages != null)
                {
                    movie2.MovieLanguages = SupportedLanguages.Select(code => new MovieLanguage
                    {
                        LanguageCode = code
                    }).ToList();
                }

                _context.Add(movie2);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "建立成功！";
                return RedirectToAction(nameof(Index));
            }
            else if (!ModelState.IsValid)
            {
                // 輸出 Model 驗證錯誤
                foreach (var modelState in ModelState)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        // 這裡你可以將錯誤訊息記錄到日誌或者直接回傳給使用者
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                return View(movie2);
            }

            return View(movie2);
        }

        // GET: Movie2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie2 = await _context.Movie2
                .Include(m => m.MovieLanguages)  // 載入 MovieLanguages
                .ThenInclude(ml => ml.Language)  // 載入 Language 資料
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie2 == null)
            {
                return NotFound();
            }

            // 傳遞所有可選語言
            ViewBag.AllLanguages = await _context.Languages
                .Select(l => l.Code)
                .ToListAsync();
            // 建立下拉選單
            ViewBag.GenreList = Enum.GetValues(typeof(GenreType))
                .Cast<GenreType>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.GetDisplayName(),
                    Selected = (movie2.Genre == g)
                }).ToList();

            return View(movie2);
        }

        // POST: Movie2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie2 movie2, List<string> SupportedLanguages)
        {
            if (id != movie2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. 清除舊的語言關聯
                    var existingMovieLanguages = _context.MovieLanguages.Where(ml => ml.Movie2Id == movie2.Id).ToList();
                    _context.MovieLanguages.RemoveRange(existingMovieLanguages);  // 刪除舊關聯

                    // 2. 新增選中的語言
                    if (SupportedLanguages != null)
                    {
                        foreach (var code in SupportedLanguages)
                        {
                            var movieLanguage = new MovieLanguage
                            {
                                Movie2Id = movie2.Id,
                                LanguageCode = code
                            };
                            _context.MovieLanguages.Add(movieLanguage); // 新增新的語言關聯
                        }
                    }

                    // 3. 更新電影資料
                    _context.Update(movie2);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "編輯成功！";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Movie2Exists(movie2.Id))
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
            // 建立下拉選單
            ViewBag.GenreList = Enum.GetValues(typeof(GenreType))
            .Cast<GenreType>()
            .Select(g => new SelectListItem
            {
                Value = ((int)g).ToString(),
                Text = g.GetDisplayName(),
                Selected = (movie2.Genre == g)
            }).ToList();

            // 傳遞所有可選語言
            ViewBag.AllLanguages = await _context.Languages
                .Select(l => l.Code)
                .ToListAsync();

            return View(movie2);
        }

        // GET: Movie2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie2 = await _context.Movie2
                .Include(m => m.MovieLanguages) // 載入 MovieLanguages
                .ThenInclude(ml => ml.Language) // 載入 MovieLanguage 對應的 Language
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie2 == null)
            {
                return NotFound();
            }

            return View(movie2);
        }

        // POST: Movie2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie2 = await _context.Movie2.FindAsync(id);
            if (movie2 != null)
            {
                _context.Movie2.Remove(movie2);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Movie2Exists(int id)
        {
            return _context.Movie2.Any(e => e.Id == id);
        }
    }
}
