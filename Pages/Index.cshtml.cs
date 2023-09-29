using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace STMWeb.Pages
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        //module info list
        public List<ModuleInfo> modules = new();
        //getting todays date
        public string today = DateTime.Today.DayOfWeek.ToString();
        //error message
        public string errorMessage = "";

        public void OnGet()
        {
            try
            {
                // connecting to the database
                string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using SqlConnection connection = new(connString);

                //opening the connection and creating a sql query script
                connection.Open();
                string sql = "SELECT Code, Name, [Reminder Date]" +
                             "FROM Module";

                using SqlCommand cmd = new(sql, connection);
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ModuleInfo module = new();

                    module.code = reader.GetString(0);
                    module.name = reader.GetString(1);
                    module.studyDay = reader.GetString(2);
                    //adding the object to the list
                    modules.Add(module);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }

    public class ModuleInfo
    {
        public string? code;
        public string? name;
        public string? studyDay;
    }
}