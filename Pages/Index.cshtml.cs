using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Pages.Models;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DB d;
        
        public DataTable BestCosm { get; set; }=new DataTable();
        public DataTable BestMed {  get; set; }=new DataTable();
        public IndexModel(ILogger<IndexModel> logger,DB d)
        {
            _logger = logger;
            this.d = d;
        }

        public void OnGet()
        {
            BestCosm = d.bestsellingCosmetics();
            BestMed = d.bestsellingMedicine();
        }
        public IActionResult OnPost(int id)
        {
            return RedirectToPage("/View_Items", new { id = id });
        }
    }
}
