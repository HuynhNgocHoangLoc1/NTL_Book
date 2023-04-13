using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTL_Book.Areas.Identity.Data;
using NTL_Book.ViewModels;

namespace NTL_Book.Controllers;

public class BooksController : Controller
{
    private readonly NTL_BookDbContext _context;

    public BooksController(NTL_BookDbContext context)
    {
        _context = context;
    }

    // GET: Books
    public async Task<IActionResult> Index(SearchViewModel? model)
    {
        var query = _context.Books
            .Include(b => b.Category)
            .Include(b => b.Image)
            .AsQueryable();

        if (model != null && !string.IsNullOrWhiteSpace(model.KeyWord))
        {
            var keyword = model.KeyWord.Trim().ToLower();
            query = query.Where(b => b.Title.ToLower().Contains(keyword)
                                     || b.Category.Name.ToLower().Contains(keyword));
        }

        return View(await query.ToListAsync());
    }

    // GET: Books/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Books == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.Category)
            .Include(b => b.Store)
            .Include(b => b.Image)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }
}