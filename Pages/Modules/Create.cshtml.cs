using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using STMWeb.ViewModels;
using System.Data.SqlClient;

namespace STMWeb.Pages.Modules
{

    public class CreateModel : PageModel
    {
        //module list to store the module objects
        public List<Helper.Modules> modules = new();

        //module and semester objects
        public STLibrary.Module module = new();

        //store error and success messages
        public string errorMessage = "";
        public string successMessage = "";

        //creating view model object
        public ModuleViewModel model = new();

        public void OnGet()
        {
            //retrieving data from the semester table
            var semesterData = Helper.Semesters.GetAll();

            model.SemesterSelectList = new();

            //displaying data to the select list
            foreach (var semester in semesterData)
            {
                model.SemesterSelectList.Add(new SelectListItem { Text = "SEMESTER " + semester.SemesterID, Value = semester.SemesterID.ToString() });
            }
        }

        public void OnPost()
        {
            //if any text box is empty, than populate the error message with the error
            if (module.SemesterID == 0 || module.Code.Length == 0 || module.Name.Length == 0 ||
                module.Credits <= 0 || module.ClassHoursPerWeek <= 0 || module.ReminderDate.Length == 0)
            {
                errorMessage = "All the feilds are required!";
                return;
            }

            //retrieve values from the form and store to the properties
            module.SemesterID = Convert.ToInt32(Request.Form["semesterID"]);
            module.Code = Request.Form["code"];
            module.Name = Request.Form["name"];
            module.Credits = Convert.ToInt32(Request.Form["credits"]);
            module.ClassHoursPerWeek = Convert.ToInt32(Request.Form["hours"]);
            module.ReminderDate = Request.Form["reminder"];            

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
                string sql = "INSERT INTO Module" +
                             "([Semester Number],Code,Name,[Number Of Credits],[Class Hours Per Week], [Reminder Date]) VALUES" +
                             "(@SemesterID, @Code, @Name, @NumberOfCredits, @ClassHoursPerWeek, @ReminderDate)";

                //creating an sql command using the sql query and connection
                using SqlCommand cmd = new(sql, connection);
                cmd.Parameters.AddWithValue("@SemesterID", module.SemesterID);
                cmd.Parameters.AddWithValue("@Code", module.Code);
                cmd.Parameters.AddWithValue("@Name", module.Name);
                cmd.Parameters.AddWithValue("@NumberOfCredits", module.Credits);
                cmd.Parameters.AddWithValue("@ClassHoursPerWeek", module.ClassHoursPerWeek);
                cmd.Parameters.AddWithValue("@ReminderDate", module.ReminderDate);
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
            module.SemesterID = 0; module.Code = ""; module.Name = ""; module.Credits = 0; module.ClassHoursPerWeek = 0; module.ReminderDate = "";
            successMessage = "New Module Successfully Added!";

            //redirecting the user to the modules index page
            Response.Redirect("/Modules/Index");
        }
    }
}
