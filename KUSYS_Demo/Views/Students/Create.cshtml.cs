using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace KUSYS_Demo.Views.Students
{
    public class CreateModel : PageModel
    {
        public StudentsInfo studentsInfo = new StudentsInfo();
        public String errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            studentsInfo.studentid = Request.Form["studentid"];
            studentsInfo.name = Request.Form["name"];
            studentsInfo.surname = Request.Form["surname"];
            studentsInfo.birthday = Request.Form["birthday"];

            if(studentsInfo.name.Length==0||studentsInfo.surname.Length==0||
                studentsInfo.birthday.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            //save the new client info the database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO students" +
                                 "(studentid,name,surname,birthdate) VALUES" +
                                 "(@studentis, @name, @surname, @birthday);";
                
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@studentid", studentsInfo.studentid);
                        command.Parameters.AddWithValue("@name", studentsInfo.name);
                        command.Parameters.AddWithValue("@surname", studentsInfo.surname);
                        command.Parameters.AddWithValue("@birthday", studentsInfo.birthday);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            studentsInfo.name = ""; 
            studentsInfo.surname = ""; 
            studentsInfo.birthday = "";
            successMessage = "New Client Added Correctly";

            Response.Redirect("/Students/Index");
        }
    }
}
