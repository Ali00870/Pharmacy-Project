using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;

namespace Pharmacy_back.Pages
{
    public class Add_itemModel : PageModel
    {
        [BindProperty]
        public int Product_ID { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public float Price { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public string Manufacturer { get; set; }
        [BindProperty]
        public string Category { get; set; }
        [BindProperty]
        public string Dosage { get; set; } // Medicine only
        [BindProperty]
        public string Active_Ingredients { get; set; } // Medicine only
        [BindProperty]
        public string Form { get; set; } // Medicine only
        [BindProperty]
        public string Type { get; set; } // Cosmetics only
        [BindProperty]
        public string Description { get; set; } // Cosmetics only

        public IActionResult OnPost()
        {
            DB db = new DB();

            if (Category == "medicine")
            {
                db.AddMedicine(Product_ID, Name, Price, Quantity, Manufacturer, Dosage, Active_Ingredients, Form);
            }
            else if (Category == "cosmetic")
            {
                db.AddCosmetic(Product_ID, Name, Price, Quantity, Manufacturer, Type, Description);
            }

            return RedirectToPage("Index"); 
        }
    }
}
