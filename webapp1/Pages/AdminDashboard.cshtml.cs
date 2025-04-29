using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace webapp1.Pages
{
    public class AdminDashboardModel : PageModel
    {
        private readonly string _connectionString = "Server=localhost;Database=artfulminds;User=root;Password=;";

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int Class { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string StudentCode { get; set; }
        [BindProperty]
        public int StudentId { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();

        public bool IsEditing { get; set; } = false;

        public void OnGet(string studentId)
        {
            if (!string.IsNullOrEmpty(studentId))
            {
                // Fetch the student details for editing
                IsEditing = true;
                StudentId = Convert.ToInt32(studentId);

                using var connection = new MySqlConnection(_connectionString);
                connection.Open();

                string query = "SELECT Id, name, class, phone, studentCode FROM student WHERE Id = @Id";
                using var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", StudentId);
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    StudentId = reader.GetInt32("Id");
                    Name = reader.GetString("name");
                    Class = reader.GetInt32("class");
                    Phone = reader.GetString("phone");
                    StudentCode = reader.GetString("studentCode");
                }

                connection.Close();
            }
            else
            {
                // Load all students when not editing
                LoadStudents();
            }
        }

        public IActionResult OnPost()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query;

            if (StudentId!=0)
            {
                // Update the existing student
                query = "UPDATE Student SET name = @Name, class = @Class, phone = @Phone, studentCode = @StudentCode WHERE Id=@Id";
            }
            else
            {
                // Add a new student
                query = "INSERT INTO Student (name, class, phone, studentCode) VALUES (@Name, @Class, @Phone, @StudentCode)";
            }

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", StudentId);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Class", Class);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@StudentCode", StudentCode);

            command.ExecuteNonQuery();
            connection.Close();

            // Redirect after the operation
            return RedirectToPage("/AdminDashboard");
        }
        public IActionResult OnPostDelete(int studentId)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM Student WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", studentId);

            command.ExecuteNonQuery();
            connection.Close();

            // Redirect after deletion
            return RedirectToPage("/AdminDashboard");
        }


        private void LoadStudents()
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT Id, name, class, phone, studentCode FROM student";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Students.Add(new Student
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("name"),
                    Class = reader.GetInt32("class"),
                    Phone = reader.GetString("phone"),
                    StudentCode = reader.GetString("studentCode")
                });
            }

            connection.Close();
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Class { get; set; }
            public string Phone { get; set; }
            public string StudentCode { get; set; }
        }
    }
}
