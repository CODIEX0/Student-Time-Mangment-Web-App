using Microsoft.AspNetCore.Mvc.RazorPages;
using STLibrary;
using System.Data.SqlClient;
using System.Globalization;

namespace STMWebApp.Pages.Modules
{
    public class Index : PageModel
    {
        //list of module        
        public List<Module> modules = new();
        //error message
        public string errorMessage = "";

        public void OnGet()
        {
            try
            {
                //connecting to the database
                string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using SqlConnection connection = new(connString);

                //opening the connection and creating a sql query script
                connection.Open();
                string sql = "SELECT m.*, s.[Number of Weeks]" +
                             "FROM Module m, Semester s" +
                             "WHERE m.[Semester Number] = s.Id";

                using SqlCommand cmd = new(sql, connection);
                using SqlDataReader reader = cmd.ExecuteReader();

                //reading data from the database
                while (reader.Read())
                {
                    Module module = new();

                    module.id = "" + reader.GetInt32(0);
                    module.semesterNum = "" + reader.GetInt32(1);
                    module.code = reader.GetString(2).ToUpper();
                    module.name = reader.GetString(3).ToUpper();
                    module.credits = "" + reader.GetInt32(4);
                    module.hours = "" + reader.GetDecimal(5);
                    module.reminder = reader.GetString(6);
                    module.selfStudyHours = Semester.GetHoursOfSelfStudy(reader.GetInt32(7), reader.GetInt32(4), Convert.ToDouble(reader.GetDecimal(5))).ToString("##.##", CultureInfo.InvariantCulture) + "Hrs  Per Week";
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

    public class Module
    {
        public string? id;
        public string? semesterNum;
        public string? code;
        public string? name;
        public string? credits;
        public string? hours;
        public string? reminder;
        public string? numWeeks;
        public string? selfStudyHours;
    }

}
