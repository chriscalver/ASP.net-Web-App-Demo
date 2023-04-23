using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ASPTest.Pages.Members
{
    public class CreateModel : PageModel
    {

        public MemberInfo memberInfo = new MemberInfo();
        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {

        }
        public void OnPost()
        {
            memberInfo.Age = Request.Form["age"];
            memberInfo.Name = Request.Form["name"];

            if (memberInfo.Age.Length == 0 || memberInfo.Name.Length == 0)
            {
                errorMessage = "Please fill in all fields";
                return;
            }

            try
            {

                String connectionString = ASPTest.Startup.Variables.tester;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Table_3" + "(name, age) VALUES " + "(@name, @age);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", memberInfo.Name);
                        command.Parameters.AddWithValue("@age", memberInfo.Age);
                        command.ExecuteNonQuery();

                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }



            memberInfo.Name = "";
            memberInfo.Age = "";
            successMessage = "New Member Successfully Added";

            Response.Redirect("/projects/aspwebapp/Members/Index");



        }
    }
}
