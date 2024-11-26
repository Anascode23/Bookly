using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.Interface;
using Bookly.Models.ViewModels;
using Bookly.Models;
using Bookly.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooklyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _work;
        private readonly BooklyDbContext _booklyDb;
        public CompanyController(IUnitOfWork work, BooklyDbContext booklyDb)

        {
            _work = work;
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
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company Companyobj = _work.Company.Get(u => u.Id == id);
                return View(Companyobj);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Company Companyobj)
        {
            if (ModelState.IsValid)
            {
                if (Companyobj.Id == 0)
                {
                    _work.Company.Add(Companyobj);
                }

                else
                {
                    _work.Company.Update(Companyobj);
                }

                _work.Save();
                TempData["success"] = "Category was updated successfully";
                return RedirectToAction("Index");
            }


            else
            {
                return View(Companyobj);
            }
        }

        #region API CALLS


        [HttpGet]
        public IActionResult GetCompanyList() //There is something wrong with this method
        {
            var objCompanyList = _work.Company.GetAll().ToList();

            return Json(new { data = objCompanyList });
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


