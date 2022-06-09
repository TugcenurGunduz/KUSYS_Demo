using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace KUSYS_Demo.Views.Students
{
    public class IndexModel : PageModel
    {
        public List<StudentsInfo> listStudents = new List<StudentsInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students";
                    using(SqlCommand command =new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StudentsInfo studentsInfo = new StudentsInfo();
                                studentsInfo.id = "" + reader.GetInt32(0);
                                studentsInfo.studentid = reader.GetString(1);
                                studentsInfo.name = reader.GetString(2);
                                studentsInfo.surname = reader.GetString(3);
                                studentsInfo.birthday = reader.GetDateTime(4).ToString();

                                listStudents.Add(studentsInfo);

                            
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class StudentsInfo
    {
        public String id;
        public String studentid;
        public String name;
        public String surname;
        public String birthday;

    }
}
