using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class allproductsModel : PageModel
    {
        public List<string> CCategories { get; set; } = new List<string> {
            "Hair", "Face", "Perfume", "Shampoo", "Makeup"


            };
        public List<string> MCategories { get; set; } = new List<string>
    {
        "Supplement","Reliever","Vitamin"
    };
        private readonly DB d;

        public allproductsModel(DB d)
        {
            this.d = d;
        }
        // Bind selected categories
        [BindProperty]
        public List<string> SelectedMCategories { get; set; } = new List<string>();
        [BindProperty]
        public List <string> SelectedCCategories { get; set; }=new List<string>();

        [BindProperty (SupportsGet =true)]
        public int OffsetPatameter { get; set; } = 0;
        // Results to display
        public List<string> FilteredResults { get; set; } = new List<string>();
        public DataTable products { get; set; } = new DataTable();
        public DataTable products2 { get; set; }=new DataTable();
        // Simulated method to fetch data as DataTable
        public void OnGet()
        {
            OffsetPatameter = 0;
            // Load all products initially
            products = d.allproducts();
            products2 = d.allproducts2();
            //FilteredResults = products.AsEnumerable().Select(row => row.Field<string>("Name")).ToList();
            HttpContext.Session.SetInt32("Offset", OffsetPatameter);
        }

        public void OnPost()
        {

            //OnPostCosm();
            DataTable eachCategory=new DataTable();
            DataTable eachCategory2 = new DataTable();
            if (SelectedMCategories.Any())
            {
               
                // Filter DataTable rows based on the selected categories
                //FilteredResults = products.AsEnumerable()
                //    .Where(row => SelectedCategories.Contains(row.Field<string>("Category")))
                //    .Select(row => row.Field<string>("Name"))
                //    .ToList();
                for(int i = 0; i < SelectedMCategories.Count; i++)
                {
                    string category = SelectedMCategories[i];
                    eachCategory.Merge(d.ProductsFiltered(category));
                    

                }
                products = eachCategory;
                
            }
            else
            {
                products = d.allproducts();// If no filters are selected, show all products
               //FilteredResults = products.AsEnumerable().Select(row => row.Field<string>("Name")).ToList();
               products2= d.allproducts2();
            }
            if (SelectedCCategories.Any())
            {

                // Filter DataTable rows based on the selected categories
                //FilteredResults = products.AsEnumerable()
                //    .Where(row => SelectedCategories.Contains(row.Field<string>("Category")))
                //    .Select(row => row.Field<string>("Name"))
                //    .ToList();
                for (int i = 0; i < SelectedCCategories.Count; i++)
                {
                    string category = SelectedCCategories[i];
                    eachCategory2.Merge(d.ProductsFiltered2(category));


                }
                products2 = eachCategory2;
            }
            else
            {
                products2 = d.allproducts2();
                // If no filters are selected, show all products
                //FilteredResults = products.AsEnumerable().Select(row => row.Field<string>("Name")).ToList();
            }
        }
        public  void OnPostShowMore(string n)
        {
            int offset = HttpContext.Session.GetInt32("OffsetParameter") ?? 0;
            if (n == "Next")
            {
                
                offset+=6;
                

            }
            else if (n == "Previous")
            {
                if (offset>= 6)
                {
                    offset-=6;
                    

                }
                else
                {

                }
            }
            HttpContext.Session.SetInt32("OffsetParameter", offset);
            products = d.allproducts(offset);
            products2 = d.allproducts2(offset);


        }
        public IActionResult OnPostAddToCart(int id)
        {
            
            return RedirectToPage("/View_Items", new { id =  id });
        }
 public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove("username");
        return RedirectToPage("/signin");
    }
    }
   
}
