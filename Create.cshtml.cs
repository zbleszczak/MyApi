using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApi.Models;
using MyApi.Data;

namespace MyApi.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MyDbContext _db;

        public CreateModel(MyDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product? Product { get; set; } // Zadeklaruj jako nullable

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Products.Add(Product);
            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
