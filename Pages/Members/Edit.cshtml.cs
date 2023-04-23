using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ASPTest.Pages.Members
{
    public class EditModel : PageModel
    {
        public MemberInfo memberInfo = new MemberInfo();

        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String ID = Request.Query["ID"];

            try
            {
                String connectionString = ASPTest.Startup.Variables.tester;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM Table_3 WHERE ID=@ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", ID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                memberInfo.ID = "" + reader.GetInt32(0);
                                memberInfo.Name = reader.GetString(1);
                                memberInfo.Age = "" + reader.GetInt32(2);
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

            memberInfo.ID = Request.Form["ID"];
            memberInfo.Name = Request.Form["name"];
            memberInfo.Age = Request.Form["age"];           

            if (memberInfo.Name.Length == 0 || memberInfo.Age.Length == 0)
            {
                errorMessage = "Name and Age are required";
                return;
            }

            try
            {

                String connectionString = ASPTest.Startup.Variables.tester;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "UPDATE Table_3 " + "SET name=@name, age=@age " + "WHERE ID=@ID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", memberInfo.Name);
                        command.Parameters.AddWithValue("@age", memberInfo.Age);
                        command.Parameters.AddWithValue("@ID", memberInfo.ID);
                        command.ExecuteNonQuery();




                    }



                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/projects/aspwebapp/Members/Index");
        }
    }
}
