using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace webapp1.Pages
{
    public class ExploreModel : PageModel
    {
        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";
        public List<Detail> contents = new List<Detail>();
        public void OnGet()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM  Contexttable";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                contents.Add(new Detail
                {
                    Id = reader.GetInt16("Id"),
                    Title = reader.GetString("title"),
                    Category = reader.GetString("category"),
                    Content = reader.GetString("Content"),
                    StudentName = reader.GetString("StudentName"),
                });
            }
            connection.Close();
        }
    }
    public class Detail
    {
        public int Id;
        public string Title;
        public string Category;
        public string Content;
        public string StudentName;
    }
}
