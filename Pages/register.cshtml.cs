using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
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
            //{   if (username.Contains("pharmacist"))
            //    {
            //        if (password == repeatpassword)
            //        {

            //            db.InsertNewUsers(username, email, name, password);
            //            return RedirectToPage("/Index", new {});
            //        }
            //        else
            //        {
            //            string message = "The password isn't identical ";
            //            return RedirectToPage("/register", new { message = message });
            //        }
            //    }
            // else
            {
                if (password == repeatpassword)
                {

                    HttpContext.Session.SetString(username, password);
                    db.InsertNewUsers(username, email, name, password, district, street, phone_number);
                    return RedirectToPage("/Index");
                }
                else
                {
                    string message = "The password isn't identical ";
                    return RedirectToPage("/register", new { message = message });
                }
            }
        }
    }
}