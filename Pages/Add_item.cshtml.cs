using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;

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
        [BindProperty(SupportsGet =true)]
        public string msg {  get; set; }
        public IActionResult OnPost()
        {
            DB db = new DB();

            //if (Category == "medicine")
            //{
            //    if (db.isidexist(Product_ID))
            //    {
            //        db.UpdateProductsQuantity(Product_ID, Quantity);
            //    }
            //    else
            //    {
            //        db.AddMedicine(Product_ID, Name, Price, Quantity, Manufacturer, Dosage, Active_Ingredients, Form);
            //    }
            //}
            //else if (Category == "cosmetic")
            //{
            //    if (db.isidexist(Product_ID))
            //    {

            //        db.UpdateProductsQuantity(Product_ID, Quantity);
            //    }
            //    else
            //    {
            //        db.AddCosmetic(Product_ID, Name, Price, Quantity, Manufacturer, Type, Description);
            //    }
            //}

            //return RedirectToPage("Index");
            if (Category == "medicine")
            {
              bool isInserted=  db.AddMedicine(Name, Price, Quantity, Manufacturer, Dosage, Active_Ingredients, Form);
                if (isInserted) { msg = "Item Added Successfully!"; }
                else { msg = "Quantity Updated Succesfully!"; }

            }
            else if(Category=="cosmetic" ){

               bool isInserted= db.AddCosmetic(Name, Price, Quantity, Manufacturer, Type, Description);
                if (isInserted) { msg = "Item Added Successfully!"; }
                else { msg = "Quantity Updated Succesfully!"; }

            }
            return RedirectToPage("/Add_item", new {msg=msg});
        }
        public IActionResult OnGet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy"))||HttpContext.Session.GetString("username")=="pharmacist10")
            {
                return Page();
            }

            else
            {
                return RedirectToPage("Index");
            }
        }
        public IActionResult OnPostLogout()
        {
            //HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }
    }
}

