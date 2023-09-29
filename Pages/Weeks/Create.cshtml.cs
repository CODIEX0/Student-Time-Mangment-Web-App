using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using STMWeb.Areas.Identity.Data;
using STMWeb.ViewModels;
using System.Data.SqlClient;

namespace STMWeb.Pages.Weeks
{
    public class CreateModel : PageModel
    {
        //week list to store the week objects
        public STLibrary.Week week = new();

        //store error and success mssages
        public string errorMessage = "";
        public string successMessage = "";

        public WeekViewModel model = new();
        //public string selectedWeek = "Select Week!";
        public void OnGet()
        {
            //retrieving data from the semester and module table
            var semesterData = Helper.Semesters.GetAll();
            var moduleData = Helper.Modules.GetAll();

            model.SemesterSelectList = new();
            model.ModuleSelectList = new();

            //displaying data to the semester and module select lists 
            foreach (var semester in semesterData)
            {
                model.SemesterSelectList.Add(new SelectListItem { Text = "SEMESTER " + semester.SemesterID, Value = semester.SemesterID + "" });
            }
            foreach (var module in moduleData)
            {
                model.ModuleSelectList.Add(new SelectListItem { Text = module.Code.ToUpper() + " - " + module.Name.ToUpper(), Value = module.Code });
            }
        }

        public void OnPost()
        {
            //if any text box is empty, than populate the error message with the error
            if (week.SemesterID == 0 || week.Code.Length == 0 || week.HoursSpentWorking == 0 || week.StartDate.Equals(DateTime.MinValue))
            {
                errorMessage = "All the feilds are required!";
                return;
            }

            //retrieve values from the form and store to the properties
            week.SemesterID = Convert.ToInt32(Request.Form["semesterID"]);
            week.Code = Request.Form["moduleCode"];
            week.HoursSpentWorking = Convert.ToDouble(Request.Form["hours"]);
            week.StartDate = Convert.ToDateTime(Request.Form["startDate"]);

            

            //try to save the data to the database
            try
            {
                //creating connection string
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                //creating an sql connection using the connection string
                using SqlConnection connection = new(connectionString);

                //opening the the sql connection
                connection.Open();

                //creating an sql query 
                string sql = "INSERT INTO Week" +
                             "([Semester Number], Code, [Hours Spent], [Start Date]) VALUES" +
                             "(@[Semester Number], @Code, @[Hours Spent], @[Start Date])";

                //creating an sql command using the sql query and connection
                using SqlCommand cmd = new(sql, connection);

                cmd.Parameters.AddWithValue("@[Semester Number]", week.SemesterID);
                cmd.Parameters.AddWithValue("@Code", week.Code);
                cmd.Parameters.AddWithValue("@[Hours Spent]", Convert.ToDecimal(week.HoursSpentWorking));
                cmd.Parameters.AddWithValue("@[Start Date]", week.StartDate);

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
            week.SemesterID = 0; week.Code = ""; week.HoursSpentWorking = 0; week.StartDate.Equals(DateTime.MinValue);
            successMessage = "New Week Successfully Added!";

            //redirecting the user to the weeks index page
            Response.Redirect("/Weeks/Index");
        }
    }
}
