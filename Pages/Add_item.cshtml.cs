using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.Collections.Generic;

namespace Pharmacy_back.Pages
{
    public class Add_itemModel : PageModel
    {
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

        [BindProperty(SupportsGet = true)]
        public string msg { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> ProductNames { get; set; } = new List<string>();

        // Handle form submission
        public IActionResult OnPost()
        {
            DB db = new DB();

            // Validate if the product name exists and category is selected
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Category))
            {
                msg = "Please provide a product name and select a category.";
                return RedirectToPage("/Add_item", new { msg = msg });
            }

            // Add or update logic for Medicine or Cosmetic
            if (Category == "medicine")
            {
                bool isInserted = db.AddMedicine(Name, Price, Quantity, Manufacturer, Dosage, Active_Ingredients, Form);
                if (isInserted) { msg = "Item Added Successfully!"; }
                else { msg = "Quantity Updated Successfully!"; }
            }
            else if (Category == "cosmetic")
            {
                bool isInserted = db.AddCosmetic(Name, Price, Quantity, Manufacturer, Type, Description);
                if (isInserted) { msg = "Item Added Successfully!"; }
                else { msg = "Quantity Updated Successfully!"; }
            }

            return RedirectToPage("/Add_item", new { msg = msg });
        }

        // Populate product names on page load
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")) || HttpContext.Session.GetString("username") == "pharmacist10")
            {
                DB db = new DB();
                ProductNames = db.GetProductNames(); // Replace with your database call
                return Page();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }

        // Logout logic
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }
    }
}
