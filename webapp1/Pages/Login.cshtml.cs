using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp1.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public IActionResult OnPost()
        {
            if (Email == "ab@gml.com" && Password == "aa")
            {
                HttpContext.Session.SetString("Login", "true"); // Set session value


                return RedirectToPage("/AdminDashboard");
            }

            HttpContext.Session.SetString("Login", "false"); // Set session value
            ViewData["Login"] = false; // Pass error state to the view
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            // Remove the session value to log the user out
            HttpContext.Session.Remove("Login");

            // Redirect to the login page or home page
            return RedirectToPage("/Login");
        }
        public void OnGet()
        {
        }
    }
}
