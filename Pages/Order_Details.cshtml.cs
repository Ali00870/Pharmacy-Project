using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Pharmacy_back.Model;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace Pharmacy_back.Pages
{
    
    public class Order_DetailsModel : PageModel
    {
        private const string SessionKey = "MedicineList";
        private const string SessionKeyC = "CosmeticsList";

        [BindProperty(SupportsGet = true)]
        public Medicine M { get; set; } = new Medicine(); // Ensure initialized

        [BindProperty(SupportsGet = true)]
        public Cosmetics C { get; set; } = new Cosmetics(); // Ensure initialized
        [BindProperty]
        [Required(ErrorMessage ="Please Choose a Pharmacy")]
        public string SelectedItem { get; set; } // Property to hold the selected value

        public List<SelectListItem> Items { get; set; } = new List<SelectListItem>(); // List for dropdown options
        private readonly DB db;
        public DateTime OrderDate {  get; set; }
        [BindProperty(SupportsGet =true)]
        public string SelectMsg {  get; set; }
        public Order_DetailsModel(DB db)
        {
            this.db = db;
        }
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
        public List<Cosmetics> Cosmetics { get; set; } = new List<Cosmetics>();
        public float TotalPrice { get; set; } = 0;
        public string orderMessage {  get; set; }
        [BindProperty(SupportsGet =true)]
        public string username {  get; set; }
        [BindProperty(SupportsGet = true)]
        public  int order_quantity { get; set; }
        [BindProperty(SupportsGet = true)]
        public int type { get; set; } = -1;
        [BindProperty(SupportsGet = true)]
        public string jsonstring {  get; set; }



        public void OnGet()
        {
            // Load pharmacy list
            DataTable d = db.pharmacies();
            for (int i = 0; i < d.Rows.Count; i++)
            {
                SelectListItem li = new SelectListItem
                {
                    Value = d.Rows[i]["pharmacyname"].ToString(),
                    Text = d.Rows[i]["pharmacyname"].ToString()
                };
                Items.Add(li);
            }

            // Load existing Medicines from the session
            var medicineJson = HttpContext.Session.GetString(SessionKey);
            Medicines = !string.IsNullOrEmpty(medicineJson)
                ? JsonSerializer.Deserialize<List<Medicine>>(medicineJson)
                : new List<Medicine>();

            // Load existing Cosmetics from the session
            var cosmeticsJson = HttpContext.Session.GetString(SessionKeyC);
            Cosmetics = !string.IsNullOrEmpty(cosmeticsJson)
                ? JsonSerializer.Deserialize<List<Cosmetics>>(cosmeticsJson)
                : new List<Cosmetics>();

            // Load total price from the session
            var priceString = HttpContext.Session.GetString("totalPrice");
            TotalPrice = !string.IsNullOrEmpty(priceString) ? float.Parse(priceString) : 0;

            // Only add items if jsonstring is set AND a flag indicates it's from View_Items
            if (!string.IsNullOrEmpty(jsonstring) && HttpContext.Session.GetString("SourcePage") == "View_Items")
            {
                if (type == 0) // Medicine
                {
                    var MedObj = JsonSerializer.Deserialize<Medicine>(jsonstring);
                    if (MedObj != null && !string.IsNullOrEmpty(MedObj.Name))
                    {
                        MedObj.Quantity = order_quantity;
                        Medicines.Add(MedObj);
                        TotalPrice += MedObj.Price * MedObj.Quantity;
                    }
                }
                else if (type == 1) // Cosmetic
                {
                    var CosmObj = JsonSerializer.Deserialize<Cosmetics>(jsonstring);
                    if (CosmObj != null && !string.IsNullOrEmpty(CosmObj.Name))
                    {
                        CosmObj.Quantity = order_quantity;
                        Cosmetics.Add(CosmObj);
                        TotalPrice += CosmObj.Price * CosmObj.Quantity;
                    }
                }

                // Save updated lists and total price back to the session
                HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Medicines));
                HttpContext.Session.SetString(SessionKeyC, JsonSerializer.Serialize(Cosmetics));
                HttpContext.Session.SetString("totalPrice", TotalPrice.ToString("F2"));

                // Clear jsonstring and source flag after processing
                jsonstring = "";
                HttpContext.Session.Remove("SourcePage");
            }
        }




        public IActionResult OnPostAnotherItem()
        {
            return RedirectToPage("/allproducts");
        }
        public IActionResult OnPost()
        {
            if (!SelectedItem.IsNullOrEmpty())
            {
                string username = HttpContext.Session.GetString("username");
                if (string.IsNullOrEmpty(username)) { return RedirectToPage("/signin"); }

                var failedOrders = new List<string>();
                var successfulOrders = 0;
                var medicineJson = HttpContext.Session.GetString(SessionKey);
                Medicines = !string.IsNullOrEmpty(medicineJson)
                    ? JsonSerializer.Deserialize<List<Medicine>>(medicineJson)
                    : new List<Medicine>();

                // Load existing Cosmetics from the session
                var cosmeticsJson = HttpContext.Session.GetString(SessionKeyC);
                Cosmetics = !string.IsNullOrEmpty(cosmeticsJson)
                    ? JsonSerializer.Deserialize<List<Cosmetics>>(cosmeticsJson)
                    : new List<Cosmetics>();
                OrderDate = DateTime.Now;
                // Process Medicines
                foreach (var M in Medicines)
                {

                    int pid = M.Id;
                    int quantity = M.Quantity;

                    try
                    {
                        string msg = "f";
                        int done = db.InsertOrder(username, pid, quantity, SelectedItem, ref msg, OrderDate);
                        if (done == 1)
                        {
                            successfulOrders++;
                        }
                        else
                        {
                            failedOrders.Add($"Medicine ID: {pid}");

                        }
                    }
                    catch (Exception ex)
                    {

                        failedOrders.Add($"Medicine ID: {pid}, Error: {ex.Message}");
                    }
                }

                // Process Cosmetics
                foreach (var M in Cosmetics)
                {
                    int pid = M.Id;
                    int quantity = M.Quantity;

                    try
                    {

                        string msg = "f";
                        int done = db.InsertOrder(username, pid, quantity, SelectedItem, ref msg, OrderDate);
                        if (done == 1)
                        {
                            successfulOrders++;
                        }
                        else
                        {
                            failedOrders.Add($"Cosmetic ID: {pid}");
                        }

                    }
                    catch (Exception ex)
                    {
                        failedOrders.Add($"Cosmetic ID: {pid}, Error: {ex.Message}");
                    }
                }


               
                if( successfulOrders == 0) { return RedirectToPage("/Order_Details", new { SelectMsg = "Please Order at least one Item" }); }
                HttpContext.Session.Remove(SessionKey);
                HttpContext.Session.Remove(SessionKeyC);
                HttpContext.Session.Remove("totalPrice");
                return RedirectToPage("/follow_order", new { c_username = username });
                
            }
            else
            {
               // HttpContext.Session.SetString("SourcePage", "View_Items");
               
               return RedirectToPage("/Order_Details", new {SelectMsg="Please Select a Pharmacy"});
            }
        }
        public IActionResult OnPostDeleteMedicine(int id)
        {
            // Load existing Medicines from the session
            var medicineJson = HttpContext.Session.GetString(SessionKey);
            var medicines = !string.IsNullOrEmpty(medicineJson)
                ? JsonSerializer.Deserialize<List<Medicine>>(medicineJson)
                : new List<Medicine>();

            // Remove the medicine with the given ID
            var medicineToRemove = medicines.FirstOrDefault(m => m.Id == id);
            if (medicineToRemove != null)
            {
                medicines.Remove(medicineToRemove);
                var priceString = HttpContext.Session.GetString("totalPrice");
                TotalPrice = !string.IsNullOrEmpty(priceString) ? float.Parse(priceString) : 0;
                TotalPrice -= medicineToRemove.Price * medicineToRemove.Quantity;

                // Save updated list and total price back to the session
                HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(medicines));
                HttpContext.Session.SetString("totalPrice", TotalPrice.ToString("F2"));
            }

            return RedirectToPage("/Order_Details"); // Refresh the page to update the UI
        }

        public IActionResult OnPostDeleteCosmetic(int id)
        {
            // Load existing Cosmetics from the session
            var cosmeticsJson = HttpContext.Session.GetString(SessionKeyC);
            var cosmetics = !string.IsNullOrEmpty(cosmeticsJson)
                ? JsonSerializer.Deserialize<List<Cosmetics>>(cosmeticsJson)
                : new List<Cosmetics>();

            // Remove the cosmetic with the given ID
            var cosmeticToRemove = cosmetics.FirstOrDefault(c => c.Id == id);
            if (cosmeticToRemove != null)
            {
                cosmetics.Remove(cosmeticToRemove);
                var priceString = HttpContext.Session.GetString("totalPrice");
                TotalPrice = !string.IsNullOrEmpty(priceString) ? float.Parse(priceString) : 0;
                // Update the total price
                TotalPrice -= cosmeticToRemove.Price * cosmeticToRemove.Quantity;

                // Save updated list and total price back to the session
                HttpContext.Session.SetString(SessionKeyC, JsonSerializer.Serialize(cosmetics));
                HttpContext.Session.SetString("totalPrice", TotalPrice.ToString("F2"));
            }

            return RedirectToPage("/Order_Details"); // Refresh the page to update the UI
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("/signin");
        }
    }

}
