using STLibrary;
using System.Data.SqlClient;

namespace STMWeb.Helper
{
    public class Semesters
    {
        public static List<Semester> listSemesters = new();

        public static List<Semester> GetAll()
        {
            listSemesters.Clear();
            //connect to database
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //getting the connection string
            using SqlConnection conn = new(connectionString);

            //opening the connection and creating a sql query script
            conn.Open();
            string sql = "SELECT * FROM Semester";

            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            //reading data from the database
            while (reader.Read())
            {
                Semester semester = new();

                semester.SemesterID = reader.GetInt32(0);
                semester.NumberOfWeeks = reader.GetInt32(1);
                semester.StartDate = reader.GetDateTime(2);
                //adding the object to the list
                listSemesters.Add(semester);
            }

            return listSemesters;
        }
    }
}
