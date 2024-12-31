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


        public string District { get; set; }
        [BindProperty]

        public string Street { get; set; }
        [BindProperty]
        public int HouseNum { get; set; }
        [BindProperty]


        public string Email { get; set; }

        //[MinLength(3,ErrorMessage ="Password must be exactly 8 characters")]

        //public string Password { get; set; }
        //[BindProperty]
        //[Compare(nameof(Password),ErrorMessage ="Repeated password must match the password")]
        //public string repeatPassword {  get; set; }
        [BindProperty]

        public string Name { get; set; }
        [BindProperty]

        public string PhoneNumber { get; set; }
        [BindProperty]
        public double salary { get; set; }
        [BindProperty]
        public int shift_hours { get; set; }


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

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")))
            {
                db.UpdateAccounts(Username, Name, District, Street, HouseNum, Email, PhoneNumber);
                // TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToPage("/ViewProfile");
            }
            else
            {
                db.updateUserInfo(Username, Email, Name, PhoneNumber);
                // TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToPage("/ViewProfile");
            }




        }

    }
}
