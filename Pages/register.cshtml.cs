using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Pharmacy_back.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Numerics;
namespace Pharmacy_back.Pages
{
    public class registerModel : PageModel
    {


        public DB db { get; set; }
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string district { get; set; }
        [BindProperty]
        public string street { get; set; }
        [BindProperty]
        public string phone_number { get; set; }
        [Required, Range(6, 8, ErrorMessage = "please enter password between (6 - 8) Letters")]
        [BindProperty]
        public string password { get; set; }
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string repeatpassword { get; set; }
        public void OnGet()
        {
        }
        public registerModel(DB db)
        {
            this.db = db;

        }
        public IActionResult OnPostRegister()
        {
            if (db.checkusername(username, "sdj") ==0)
            {

                if (password == repeatpassword)
                {
                    if (password.Length>=6&& password.Length<=8)
                    {

                        HttpContext.Session.SetString(username, password);
                        db.InsertNewUsers(username, email, name, password, district, street, phone_number);
                        HttpContext.Session.SetString("username",username);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        string message = "please enter password between(6 - 8) Letters";
                        return RedirectToPage("/register", new { message = message });
                        //return Page();
                    }
                }
                else
                {
                    string message = "The password isn't identical ";
                    return RedirectToPage("/register", new { message = message });
                }
            }
            else
            {
                string message = "The Username already exists";
                return RedirectToPage("/register", new { message = message });
            }
            
        }
    }
}