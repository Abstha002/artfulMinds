using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace webapp1.Pages
{
    public class GalleryModel : PageModel
    {
        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";
        public List<imgDetail> images = new List<imgDetail>();
        public void OnGet()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM  imgTable";

            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                images.Add(new imgDetail
                {
                    Id = reader.GetInt16("Id"),
                    imgTitle = reader.GetString("imgTitle"),
                    imgLink = reader.GetString("imgLink"),
                    StudentName = reader.GetString("imgCreator"),
                });
            }
            connection.Close();
        }
    }
    public class imgDetail
    {
        public int Id;
        public string imgTitle;
        public string imgLink;
        public string StudentName;
    }
}
