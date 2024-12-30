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
        [MaxLength(50,ErrorMessage = "District information must be less than 50 characters")]

        public string District { get; set; }
        [BindProperty]
        [MaxLength(50,ErrorMessage ="Street information must be less than 50 characters")]
        public string Street { get; set; }
        [BindProperty]
        public int HouseNum { get; set; }
        [BindProperty]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]

        public string Email { get; set; }
        [BindProperty]
        [StringLength(8,ErrorMessage ="Password must be exactly 8 characters")]
       
        public string Password { get; set; }
        [BindProperty]
        //[Compare(nameof(Password),ErrorMessage ="Repeated password must match the password")]
        public string repeatPassword {  get; set; }
        [BindProperty]
        [MaxLength(50,ErrorMessage ="Name must be less than 50 characters")]
        public string Name { get; set; }
        [BindProperty]
        [MaxLength(15,ErrorMessage ="Phone number must be less than 15 digits")]
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
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")))
                {
                    db.UpdateAccounts(Username, Name,District, Street, HouseNum, Email, Password, PhoneNumber);
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToPage("/ViewProfile");
                }
                else
                {
                    db.updateUserInfo(Username, Password, Email, Name);
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToPage("/ViewProfile");
                }
            }
            else
            {
                return Page();
            }
            
        }
    }
}
