using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Data;
using System.Text.Json;

namespace Pharmacy_back.Pages
{

    public class View_ItemsModel : PageModel
    {

        public DB db { get; set; }
        public View_ItemsModel(DB db)
        {
            this.db = db;
        }



        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public float Price { get; set; }
        [BindProperty]
        public string Manufacturer { get; set; }
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

        [BindProperty] public int order_quantity { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public void OnGet()
        {
            int count = db.check(id);
            if (count == 1)
            {
                DataTable dt;
                dt = db.Viewcosmetics(id);
                var pricestring = dt.Rows[0]["price"].ToString();
                Price = !string.IsNullOrEmpty(pricestring) ? float.Parse(pricestring) : 0;
                name = dt.Rows[0]["name"].ToString();
                Manufacturer = dt.Rows[0]["manufacturer"].ToString();
                Type = dt.Rows[0]["type"].ToString();
                Description = dt.Rows[0]["Description"].ToString();


            }
            else
            {
                DataTable dt;
                dt = db.ViewMedicine(id);
                var pricestring = dt.Rows[0]["price"].ToString();
                Price = !string.IsNullOrEmpty(pricestring) ? float.Parse(pricestring) : 0;
                name = dt.Rows[0]["name"].ToString();
                Manufacturer = dt.Rows[0]["manufacturer"].ToString();
                Dosage = dt.Rows[0]["dosage"].ToString();
                Active_Ingredients = dt.Rows[0]["Active_Ingredient"].ToString();
                Form = dt.Rows[0]["form"].ToString();


            }

        }
        public IActionResult OnPost()
        {

            Medicine medicine = new Medicine();
            medicine.Id = id;
            medicine.Price = Price;
            medicine.Name = name;

            Cosmetics cosmetics = new Cosmetics();
            cosmetics.Id = id; cosmetics.Price = Price; cosmetics.Name = name;

            int count = db.check(id);
            if (count == 1)
            {
                string jsonstring = JsonSerializer.Serialize(medicine);
                return RedirectToPage("/Order_details", new { order_quantity = order_quantity, jsonstring = jsonstring });
            }
            else
            {

                string jsonstring = JsonSerializer.Serialize(cosmetics);
                return RedirectToPage("/Order_details", new { order_quantity = order_quantity, jsonstring = jsonstring });
            }
        }
    }
}
