using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
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

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
        public List<Cosmetics> Cosmetics { get; set; } = new List<Cosmetics>();
        public float TotalPrice { get; set; } = 0;

        public void OnGet()
        {
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
            M.Id = 23;
            M.Price = 25;
            M.Name = "Ahmed";

            C.Id = 23;
            C.Price = 30;
            C.Name = "Hamada";

            // Add new Medicine
            if (M != null && !string.IsNullOrEmpty(M.Name))
            {
                Medicines.Add(new Medicine
                {
                    Id = M.Id,
                    Name = M.Name,
                    Price = M.Price,
                    Manufacturer = M.Manufacturer,
                    Dosage = M.Dosage,
                    Quantity = M.Quantity,
                    Active_Ingredient = M.Active_Ingredient,
                    Type = M.Type
                });
                TotalPrice += M.Price;
            }

            // Add new Cosmetic
            if (C != null && !string.IsNullOrEmpty(C.Name))
            {
                Cosmetics.Add(new Cosmetics
                {
                    Id = C.Id,
                    Name = C.Name,
                    Price = C.Price,
                    Manufacturer = C.Manufacturer,
                    Quantity = C.Quantity,
                    Type = C.Type,
                    Description = C.Description
                });
                TotalPrice += C.Price;
            }

            // Save updated lists and total price back to the session
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(Medicines));
            HttpContext.Session.SetString(SessionKeyC, JsonSerializer.Serialize(Cosmetics));
            HttpContext.Session.SetString("totalPrice", TotalPrice.ToString());
        }

        public void OnPost()
        {
            // Handle POST requests if needed
        }
    }

}
