using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;

namespace Pharmacy_back.Pages
{
    public class adminstatisticsModel : PageModel
    {

        private readonly ILogger<adminstatisticsModel> _logger;

        public DB db { get; set; }
        public int users { get; set; }
        public int pharmacists { get; set; }
        public int customers { get; set; }
        public int stocked { get; set; }
        public int empty { get; set; }
        public int almostempty { get; set; }
        public int medsept { get; set; }
        public int medoct { get; set; }
        public int mednov { get; set; }
        public int cosmsept { get; set; }
        public int cosmoct { get; set; }
        public int cosmnov { get; set; }

        public adminstatisticsModel(ILogger<adminstatisticsModel> logger, DB db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == "pharmacist10")
            {
                users = db.Getusers();
                pharmacists = db.Getpharmacists();
                customers = db.Getcustomers();
                stocked = db.Getstocked();
                empty = db.Getoutofstock();
                almostempty = db.Getsmallstock();
                medsept = db.medicinesept();
                medoct = db.medicineoct();
                mednov = db.medicinenov();
                cosmsept = db.cosmeticssept();
                cosmoct = db.cosmeticsoct();
                cosmnov = db.cosmeticsnov();
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }

    }
}
