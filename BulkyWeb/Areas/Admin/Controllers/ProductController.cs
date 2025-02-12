using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository;
using Bookly.DataAccess.Repository.Interface;
using Bookly.Models.Models;
using Bookly.Models.ViewModels;
using Bookly.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BooklyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly BooklyDbContext _booklyDb;
        private readonly IWebHostEnvironment _webHost;
        public ProductController(IUnitOfWork db, BooklyDbContext booklyDb, IWebHostEnvironment webHost)

        {
            _work = db;
            _booklyDb = booklyDb;
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            var objCategoryList = _work.Product.GetAll(includeProperties: "Category").ToList();

            return View(objCategoryList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _work.Category.GetAll()
               .Select(c => new SelectListItem
               {
                   Text = c.Name,
                   Value = c.Id.ToString()
               }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _work.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHost.WebRootPath;
                if (file != null)
                {
                    string filName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string ProductPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImgPath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(ProductPath, filName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + filName;
                }
                if (productVM.Product.Id == 0)
                {
                    _work.Product.Add(productVM.Product);
                }

                else
                {
                    _work.Product.Update(productVM.Product);
                }

                _work.Save();
                TempData["success"] = "Category was updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _work.Category.GetAll()
                          .Select(c => new SelectListItem
                          {
                              Text = c.Name,
                              Value = c.Id.ToString()
                          });
                return View(productVM);
            }
        }
        #region API CALLS


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objproductList = _work.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = objproductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _work.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldImgPath = Path.Combine(_webHost.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImgPath))
            {
                System.IO.File.Delete(oldImgPath);
            }

            _work.Product.Delete(productToBeDeleted);
            _work.Save();

            return Json(new { success = true, message = "Deleted Successfully" });


        }

        #endregion
    }
}
