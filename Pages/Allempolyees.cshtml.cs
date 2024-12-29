using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.Data;
using System.Reflection;

namespace Pharmacy_back.Pages
{
    public class AllempolyeesModel : PageModel
    {   public DataTable table {  get; set; }   
        public int ID { get; set; }
        public DB Db { get; set; }
        public AllempolyeesModel(DB db)
        {
        Db = db;
        }  
         public void OnGet()
         {
            table=Db.Getemployees();
         }
        public IActionResult OnPostEdit(int ID)
        {
            return RedirectToPage("/ChangeEmployees", new {ID=ID});

        }
        public void OnPostDelete(int id)
        {
            try
            {
                if (id > 0)
                {
                    Db.deletEemployee(id); 
                    Console.WriteLine($"Employee with ID '{id}' has been deleted.");
                }
                else
                {
                    Console.WriteLine("No valid ID provided for deletion.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deletion: {ex.Message}");
            }
            finally
            {
                
                table = Db.Getemployees(); 
            }
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("/signin");
        }

    }
}
