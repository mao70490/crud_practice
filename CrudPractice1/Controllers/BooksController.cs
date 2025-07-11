using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudPractice1.Data;
using CrudPractice1.Models;
using CrudPractice1.Extensions;
using CrudPractice1.Enums;

namespace CrudPractice1.Controllers
{
    public class BooksController : Controller
    {
        private readonly CrudPractice1Context _context;

        public BooksController(CrudPractice1Context context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(string titleFilter, string categoryFilter)
        {
            var books = _context.Book
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(titleFilter))
            {
                books = books.Where(b => b.Title.Contains(titleFilter));
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                books = books.Where(b =>
                    b.BookCategories.Any(bc => bc.Category.Name.Contains(categoryFilter)));
            }

            ViewData["TitleFilter"] = titleFilter;
            ViewData["CategoryFilter"] = categoryFilter;

            return View(await books.ToListAsync());
        }


        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["FormatList"] = GetFormatSelectList();
            ViewBag.AllCategories = _context.Categories.ToList();
            return View(new Book());
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, List<int> SelectedCategoryIds)
        {
            if (ModelState.IsValid)
            {
                // 加入 BookCategories 中介關聯
                if (SelectedCategoryIds != null && SelectedCategoryIds.Any())
                {
                    book.BookCategories = SelectedCategoryIds
                        .Select(cid => new BookCategory
                        {
                            CategoryId = cid,
                            Book = book
                        }).ToList();
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "建立成功！";
                return RedirectToAction(nameof(Index));
            }
            ViewData["FormatList"] = GetFormatSelectList();
            ViewBag.AllCategories = await _context.Categories.ToListAsync();
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookCategories)       
                .ThenInclude(bc => bc.Category)       
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            // 確保將 Categories 資料傳遞給 ViewBag
            ViewBag.AllCategories = await _context.Categories.ToListAsync();
            ViewData["FormatList"] = GetFormatSelectList();
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book, List<int> SelectedCategoryIds)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. 刪除原有的 BookCategory 關聯
                    var existingBookCategories = _context.BookCategories
                        .Where(bc => bc.BookId == id)
                        .ToList();
                    _context.BookCategories.RemoveRange(existingBookCategories);

                    // 2. 新增勾選的 Category 關聯
                    if (SelectedCategoryIds != null && SelectedCategoryIds.Any())
                    {
                        foreach (var catId in SelectedCategoryIds)
                        {
                            _context.BookCategories.Add(new BookCategory
                            {
                                BookId = id,
                                CategoryId = catId
                            });
                        }
                    }

                    // 3. 更新 Book 本體資料
                    _context.Update(book);

                    // 儲存變更
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "編輯成功！";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
                        return NotFound();
                    else
                        throw;
                }
            }

            // 若 ModelState 無效，返回 View 並顯示錯誤
            ViewBag.AllCategories = await _context.Categories.ToListAsync();
            ViewData["FormatList"] = GetFormatSelectList();
            return View(book);
        }
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.BookId == id);
        }

        //下拉選單
        private List<SelectListItem> GetFormatSelectList()
        {
            return Enum.GetValues(typeof(FormatType))
                       .Cast<FormatType>()
                       .Select(f => new SelectListItem
                       {
                           Value = f.ToString(),
                           Text = f.GetDisplayName()
                       }).ToList();
        }
    }
}
