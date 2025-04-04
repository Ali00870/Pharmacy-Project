using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System;
using System.ComponentModel.DataAnnotations;

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
        [MinLength(3,ErrorMessage ="Password must be exactly 8 characters")]
       
        public string Password { get; set; }
        [BindProperty]
        [Compare(nameof(Password),ErrorMessage ="Repeated password must match the password")]
        public string repeatPassword {  get; set; }
        [BindProperty]
       
        public string Name { get; set; }
        [BindProperty]
        
        public string PhoneNumber { get; set; }


        public Edit_ProfileModel(DB database)
        {
            db = database;
        }

        public void OnGet()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            //{ Username = HttpContext.Session.GetString("username");
            //    return Page();
            //}
            //else { return RedirectToPage("/Index"); }
            Username = HttpContext.Session.GetString("username");
            
        }

        public IActionResult OnPost()
        {
           
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")))
                {
                    db.UpdateAccounts(Username, Name, District, Street, HouseNum, Email, Password, PhoneNumber);
                   // TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToPage("/ViewProfile");
                }
                else
                {
                    db.updateUserInfo(Username, Password, Email, Name);
                   // TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToPage("/ViewProfile");
                }
            
            
            
            
        }
    }
}
