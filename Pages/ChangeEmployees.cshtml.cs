using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Models;



namespace Pharmacy_back.Pages
{
    public class ChangeEmployeesModel : PageModel
    {
        public DB db;

        public ChangeEmployeesModel(DB db)
        {
            this.db = db;
        }

        // Bind properties for employees
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; } // Employee ID passed in query string

        [BindProperty]
        
        public int id { set; get; }
        [BindProperty]
        public string employeename { get; set; }

        [BindProperty]
        public int salaryEmployee { get; set; }

        [BindProperty]
        public int shiftemployee { get; set; }

        // Property to check if the page is in "Edit" mode
        public bool IsEditMode { get; set; }

        public void OnGet()
        {
            if (ID > 0)
            {
                
                IsEditMode = true;
            }
            else
            {
                IsEditMode = false;
            }
        }

        public IActionResult OnPostAddEmployee()
        {
            try
            {
                // Add new employee
                db.AddEmployee(id,employeename, salaryEmployee, shiftemployee); 
                Console.WriteLine("Added Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex}");
            }
            return RedirectToPage("/Allempolyees"); 
        }

        public IActionResult OnPostEditEmployee()
        {
            try
            {
                
                db.UpdateEmployee(ID,employeename,salaryEmployee,shiftemployee); 
                Console.WriteLine("Updated Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex}");
            }
            finally { 
            }
            return RedirectToPage("/Allempolyees");
        }
        public  IActionResult OnPostDelete()
        {
            db.deletEemployee(ID);
            return RedirectToPage("/Allempolyees");
        }
    }
}

