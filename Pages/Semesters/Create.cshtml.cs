using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STMWeb.Areas.Identity.Data;
using System.Data.SqlClient;

namespace STMWeb.Pages.Semesters
{
    public class CreateModel : PageModel
    {
        //semester list
        public List<Semester> Semesters = new();

        public STLibrary.Semester semester = new();

        //store error and success messages
        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            //if any text box is empty, than populate the error message with the error
            if (semester.NumberOfWeeks == 0 || semester.StartDate.Equals("01/01/0001"))
            {
                errorMessage = "All the fields are required!";
                return;
            }

            semester.NumberOfWeeks = Int32.Parse(Request.Form["weeks"]);
            semester.StartDate = Convert.ToDateTime(Request.Form["startdate"]);

            //if any text box is empty, than populate the error message with the error
            if (semester.NumberOfWeeks == 0 || semester.StartDate.Equals("01/01/0001"))
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
                //creating connection string
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                //creating an sql connection using the connection string
                using SqlConnection connection = new(connectionString);
                //opening the the sql connection
                connection.Open();
                //creating a sql query 
                string sql = "INSERT INTO Semester" +
                             "([Number of Weeks], [Start Date]) VALUES" +
                             "(@weeks, @startdate)";

                //creating a sql command using the sql query and connection
                using SqlCommand cmd = new(sql, connection);

                cmd.Parameters.AddWithValue("@weeks", semester.NumberOfWeeks);
                cmd.Parameters.AddWithValue("@startdate", semester.StartDate);

                //executing the query
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //populate the error message with the exception message
                errorMessage = ex.Message;
                return;
            }

            //emptying the textboxes and populating the success message
            semester.NumberOfWeeks = 0; semester.StartDate = DateTime.MinValue;
            successMessage = "New Semester Successfully Added!";

            //redirecting the user to the Semesters index page
            Response.Redirect("/Semesters/Index");
        }
    }
}
