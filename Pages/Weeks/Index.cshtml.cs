using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STLibrary;
using STMWeb.Areas.Identity.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace STMWeb.Pages.Weeks
{
    public class IndexModel : PageModel
    {

        //list of week datails
        public List<WeekDetails> weeks = new();
        //error meassage
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
                string sql = "SELECT w.Id, w.[Semester Number], m.Code, m.[Number Of Credits], m.[Class Hours Per Week], w.[Hours Spent], s.[Number of Weeks], w.[Start Date]" +
                             "FROM Week w JOIN Semester s ON s.Id = w.[Semester Number]" +
                             "JOIN Module m  ON m.[Semester Number] = w.[Semester Number]" +
                             "AND m.Code = w.Code";

                using SqlCommand cmd = new(sql, connection);
                using SqlDataReader reader = cmd.ExecuteReader();

                //reading data from the database
                while (reader.Read())
                {
                    WeekDetails week = new();

                    week.Id = "" + reader.GetInt32(0);
                    week.semesterNum = "" + reader.GetInt32(1);
                    week.code = reader.GetString(2);
                    week.hoursSpent = "" + reader.GetDecimal(5);
                    week.startDate = reader.GetDateTime(7).ToShortDateString();
                    week.selfStudyHours = Semester.GetHoursOfSelfStudy(reader.GetInt32(6), reader.GetInt32(3), Convert.ToDouble(reader.GetDecimal(4))).ToString("##.##", CultureInfo.InvariantCulture) + "Hrs  Per Week";
                    week.remainingSelfStudyHours = (Semester.GetHoursOfSelfStudy(reader.GetInt32(6), reader.GetInt32(3), Convert.ToDouble(reader.GetDecimal(4))) - Convert.ToDouble(reader.GetDecimal(5))).ToString("##.##", CultureInfo.InvariantCulture) + "Hrs  Per Week";
                    //adding the object to the list
                    weeks.Add(week);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }

    public class WeekDetails
    {
        public string? Id;
        public string? semesterNum;
        public string? code;
        public string? hoursSpent;
        public string? selfStudyHours;
        public string? remainingSelfStudyHours;
        public string? startDate;
    }
}
