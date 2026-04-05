using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vertex.Admin.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnGet()
        {
            var token = HttpContext.Session.GetString("JWT");

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Login");

            return Page();
        }
    }
}
