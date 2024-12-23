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


        public string a = "a7a";
        //[BindProperty]
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
            name = name;
            Price = Price;

            HttpContext.Session.SetString("prod_name", name);
            HttpContext.Session.SetString("prod_price", Price.ToString());
        }
		public IActionResult OnPost()
		{
			// Ensure the quantity is at least 1
			if (order_quantity < 1)
			{
				ModelState.AddModelError(string.Empty, "Quantity must be at least 1.");

				// Reload item data to retain details on the page
				LoadItemDetails();

				return Page(); // Stay on the same page
			}

			string namee = HttpContext.Session.GetString("prod_name");
			float pricee = !string.IsNullOrEmpty(HttpContext.Session.GetString("prod_price")) ? float.Parse(HttpContext.Session.GetString("prod_price")) : 0;

			Medicine medicine = new Medicine
			{
				Id = id,
				Price = pricee,
				Name = namee
			};

			Cosmetics cosmetics = new Cosmetics
			{
				Id = id,
				Price = pricee,
				Name = namee
			};

			int count = db.check(id);
			string jsonstring;

			if (count == 1)
			{
				jsonstring = JsonSerializer.Serialize(cosmetics);
			}
			else
			{
				jsonstring = JsonSerializer.Serialize(medicine);
			}

			return RedirectToPage("/Order_details", new { order_quantity = order_quantity, jsonstring = jsonstring, type = count });
		}

		// Helper method to reload item data
		private void LoadItemDetails()
		{
			int count = db.check(id);
			if (count == 1)
			{
				DataTable dt = db.Viewcosmetics(id);
				Price = float.Parse(dt.Rows[0]["price"].ToString() ?? "0");
				name = dt.Rows[0]["name"].ToString();
				Manufacturer = dt.Rows[0]["manufacturer"].ToString();
				Type = dt.Rows[0]["type"].ToString();
				Description = dt.Rows[0]["Description"].ToString();
			}
			else
			{
				DataTable dt = db.ViewMedicine(id);
				Price = float.Parse(dt.Rows[0]["price"].ToString() ?? "0");
				name = dt.Rows[0]["name"].ToString();
				Manufacturer = dt.Rows[0]["manufacturer"].ToString();
				Dosage = dt.Rows[0]["dosage"].ToString();
				Active_Ingredients = dt.Rows[0]["Active_Ingredient"].ToString();
				Form = dt.Rows[0]["form"].ToString();
			}
		}

	}
}
