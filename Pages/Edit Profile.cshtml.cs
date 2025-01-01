using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class Edit_ProfileModel : PageModel
    {
        private readonly DB db;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]

        public string? District { get; set; }
        [BindProperty]
        public string? Street { get; set; }
        [BindProperty]
        public int? HouseNum { get; set; }
        [BindProperty]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]

        public string Email { get; set; }

        
        [BindProperty]
        [MaxLength(50,ErrorMessage ="Name must not exceed 50 charcters")]
        public string Name { get; set; }
        [BindProperty]
        [MaxLength(15,ErrorMessage ="Phone number must not exceed 15 digits")]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public double? salary { get; set; }
        [BindProperty]
        public int? shift_hours { get; set; }


        public Edit_ProfileModel(DB database)
        {
            db = database;
        }

        public void OnGet()
        {

            Username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")))
            {
                DataTable dt = db.viewProfile(Username);
                Name = dt.Rows[0]["Name"].ToString();
                Email = dt.Rows[0]["email"].ToString();
                // Password = dt.Rows[0]["password"].ToString();
                //repeatPassword = dt.Rows[0]["password"].ToString();
                Street = dt.Rows[0]["street"].ToString();
                District = dt.Rows[0]["district"].ToString();
                HouseNum = (int)dt.Rows[0]["house_number"];
                PhoneNumber = dt.Rows[0]["mainphone"].ToString();

            }
            else
            {
                DataTable dt = db.viewProfile(Username);
                Name = dt.Rows[0]["Name"].ToString();
                Email = dt.Rows[0]["email"].ToString();
                //Password = dt.Rows[0]["password"].ToString();
                //repeatPassword = dt.Rows[0]["password"].ToString();
                salary = (double)dt.Rows[0]["salary"];
                shift_hours = (int)dt.Rows[0]["shift_hours"];
                PhoneNumber = dt.Rows[0]["mainphone"].ToString();

            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")))
                {
                    db.UpdateAccounts(Username, Name, District, Street, HouseNum, Email, PhoneNumber);
                   
                    return RedirectToPage("/ViewProfile");
                }
                else
                {
                    db.updateUserInfo(Username, Email, Name, PhoneNumber);
                    
                    return RedirectToPage("/ViewProfile");
                }

            }
            else
            {
                return Page();
            }


        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }

    }
}
