using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace webapp1.Pages
{
    public class GalleryUploadModel : PageModel
    {

        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";

        [BindProperty]
        public string imgTitle { get; set; }

        [BindProperty]
        public string imgLink { get; set; }

        [BindProperty]
        public string StudentName { get; set; }

        [BindProperty]
        public string StudentCode { get; set; }

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
                string insertQuery = "INSERT INTO imgTable (imgTitle, imgLink, imgCreator) VALUES (@imgTitle, @imgLink, @imgCreator)";
                using var insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@imgTitle", imgTitle);
                insertCommand.Parameters.AddWithValue("@imgLink", imgLink);
                insertCommand.Parameters.AddWithValue("@imgCreator", StudentName);

                insertCommand.ExecuteNonQuery();

                Message = "Content uploaded successfully!";
                Message1 = "Success";
                connection.Close();

                return RedirectToPage("/Gallery"); // Redirect to a success page (optional)
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

}
