using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DB d;
        [BindProperty(SupportsGet = true)]
        public bool AddItems { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool statistcs { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Edit { get; set; }
        [BindProperty(SupportsGet =true)]
        public bool Handle {  get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Allpharmacists { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Allemployeees { get; set; }
        public DataTable BestCosm { get; set; } = new DataTable();
        public DataTable BestMed { get; set; } = new DataTable();
        public IndexModel(ILogger<IndexModel> logger, DB d)
        {
            _logger = logger;
            this.d = d;
        }

       
            public void OnGet()
            {
                string? username = HttpContext.Session.GetString("username");


                if (username == "pharmacist10")
                {
                    AddItems = true;
                    statistcs = true;
                    Allpharmacists = true;
                    Allemployeees = true;
                    Edit = true;
                }

                BestCosm = d.bestsellingCosmetics();
                BestMed = d.bestsellingMedicine();
            }
        
        public IActionResult OnPost(int id)
        {
            return RedirectToPage("/View_Items", new { id = id });
        }
        public IActionResult OnPostAddItem()
        {

            return RedirectToPage("/Add_item");

        }
        public IActionResult OnPostAddItems()
        {
            return RedirectToPage("/Add_item");
        }
        public IActionResult OnPostStatistics()
        {
            return RedirectToPage("/adminstatistics");
        }
        public IActionResult OnPostAllPharmacists()
        {
            return RedirectToPage("/Allpharmacists");
        }
        public IActionResult OnPostAllEmployees()
        {
            return RedirectToPage("/Allempolyees");
        }
        public IActionResult OnPostHandleOrders() {

            return RedirectToPage("/HandleOrders");
        
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("/signin");
        }


    }
}