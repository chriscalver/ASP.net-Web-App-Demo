using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ASPTest.Pages.Members
{
    public class IndexModel : PageModel
    {
        public String errorMessage = "";

        public List<MemberInfo> ListMembers = new List<MemberInfo>();      


        public void OnGet()
        {

            try
            {             
                String connectionString = ASPTest.Startup.Variables.tester;

                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    String sql = "SELECT * From Table_3";

                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MemberInfo memberinfo = new MemberInfo();
                                memberinfo.ID = "" + reader.GetInt32(0);
                                memberinfo.Name = reader.GetString(1);
                                memberinfo.Age = "" + reader.GetInt32(2);

                                ListMembers.Add(memberinfo);

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {                
                errorMessage = ex.Message;       
             
                return;
            
            }
        }
    }


    public class MemberInfo 
    {

        public String ID;
        public String Name;
        public String Age;

    }
}
