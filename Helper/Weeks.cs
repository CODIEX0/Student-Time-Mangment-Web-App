using STLibrary;
using System.Data.SqlClient;

namespace STMWeb.Helper
{
    public class Weeks
    {
        public static List<Week> listWeeks = new();
        public static List<Week> GetAll()
        {
            listWeeks.Clear();
            //connect to database
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //getting the connection string
            using SqlConnection conn = new(connectionString);

            //opening the connection and creating a sql query script
            conn.Open();
            string sql = "SELECT * FROM Week";

            using SqlCommand command = new(sql, conn);
            using SqlDataReader reader = command.ExecuteReader();

            //reading data from the database
            while (reader.Read())
            {
                Week week = new();
                week.Id = reader.GetInt32(0);
                week.SemesterID = reader.GetInt32(1);
                week.Code = reader.GetString(2);
                week.HoursSpentWorking = reader.GetDouble(5);
                //adding the object to the list
                listWeeks.Add(week);
            }

            return listWeeks;
        }
    }
}
