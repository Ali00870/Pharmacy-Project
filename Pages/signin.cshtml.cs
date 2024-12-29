using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class signinModel : PageModel
    {
        public DB db { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public signinModel(DB db)
        {
            this.db = db;
        }

        public IActionResult OnPost()
        {
            int counter = db.checkusername(Username, Password);
            if (counter == 1)
            {
                HttpContext.Session.SetString("username", Username);

                int isPharmacist = db.checkPharmacistUsers(Username, Password);
                if (isPharmacist == 1)
                {
                    if (Username == "pharmacist10")
                    {
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        DataTable dt = db.getPharmacy(Username);

                        string pharmName = dt.Rows[0]["pharmacyname"].ToString();
                        HttpContext.Session.SetString("pharmacy", pharmName);
                        return RedirectToPage("Index", new { showAddItem = true });
                    }
                }
                else
                {
                    
                    return RedirectToPage("Index");
                }
            }
            else
            {
                string message = "The username or password is invalid.";
                return RedirectToPage("/signin", new { message });
            }
        }
    }
}