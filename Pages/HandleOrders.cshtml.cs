using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.Data;


namespace Pharmacy_back.Pages
{
    public class HandleOrdersModel : PageModel
    {

        public string pharmacyName {  get; set; }
        private readonly DB db;
        public DataTable dt { get; set; }
        public HandleOrdersModel(DB d)
        {
            this.db = d;
        }
        public IActionResult OnGet()
        {
            //HttpContext.Session.SetString("pharmacy", "City Pharmacy");
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("pharmacy")))
            {

                //HttpContext.Session.SetString("pharmacy", "City Pharmacy");
                pharmacyName = HttpContext.Session.GetString("pharmacy");
                dt = new DataTable();
                dt = db.GetOrders(pharmacyName);
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }


        }

        public IActionResult OnPostDeliver(int id,int quantity)
        {
            DateTime deliverDate=DateTime.Now;
            db.DeliverOrder(id,deliverDate);
            //TempData["Message"] = "Order deleted successfully.";
            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int id,int quantity,int Pid)
        {
            db.DeleteOrder(id,quantity,Pid);
            
            return RedirectToPage();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }



    }
}
