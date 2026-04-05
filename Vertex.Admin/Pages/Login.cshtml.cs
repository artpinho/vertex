using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Vertex.Admin.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Error { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            using var cliennt = new HttpClient();

            var content = new StringContent(
                JsonSerializer.Serialize(new 
                { 
                    Username, 
                    Password 
                }), 
                Encoding.UTF8, 
                "application/json"
            );

            var response = await cliennt.PostAsync("https://localhost:7280/api/auth/login", content);

            if (!response.IsSuccessStatusCode)
            {
                Error = "Usuário ou senha inválida";
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);
            var token = doc.RootElement.GetProperty("token").GetString();

            //Salvar token na sessão
            HttpContext.Session.SetString("JWT", token);

            return RedirectToPage("/Dashboard/Index");
        }
    }
}
