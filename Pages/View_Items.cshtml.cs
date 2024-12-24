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
        [BindProperty]
        public string img { get; set; }
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
                img = dt.Rows[0]["img"].ToString();

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
                img = dt.Rows[0]["img"].ToString();

            }
            name = name;
            Price = Price;
            HttpContext.Session.SetString("img", img);
            HttpContext.Session.SetString("prod_name", name);
            HttpContext.Session.SetString("prod_price", Price.ToString());
        }
        public IActionResult OnPost()
        {
            string namee = HttpContext.Session.GetString("prod_name");
            float pricee = !string.IsNullOrEmpty(HttpContext.Session.GetString("prod_price")) ? float.Parse(HttpContext.Session.GetString("prod_price")) : 0;
            Medicine medicine = new Medicine();
            medicine.Id = id;
            string imgg = HttpContext.Session.GetString("img");
            medicine.img= imgg;
            medicine.Price = pricee;
            medicine.Name = namee;
            
            Cosmetics cosmetics = new Cosmetics();
            cosmetics.Id = id; cosmetics.Price = pricee; cosmetics.Name = namee;
            cosmetics.img = imgg;
            int count = db.check(id);
            string jsonstring;
          
            if (count == 1)
            {
               
                 jsonstring = JsonSerializer.Serialize(cosmetics);
                
            }
            else
            {
                
                jsonstring = JsonSerializer.Serialize(medicine);
                //return RedirectToPage("/Order_details", new { order_quantity = order_quantity, jsonstring = jsonstring,type=type });
            }
            //HttpContext.Session.Remove("img");

            return RedirectToPage("/Order_details", new { order_quantity = order_quantity, jsonstring = jsonstring, type = count });
        }
    }
}
