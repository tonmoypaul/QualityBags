using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualityBags.Data;
using QualityBags.Models;
using QualityBags.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;


namespace QualityBags.Controllers
{
    public class BagsController : Controller
    {
        private readonly QbDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BagsController(QbDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Bags
        public async Task<IActionResult> Index()
        {
            var qbDbContext = _context.Bags.Include(b => b.Category).Include(b => b.Supplier);

            return View(await qbDbContext.ToListAsync());
        }

        // GET: Bags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags
                .Include(b => b.Category)
                .Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // GET: Bags/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
            
            return View();
        }

        // POST: Bags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BagViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Bag.ImagePath = "";

                if (model.ImageFile != null)
                {
                    var file = model.ImageFile;

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'); // FileName returns "fileName.ext"(with double quotes)

                    if (fileName.EndsWith(".jpg"))// Important for security
                    {
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        model.Bag.ImagePath = fileName;

                    }
                }

                _context.Add(model.Bag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "ID", model.Bag.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "ID", model.Bag.SupplierID);

            return View(model);
        }

        // GET: Bags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags.SingleOrDefaultAsync(m => m.ID == id);
            if (bag == null)
            {
                return NotFound();
            }

            var viewModel = new BagViewModel
            {
                Bag = bag
            };

            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", bag.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", bag.SupplierID);
            return View(viewModel);
        }

        // POST: Bags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BagViewModel viewModel)
        {
            if (id != viewModel.Bag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                viewModel.Bag.ImagePath = "";

                if (viewModel.ImageFile != null)
                {
                    var file = viewModel.ImageFile;

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'); // FileName returns "fileName.ext"(with double quotes)

                    if (fileName.EndsWith(".jpg"))// Important for security
                    {
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        viewModel.Bag.ImagePath = fileName;

                    }
                }

                try
                {
                    _context.Update(viewModel.Bag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BagExists(viewModel.Bag.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", viewModel.Bag.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name", viewModel.Bag.SupplierID);

            return View(viewModel.Bag);
        }

        // GET: Bags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags
                .Include(b => b.Category)
                .Include(b => b.Supplier)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // POST: Bags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bag = await _context.Bags.SingleOrDefaultAsync(m => m.ID == id);
            _context.Bags.Remove(bag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BagExists(int id)
        {
            return _context.Bags.Any(e => e.ID == id);
        }
    }
}
