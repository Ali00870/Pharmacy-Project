using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;

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
        { int count = 0;
            int counter = 0;
            counter = db.checkusername(Username, Password);
            Console.WriteLine(counter);
            if (counter  ==1) {
                HttpContext.Session.SetString("username", Username);
                count = db.checkPharmacistUsers(Username, Password);
                if (count == 1)
                {

                    return RedirectToPage("Index", new { showAddItem = true });
                }
                else
                {
                    return RedirectToPage("Index");
                }
            }
            else
            { string message = "The user name is in invalid or password";
                return RedirectToPage("/signin", new {message=message});
            }
        }  
                        
        
    }
}
