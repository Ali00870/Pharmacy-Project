using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;

namespace Pharmacy_back.Pages
{
    public class works_onModel : PageModel
    {
        public DB db;

        public works_onModel(DB db)
        {
            this.db = db;
        }

        // Bind properties for pharmacists and employees
        [BindProperty(SupportsGet = true)]
        public string pusername { get; set; } // Pharmacist username passed in query string

        [BindProperty]
        public string username { get; set; }

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }

        [BindProperty]
        public int salary { get; set; }

        [BindProperty]
        public int shift { get; set; }

        [BindProperty]
        public string name { get; set; }

        [BindProperty]
        public string employeename { set; get; }

        [BindProperty]
        public int salaryEmployee { set; get; }

        [BindProperty]
        public int shiftemployee { set; get; }

        [BindProperty]
        public int ID { get; set; }

        // Property to check if the page is in "Edit" mode
        public bool IsEditMode { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("username") == "pharmacist10")
            {
                if (!string.IsNullOrEmpty(pusername))
                {
                    IsEditMode = true;
                }
                else
                {
                    IsEditMode = false;
                }
                return Page();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }

        public IActionResult OnPostAddPharmacist()
        {
            try
            {
                db.AddPharmacist(username, name, email, password, shift, salary);
                Console.WriteLine("Added Pharmacist");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding pharmacist: {ex}");
            }
            return RedirectToPage("/Allpharmacists");
        }

        public void OnPostAddEmployee()
        {
            try
            {
                db.AddEmployee(ID, employeename, salaryEmployee, shiftemployee);
                Console.WriteLine("Added Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex}");
            }
        }

        public IActionResult OnPostEditPharmacist()
        {
            try
            {
                db.UpdatePharmacist(username, shift, salary);
                Console.WriteLine("Updated Pharmacist");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating pharmacist: {ex}");
            }
            return RedirectToPage("/Allpharmacists");
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/signin");
        }
    }
}
