using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly RazorPageDbContext _db;
        [BindProperty]
        public Category category { get; set; }

        public EditModel(RazorPageDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                category = _db.Categories.Find(id);
            }

        }
        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();

        }
    }
}
