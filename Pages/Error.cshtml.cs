using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pharmacy_back.Model;
using System.Diagnostics;

namespace Pharmacy_back.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;
        public DB db { set; get; } 
        public int count {  get; set; }
        public ErrorModel(ILogger<ErrorModel> logger,DB dB)
        {
            _logger = logger;
            this.db = dB;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
           
        } 
        public IActionResult OnPostLogout()
    {
        HttpContext.Session.Remove("username");
        return RedirectToPage("/signin");
    }
    }
   

}
