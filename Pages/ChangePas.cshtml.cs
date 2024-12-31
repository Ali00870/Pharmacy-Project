using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_back.Pages
{
    public class ChangePasModel : PageModel
    {
        private readonly DB db;
        public ChangePasModel(DB db)
        {
            this.db = db;
        }
        [BindProperty]
        [Required]
        [MaxLength(8, ErrorMessage = "Password must be at most 8 letters (e.g. asp43218)")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 letters")]
        public string Password { get; set; }
        [BindProperty]
        [Compare(nameof(Password), ErrorMessage = "Repeated password doesn't match your password")]
        public string repeatPassword { get; set; }
        public string Username { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("username");
        }
        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();

            }
            else
            {
                Username = HttpContext.Session.GetString("username");
                int s = db.Updatepass(Username, Password);
                return RedirectToPage("/ViewProfile");
            }
        }

    }
}

