using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.Data;

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
        public string Dosage { get; set; }
        [BindProperty]
        public string Active_Ingredients { get; set; }
        [BindProperty]
        public string Form { get; set; }
        [BindProperty]
        public string Type { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty(SupportsGet = true)]
        public string msg { get; set; }

        public DataTable AvailableNames { get; set; }

        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")) || HttpContext.Session.GetString("username") == "pharmacist10")
            {
                DB db = new DB();
                AvailableNames = db.GetAvailableProductNames();
                return Page();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }

        public IActionResult OnPost()
        {
            DB db = new DB();

            if (Category == "medicine")
            {
                bool isInserted = db.AddMedicine(Name, Price, Quantity, Manufacturer, Dosage, Active_Ingredients, Form);
                msg = isInserted ? "Item Added Successfully!" : "Quantity Updated Successfully!";
            }
            else if (Category == "cosmetic")
            {
                bool isInserted = db.AddCosmetic(Name, Price, Quantity, Manufacturer, Type, Description);
                msg = isInserted ? "Item Added Successfully!" : "Quantity Updated Successfully!";
            }
            return RedirectToPage("/Add_item", new { msg = msg });
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }
    }
}
