using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Text.Json;

namespace Pharmacy_back.Pages
{
    public class View_ItemsModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost() { 
            Medicine medicine = new Medicine();
            medicine.Quantity = 1;
            medicine.Price = 25;
            HttpContext.Session.SetString("MedObject", JsonSerializer.Serialize(medicine));
            return RedirectToPage("/Order_Details");

        
        
        }
    }
}
