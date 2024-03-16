using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly RazorPageDbContext _db;
        [BindProperty]
        public Category category { get; set; }

        public CreateModel(RazorPageDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["success"] = "Category added successfully";
            return RedirectToPage("Index");
        }
    }
}
