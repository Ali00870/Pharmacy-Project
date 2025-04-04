using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Pharmacy_back.Models;
using System.Data;

namespace Pharmacy_back.Pages
{

    public class follow_orderModel : PageModel
    {
        public DB db { get; set; }
        public follow_orderModel(DB db)
        {
            this.db = db;
        }

        [BindProperty] public string pharmacyname { get; set; }
        [BindProperty] public string pharmacylocation { get; set; }
        [BindProperty(SupportsGet =true)] public string c_username { get; set; }
        [BindProperty] public string orderstatus { get; set; }

        public float totalprice { get; set; }



        [BindProperty] public string? orderdate { get; set; }
        public string? deliverydate {  get; set; }
        public string? pharmacyphone {  get; set; }
        [BindProperty] public string? deliverynumber { get; set; }

        public DataTable dp { get; set; }
        public DataTable? dt { get; set; }
        public void OnGet()
        {
            dt=new DataTable();
            c_username = HttpContext.Session.GetString("username");
            if (!c_username.IsNullOrEmpty())
            {
                dt = db.showdetailssorder(c_username);

                //pharmacyname = dt.Rows[0]["pharmacy_name"].ToString();
                //dp = db.pharmPhones(pharmacyname);

                //pharmacylocation = dt.Rows[0]["pharmacy_location"].ToString();
                //orderstatus = dt.Rows[0]["status"].ToString();

                //var pricestring = dt.Rows[0]["totalPrice"].ToString();
                //totalprice = !string.IsNullOrEmpty(pricestring) ? float.Parse(pricestring) : 0;
                //orderdate = dt.Rows[0]["order_date"].ToString();
                //deliverydate = dt.Rows[0]["delivery_date"].ToString();
                
            }
            else {  }
            
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }
    }

}