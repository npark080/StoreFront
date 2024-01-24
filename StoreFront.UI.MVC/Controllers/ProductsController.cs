using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using System.Drawing;
using StoreFront.UI.MVC.Utilities;

namespace StoreFront.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly StardewContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(StardewContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = _context.Products
                .Where(p => p.Season == "yearly" || p.Season == "winter")
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.OrderDetails);
            return View(await products.ToListAsync());
        }

        // GET: Products
        [AllowAnonymous]
        public async Task<IActionResult> TiledIndex()
        {
            var products = _context.Products
                .Where(p => p.Season == "yearly" || p.Season == "winter")
                .Include(p => p.Category)
                .Include(p => p.Supplier);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,ProductDescription,UnitsInStock,Season,CategoryId,SupplierId,ImageName, ImageFile")] Product product)
        {
            if (ModelState.IsValid)
            {
                #region File Upload - CREATE
                if (product.ImageFile != null && product.ImageFile.Length < 4_194_303)
                {
                    product.ImageName = Guid.NewGuid() + Path.GetExtension(product.ImageString);

                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string fullImagePath = webRootPath + "/images/";

                    using var memoryStream = new MemoryStream();
                    await product.ImageFile.CopyToAsync(memoryStream);

                    using Image img = Image.FromStream(memoryStream);

                    int maxImageSize = 500;
                    int maxThumbSize = 100;
                    ImageUtility.ResizeImage(fullImagePath, product.ImageName, img, maxImageSize, maxThumbSize);
                }
                else
                {
                    product.ImageName = "noimage.png";
                }
                #endregion
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,ProductDescription,UnitsInStock,Season,CategoryId,SupplierId,ImageName, ImageFile")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region File Upload - EDIT
                string? oldImageString = product.ImageName;
                if (product.ImageFile != null && product.ImageFile.Length < 4_194_303)
                {                  
                    product.ImageName = Guid.NewGuid() + Path.GetExtension(product.ImageString);

                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string fullImagePath = webRootPath + "/images/";

                    if (oldImageString != null && oldImageString != "noimage.png")
                    {
                        ImageUtility.Delete(fullImagePath, oldImageString);
                    }

                    using var memoryStream = new MemoryStream();
                    await product.ImageFile.CopyToAsync(memoryStream);
                    using Image img = Image.FromStream(memoryStream);

                    int maxImageSize = 500;
                    int maxThumbSize = 100;
                    ImageUtility.ResizeImage(fullImagePath, product.ImageName, img, maxImageSize, maxThumbSize);
                }
                #endregion
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'StardewContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
