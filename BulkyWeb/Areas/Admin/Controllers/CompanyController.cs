using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Interface;
using Bookly.Models.ViewModels;
using Bookly.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooklyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly BooklyDbContext _booklyDb;
        public CompanyController(IUnitOfWork db, BooklyDbContext booklyDb)

        {
            _work = db;
            _booklyDb = booklyDb;
        }
        public IActionResult Index()
        {
            var objCompanyList = _work.Company.GetAll().ToList();

            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                Company companyobj = _work.Company.Get(u => u.Id == id);
                return View(companyobj);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {

                if (CompanyObj.Id == 0)
                {
                    _work.Company.Add(CompanyObj);
                }

                else
                {
                    _work.Company.Update(CompanyObj);
                }

                _work.Save();
                TempData["success"] = "Category was updated successfully";
                return RedirectToAction("Index");
            }
            else
            {

                return View(CompanyObj);
            }
        }
        #region API CALLS


        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> CompanyList = _work.Company.GetAll().ToList();

            return Json(new { data = CompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _work.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _work.Company.Delete(CompanyToBeDeleted);
            _work.Save();

            return Json(new { success = true, message = "Deleted Successfully" });


        }

        #endregion
    }
}

