using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class ViewProfileModel : PageModel
    {
        private readonly DB dB;
        public DataTable profile {  get; set; }=new DataTable();
        public ViewProfileModel(DB d)
        {
            dB = d;
        }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {

                string username=HttpContext.Session.GetString("username").ToString();
                profile=dB.viewProfile(username);
                return Page();
                
            }
            else { return RedirectToPage("/Index"); }
        }
    }
}
