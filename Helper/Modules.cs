using STLibrary;
using System.Data.SqlClient;

namespace STMWeb.Helper
{
    public class Modules
    {
        public static List<Module> listModules = new();
        public static List<Module> GetAll()
        {
            listModules.Clear();
            //connecting to the database
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=STMWebDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //getting the connection string
            using SqlConnection conn = new(connectionString);

            //opening the connection and creating a sql query script
            conn.Open();
            string sql = "SELECT * FROM Module";

            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            //reading data from the database
            while (reader.Read())
            {
                Module module = new();
                module.Id = reader.GetInt32(0);
                module.SemesterID = reader.GetInt32(1);
                module.Code = reader.GetString(2);
                module.Name = reader.GetString(3);
                module.Credits = reader.GetInt32(4);
                module.ClassHoursPerWeek = Convert.ToDouble(reader.GetDecimal(5));
                module.ReminderDate = reader.GetString(6);
                //adding the object to the list
                listModules.Add(module);
            }
            return listModules;
        }
    }
}
