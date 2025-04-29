using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace webapp1.Pages
{
    public class UploadModel : PageModel
    {
        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Category { get; set; }

        [BindProperty]
        public new string Content { get; set; }

        [BindProperty]
        public string StudentName { get; set; }

        [BindProperty]
        public string StudentCode { get; set; }

        public List<Checker> Check = new List<Checker>();
        public string Message { get; set; }
        public string Message1 { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                // Validate Student Name and Code
                string validateQuery = "SELECT COUNT(*) FROM student WHERE name = @Name AND studentCode = @Code";
                using var validateCommand = new MySqlCommand(validateQuery, connection);
                validateCommand.Parameters.AddWithValue("@Name", StudentName);
                validateCommand.Parameters.AddWithValue("@Code", StudentCode);

                int count = Convert.ToInt32(validateCommand.ExecuteScalar());
                if (count == 0)
                {
                    Message = "Invalid Student Name or Code.";
                    Message1 = "error";
                    return Page();
                }

                // Insert Content into contexttable
                string insertQuery = "INSERT INTO contexttable (title, category, content, studentname) VALUES (@Title, @Category, @Content, @StudentName)";
                using var insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Title", Title);
                insertCommand.Parameters.AddWithValue("@Category", Category);
                insertCommand.Parameters.AddWithValue("@Content", Content);
                insertCommand.Parameters.AddWithValue("@StudentName", StudentName);

                insertCommand.ExecuteNonQuery();

                Message = "Content uploaded successfully!";
                Message1 = "Success";
                connection.Close();

                return RedirectToPage("/Explore"); // Redirect to a success page (optional)
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine(ex.Message);
                Message = "An error occurred while processing your request.";
                return Page();
            }
        }
    }

    public class Checker
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
