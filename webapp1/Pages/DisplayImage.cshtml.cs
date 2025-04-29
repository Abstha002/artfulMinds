using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace webapp1.Pages
{
    public class DisplayImageModel : PageModel
    {
        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";
        public Content2 ContentItem { get; set; }

        public IActionResult OnGet(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM imgTable WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                ContentItem = new Content2
                {
                    Id = reader.GetInt32("Id"),
                    imgTitle = reader.GetString("imgTitle"),
                    imgLink = reader.GetString("imgLink"),
                    StudentName = reader.GetString("imgCreator")
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

            string query = "DELETE FROM imgTable WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();

            // Redirect after deletion
            return RedirectToPage("Index"); // Redirect to a different page after deletion, for example, the index page
        }
    }

    public class Content2
    {
        public int Id { get; set; }
        public string imgTitle { get; set; }
        public string imgLink { get; set; }
        public string StudentName { get; set; }
    }


}
