using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace StormWeb.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=hostingtest1-server.database.windows.net;Initial Catalog=hostingtest1-database;Persist Security Info=True;User ID=hostingtest1-server-admin;Password=Q713TG0620678B66$";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM tblFullname WHERE ID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
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
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0)
            {
                errorMessage = "All Fields are Required!";
                return;
            }

            try
            {
                String connectionString = "Data Source=hostingtest1-server.database.windows.net;Initial Catalog=hostingtest1-database;Persist Security Info=True;User ID=hostingtest1-server-admin;Password=Q713TG0620678B66$";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE tblFullname " +
                                 "SET Fullname=@name " +
                                 "WHERE ID=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage =ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
