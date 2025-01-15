using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository;
using Bookly.DataAccess.Repository.Interface;
using Bookly.Models.Models;
using Bookly.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooklyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly BooklyDbContext _booklyDb;

        public CategoryController(IUnitOfWork db, BooklyDbContext booklyDb)

        {
            _work = db;
            _booklyDb = booklyDb;
        }
        public IActionResult Index()
        {
            var objCategoryList = _work.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _work.Category.Add(obj);
                _work.Save();
                TempData["success"] = "Category was created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _work.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _work.Category.Update(obj);
                _work.Save();
                TempData["success"] = "Category was updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _work.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var categoryFromDb = _work.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _work.Category.Delete(categoryFromDb);
            _work.Save();
            TempData["success"] = "Category was deleted successfully";
            return RedirectToAction("Index");



        }
    }
}
