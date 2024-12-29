using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System;

namespace Pharmacy_back.Pages
{
    public class Edit_ProfileModel : PageModel
    {
        private readonly DB db;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string District { get; set; }
        [BindProperty]
        public string Street { get; set; }
        [BindProperty]
        public int HouseNum { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public Edit_ProfileModel(DB database)
        {
            db = database;
        }

        public void OnGet()
        {
            
            Username = HttpContext.Session.GetString("username");
            
        }

        public IActionResult OnPost()
        {
                db.UpdateAccounts(Username, District, Street, HouseNum, Email, Password);
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToPage("/Profile"); 
            
        }
    }
}
