using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApi.Models;
using MyApi.Data;
using Microsoft.EntityFrameworkCore; // Dodaj tę linię

namespace MyApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyDbContext _db;

        public IndexModel(MyDbContext db)
        {
            _db = db;
        }

        public IList<Product>? Products { get; set; } // Zadeklaruj jako nullable

        public async Task OnGetAsync()
        {
            Products = await _db.Products.ToListAsync();
        }
    }
}
