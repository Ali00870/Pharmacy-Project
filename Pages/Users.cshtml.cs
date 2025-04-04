using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;
using System.Data;

namespace Pharmacy_back.Pages
{
    public class UsersModel : PageModel
    {
        private readonly ILogger<UsersModel> _logger;

        public DB db { get; set; }
        public DataTable userdata { get; set; }
        public UsersModel(ILogger<UsersModel> logger, DB db)
        {
            _logger = logger;
            this.db = db;
        }
        public void OnGet()
        {
            userdata= db.Getuserdata();
            if (userdata == null)
            {
                userdata = new DataTable(); // Initialize to avoid null reference
            }
        }
        public void OnPost(string c_username)
        {
            try
            {
                if (!string.IsNullOrEmpty(c_username))
                {
                    db.DeleteCustomer(c_username);
                    Console.WriteLine($"Customer with username '{c_username}' has been deleted.");
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

                userdata = db.Getuserdata();
            }
        }
    }
}
