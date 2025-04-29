using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace webapp1.Pages
{
    public class EssayModel : PageModel
    {

        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";
        public Content ContentItem { get; set; }

        public IActionResult OnGet(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM contexttable WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                ContentItem = new Content
                {
                    Id = reader.GetInt32("Id"),
                    Title = reader.GetString("Title"),
                    Category = reader.GetString("Category"),
                    ContentText = reader.GetString("Content"),
                    StudentName = reader.GetString("StudentName")
                };
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM contexttable WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();

            // Redirect after deletion
            return RedirectToPage("Index"); // Redirect to a different page after deletion, for example, the index page
        }

    }
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ContentText { get; set; }
        public string StudentName { get; set; }
    }


}
