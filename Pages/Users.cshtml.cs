using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class UsersModel : PageModel
    {
        private readonly ILogger<UsersModel> _logger;

        public DB db { get; set; }
        public int customers { get; set; }
        public DataTable userdata { get; set; }
        public UsersModel(ILogger<UsersModel> logger, DB db)
        {
            _logger = logger;
            this.db = db;
        }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == "pharmacist10")
            {
                customers = db.Getcustomers();
                userdata = db.Getuserdata();
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("/signin");
        }
    }
}
