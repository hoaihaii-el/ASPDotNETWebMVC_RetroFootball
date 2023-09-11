using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroFootballWeb.Models;
using RetroFootballWeb.Repository;
using RetroFootballWeb.Repository.Help;

namespace RetroFootballWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Active"] = "Product";
            return View(await _context.Products.ToListAsync());
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product)
        {
            if (String.IsNullOrEmpty(product.ID))
            {
                product.ID = HelperFunction.Help.AutoID("PD", _context.Products.Take(1).OrderByDescending(p => p.ID).Select(p => p.ID).FirstOrDefault());
                product.Time_added = DateTime.Now;
                product.Status = "Available";
                if (product.ImageUpLoad != null)
                {
                    string upLoadDir = Path.Combine(_webHostEnvironment.WebRootPath,"img/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpLoad.FileName;
                    string filePath = Path.Combine(upLoadDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpLoad.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;
                }
                if (HelperFunction.Help.ValidProduct(product))
                {
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Add successfully!";
                }
                else
                {
                    TempData["SuccessMessage"] = "Some field is missing!";
                }

                return RedirectToAction("Index");
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(string productID)
        {
            if (productID == null) return NotFound();
            var product = await _context.Products.FindAsync(productID);

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (HelperFunction.Help.ValidProduct(product))
            {
                try
                {
                    if (product.ImageUpLoad != null)
                    {
                        string upLoadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img/products");
                        string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpLoad.FileName;
                        string filePath = Path.Combine(upLoadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await product.ImageUpLoad.CopyToAsync(fs);
                        fs.Close();
                        product.Image = imageName;
                    }
                    else product.Image = await _context.Products.Where(p => p.ID == product.ID)
                            .Select(p => p.Image).FirstOrDefaultAsync();

                    product.Status = await _context.Products.Where(p => p.ID == product.ID)
                            .Select(p => p.Status).FirstOrDefaultAsync();

                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Edit product successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string productID)
        {
            if (productID == null) return NotFound();
            var product = await _context.Products.FindAsync(productID);

            if (!String.IsNullOrEmpty(product.Image))
            {
                string upLoadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img/products");
                string filePath = Path.Combine(upLoadDir, product.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Delete product successfully!";
            return RedirectToAction("Index");
        }
    }
}
