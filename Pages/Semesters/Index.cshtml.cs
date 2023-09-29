using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STMWeb.Areas.Identity.Data;
using System.Data.SqlClient;

namespace STMWeb.Pages.Semesters
{
    public class IndexModel : PageModel
    {
        //list of semesters 
        public List<Semester> semesters = new();
        //semester object
        public Semester Semester = new();
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
                string sql = "SELECT Id, [Number of Weeks], [Start Date]" +
                             "FROM Semester";

                using SqlCommand cmd = new(sql, connection);
                using SqlDataReader reader = cmd.ExecuteReader();

                //reading data from the database
                while (reader.Read())
                {
                    Semester semester = new();

                    semester.semesterId = "" + reader.GetInt32(0);
                    semester.numberOfWeeks = "" + reader.GetInt32(1);
                    semester.startDate = "" + DateOnly.Parse(reader.GetDateTime(2).ToShortDateString());
                    //adding the object to the list
                    semesters.Add(semester);
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
    }

    public class Semester
    {
        public string? semesterId;
        public string? numberOfWeeks;
        public string? startDate;
    }
}
