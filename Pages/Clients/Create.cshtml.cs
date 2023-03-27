using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StormWeb.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.name = Request.Form["name"];

            if (clientInfo.name.Length == 0)
            {
                errorMessage = "All the Fields are Required!";
                return;
            }

            //save the new client into the database
            try
            {
                String connectionString = "Data Source=hostingtest1-server.database.windows.net;Initial Catalog=hostingtest1-database;Persist Security Info=True;User ID=hostingtest1-server-admin;Password=Q713TG0620678B66$";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO tblFullname " +
                                 "(Fullname) VALUES " +
                                 "(@name);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("name", clientInfo.name);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clientInfo.name = "";
            successMessage = "New Client Added Successfully!";

            Response.Redirect("/Clients/Index");
        }
    }
}
