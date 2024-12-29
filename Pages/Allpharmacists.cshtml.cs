using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Data;
using System.Reflection;
namespace Pharmacy_back.Pages
{
    public class AllpharmacistsModel : PageModel
    {   public DB db {  get; set; }
        [BindProperty]
        public string pusername { get; set; }
        public DataTable table { get; set; }
        public void OnGet()
        {
            table = db.GetPharmacist();
            if (table == null)
            {
                table = new DataTable(); // Initialize to avoid null reference
            }
        }
        public AllpharmacistsModel(DB db)
        {
            this.db = db;
        }
        public IActionResult OnPostEdit(int id) {

           return RedirectToPage("/works_on", new {IdEmp=id ,showpharmacist=true});
        }

        public void OnPostDelete(string p_username)
        {
            try
            {
                if (!string.IsNullOrEmpty(p_username))
                {
                    db.DeletePharmacist(p_username); // Delete the pharmacist
                    Console.WriteLine($"Pharmacist with username '{p_username}' has been deleted.");
                }
                else
                {
                    Console.WriteLine("No username provided for deletion.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deletion: {ex.Message}");
            }
            finally
            {
                
                table = db.GetPharmacist(); 
            }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("/signin");
        }


    }

}
