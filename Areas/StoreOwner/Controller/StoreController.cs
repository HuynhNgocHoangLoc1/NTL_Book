using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NTL_Book.Data;
using NTL_Book.Helpers;
using NTL_Book.Models;

namespace NTL_Book.Areas.StoreOwner.Controller;
[Area("StoreOwner")]
    [Authorize(Roles = Roles.StoreOwner)]
    public class StoreController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly NTL_BookDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        private string? _storeId;

        private string StoreId
        {
            get
            {
                _storeId = _storeId ?? userManager.GetUserId(User);
                return _storeId;
            }
        }

        public StoreController(NTL_BookDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> IndexStore()
        {
            var store = await _context.Stores
                .Include(s => s.StoreOwner)
                .FirstOrDefaultAsync(s => s.Id == StoreId);
            if (store == null)
            {
                return RedirectToAction(nameof(CreateStore));
            }

            return View(store);
        }

        // GET: StoreOwner/Create
        public IActionResult CreateStore()
        {
            var model = new Store()
            {
                Id = StoreId,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStore(Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexStore));
            }

            return View(store);
        }

        // GET: StoreOwner/Edit/5
        public async Task<IActionResult> EditStore()
        {
            var store = await _context.Stores.FindAsync(StoreId);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStore(Store store)
        {
            if (StoreId != store.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(store);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(IndexStore));
            }
            return View(store);
        }
    }
