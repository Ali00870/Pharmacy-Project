using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using Pharmacy_back.Model;

using System.Data;
using System.Text.Json;

namespace Pharmacy_back.Pages
{
    //public class Order_DetailsModel : PageModel
    //{
    //    private const string SessionKey = "MedicineList";
    //    private const string SessionKeyC = "CosmeticsList";
    //    [BindProperty(SupportsGet = true)]
    //    public Medicine M { get; set; } = new Medicine();//add a copy constructor
    //    [BindProperty(SupportsGet = true)]
    //    public Cosmetic C { get; set; }//add a copy constructor
    //    public List<Medicine> Medicines {  get; set; }=new List<Medicine>();
    //    public List<Cosmetic> Cosmetics { get; set; }=new List<Cosmetic>() ;
    //    public float totalPrice { get; set; } = 0;

    //    public void OnGet()
    //    {
    //        M.Id = 23;M.Price= 25;M.Name = "Ahmed";
    //        C.Id = 23;C.Price = 30;C.Name = "Hamada";
    //        //HttpContext.Session.SetString("totalPrice", totalPrice.ToString());
    //        HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Medicines));
    //        HttpContext.Session.SetString(SessionKeyC, JsonSerializer.Serialize(Cosmetics));
    //        //HttpContext.Session.SetString("totalPrice",totalPrice.ToString());
    //        var medicineJson = HttpContext.Session.GetString(SessionKey);
    //            var medicines = !string.IsNullOrEmpty(medicineJson)
    //                ? JsonSerializer.Deserialize<List<Medicine>>(medicineJson)
    //                : new List<Medicine>();
    //        // var ObJson = HttpContext.Session.GetString("MedObj");
    //        // M = !string.IsNullOrEmpty(ObJson) ? JsonSerializer.Deserialize<Medicine>(ObJson) : new Medicine();
    //        // Add a new product

    //        medicines!.Add(new Medicine { Id = M.Id, Name = M.Name, Price = M.Price, Manufacturer = M.Manufacturer, Dosage = M.Dosage, Quantity = M.Quantity, Active_Ingredient = M.Active_Ingredient, Type = M.Type }); 
    //            string p=HttpContext.Session.GetString("totalPrice")!;
    //        if (p != null)
    //        { totalPrice = float.Parse(p) + M.Price; }
    //            // Save the updated list back to the session
    //            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(medicines));
    //            HttpContext.Session.SetString("totalPrice", totalPrice.ToString());


    //        var cosmeticsJson = HttpContext.Session.GetString(SessionKeyC);
    //        var cosmetics = !string.IsNullOrEmpty(cosmeticsJson)
    //            ? JsonSerializer.Deserialize<List<Cosmetic>>(cosmeticsJson)
    //            : new List<Cosmetic>();
    //        //totalPrice += C.Price;

    //        // Add a new product
    //        if(cosmetics != null) {
    //            cosmetics.Add(new Cosmetic { Id = C.Id, Name = C.Name, Price = C.Price, Manufacturer = C.Manufacturer, Quantity = C.Quantity, Type = C.Type, Description = C.Description });



    //        }
    //         p = HttpContext.Session.GetString("totalPrice")!;

    //            totalPrice=float.Parse(p)+C.Price;
    //            HttpContext.Session.SetString(SessionKeyC, JsonSerializer.Serialize(cosmetics));
    //            HttpContext.Session.SetString("totalPrice", totalPrice.ToString());



    //        //// Save the updated list back to the session
    //        //HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(cosmetics));




    //    }
    //    public void OnPost()
    //    {



    //    }


    //}
    public class Order_DetailsModel : PageModel
    {
        private const string SessionKey = "MedicineList";
        private const string SessionKeyC = "CosmeticsList";

        [BindProperty(SupportsGet = true)]
        public Medicine M { get; set; } = new Medicine(); // Ensure initialized

        [BindProperty(SupportsGet = true)]
        public Cosmetics C { get; set; } = new Cosmetics(); // Ensure initialized
        [BindProperty]
        public string SelectedItem { get; set; } // Property to hold the selected value

        public List<SelectListItem> Items { get; set; } = new List<SelectListItem>(); // List for dropdown options
        private readonly Model.DB db;
        public Order_DetailsModel(Model.DB db)
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
            DataTable d = db.pharmacies();
            for(int i = 0; i < d.Rows.Count; i++)
            {
                SelectListItem li = new SelectListItem() { Value = d.Rows[i]["pharmacyname"].ToString(), Text = d.Rows[i]["pharmacyname"].ToString() };
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

            // Initialize M and C with some default values for testing (Optional)
            //M.Id = 23;
            //M.Price = 25;
            //M.Name = "Ahmed";
            //M.Quantity = 3;

            //C.Id = 10;
            //C.Price = 30;
            //C.Name = "Hamada";
            //C.Quantity = 2;
            if (type == 0)
            {
                var MedObj = !string.IsNullOrEmpty(jsonstring) ?
                            JsonSerializer.Deserialize<Medicine>(jsonstring) : new Medicine();
                M = MedObj;
            }
            else if(type == 1) 
            {
                var CosmObj = !string.IsNullOrEmpty(jsonstring) ?
                            JsonSerializer.Deserialize<Cosmetics>(jsonstring) : new Cosmetics();
                C = CosmObj;
            }
            



            // Add new Medicine
            if (type==0&&M != null && !string.IsNullOrEmpty(M.Name))
            {
                //Medicines.Add(new Medicine
                //{
                //    Id = M.Id,
                //    Name = M.Name,
                //    Price = M.Price,
                //    Manufacturer = M.Manufacturer,
                //    Dosage = M.Dosage,
                //    Quantity = M.Quantity,
                //    Active_Ingredient = M.Active_Ingredient,
                //    Type = M.Type
                //});
                M.Quantity = order_quantity;
                Medicines.Add(M);
                TotalPrice += M.Price*M.Quantity;
            }

            // Add new Cosmetic
            if (type==1&&C != null && !string.IsNullOrEmpty(C.Name))
            {
                //Cosmetics.Add(new Cosmetics
                //{
                //    Id = C.Id,
                //    Name = C.Name,
                //    Price = C.Price,
                //    Manufacturer = C.Manufacturer,
                //    Quantity = C.Quantity,
                //    Type = C.Type,
                //    Description = C.Description
                //});
                C.Quantity = order_quantity;
                Cosmetics.Add(C);
                TotalPrice += C.Price*C.Quantity;
            }

            // Save updated lists and total price back to the session
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Medicines));
            HttpContext.Session.SetString(SessionKeyC, JsonSerializer.Serialize(Cosmetics));
            HttpContext.Session.SetString("totalPrice", TotalPrice.ToString());
            type = -1;
        }
        

        //public void OnPost()
        //{

        //    foreach(var M in Medicines) {
        //        int pid = M.Id;
        //        int quantity=M.Quantity;
        //        int done=db.InsertOrder(username, pid, quantity,SelectedItem);
        //        if (done == 1) {
        //            orderMessage+= "1";

        //        }
        //        else
        //        {

        //            //FailedList
        //        }

        //    }
        //    foreach(var M in Cosmetics)
        //    {
        //        int pid = M.Id;
        //        int quantity=M.Quantity;
        //        int done = db.InsertOrder(username, pid, quantity, SelectedItem);

        //        if (done == 1)
        //        {
        //            orderMessage+= "1";

        //        }
        //        else
        //        {

        //            //FailedList
        //        }
        //    }

        //}
        public IActionResult OnPostAnotherItem()
        {
            return RedirectToPage("/allproducts");
        }
        public void OnPost()
        {
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
            // Process Medicines
            foreach (var M in Medicines)
            {
                
                int pid = M.Id;
                int quantity = M.Quantity;

                try
                {
                    string msg = "f";
                    int done = db.InsertOrder(username, pid, quantity, SelectedItem,ref msg);
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
            foreach ( var M in Cosmetics)
            {
                int pid = M.Id;
                int quantity = M.Quantity;

                try
                {
                    string msg = "f";
                    int done = db.InsertOrder(username, pid, quantity, SelectedItem,ref msg);
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

            // Provide feedback
            orderMessage = $"Orders successful: {successfulOrders}. Our delivery man will call you soon!";

            
                        
            if (failedOrders.Any())
            {
                orderMessage += $" Failed orders: {string.Join(", ", failedOrders)}.";
                
            }

        }

    }

}
