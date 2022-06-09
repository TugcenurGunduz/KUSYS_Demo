using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace KUSYS_Demo.Views.Students
{
    public class EditModel : PageModel
    {
        public StudentsInfo studentsInfo = new StudentsInfo();
        public String errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM students WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                studentsInfo.id = "" + reader.GetInt32(0);
                                studentsInfo.studentid = "" + reader.GetInt32(1);
                                studentsInfo.name = "" + reader.GetString(2);
                                studentsInfo.surname = "" + reader.GetString(3);
                                studentsInfo.birthday = "" + reader.GetString(4);

                            }
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
            }
        }
        public void OnPost()
        {
            studentsInfo.studentid = Request.Form["studentsId"];
            studentsInfo.id = Request.Form["Id"];
            studentsInfo.name = Request.Form["name"];
            studentsInfo.surname = Request.Form["surname"];
            studentsInfo.birthday = Request.Form["birthdate"];

            if(studentsInfo.id.Length == 0|| studentsInfo.name.Length==0||
                studentsInfo.surname.Length==0|| studentsInfo.birthday.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE students" +
                        "SET studentid=@studentid, name=@name, surname=@surname, birthdat=@birthday" +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@studentid", studentsInfo.studentid);
                        command.Parameters.AddWithValue("@id", studentsInfo.id);
                        command.Parameters.AddWithValue("@name", studentsInfo.name);
                        command.Parameters.AddWithValue("@surname", studentsInfo.surname);
                        command.Parameters.AddWithValue("@birthdate", studentsInfo.birthday);

                        command.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Students/Index");
        }
    }
}
