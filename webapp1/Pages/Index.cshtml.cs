using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace webapp1.Pages { 

public class IndexModel : PageModel
{
        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";
        public List<Detailor> contents = new List<Detailor>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Contexttable ORDER BY Id DESC LIMIT 1";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                contents.Add(new Detailor
                {
                    Id = reader.GetInt16("Id"),
                    Title = reader.GetString("title"),
                    Category = reader.GetString("category"),
                    Content = Regex.Replace(reader.GetString("Content"), "<.*?>", string.Empty),
                    StudentName = reader.GetString("StudentName"),
                });
            }
            connection.Close();
        }

    }

    public class Detailor
    {
        public int Id;
        public string Title;
        public string Category;
        public string Content;
        public string StudentName;
    }
}
