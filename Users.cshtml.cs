using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
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
        public void OnGet()
        {
            customers = db.Getcustomers();
            userdata= db.Getuserdata();
        }
    }
}
