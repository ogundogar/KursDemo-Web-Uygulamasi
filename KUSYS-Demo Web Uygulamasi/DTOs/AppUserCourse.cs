using Microsoft.Data.SqlClient;

namespace KUSYS_Demo_Web_Uygulamasi.DTOs
{
    public class AppUserCourse
    {
        public string CourseName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public static AppUserCourse FromSqlQuery(SqlDataReader reader)
        {
            return new AppUserCourse
            {
                CourseName = reader["CourseName"].ToString(),
                Name = reader["Name"].ToString(),
                Surname = reader["Surname"].ToString(),
                Email = reader["Email"].ToString()
            };
        }
    }
}