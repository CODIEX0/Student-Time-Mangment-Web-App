using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STLibrary
{
    public class Semester
    {
        [Key]
        public int SemesterID { get; set; }

        [Column(name: "Number of Weeks")]
        [Required]
        public int NumberOfWeeks { get; set; }

        [Column(name: "Start Date")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Column(name: "User ID")]
        [Required]
        public string UserID { get; set; } = string.Empty;



        public static double GetHoursOfSelfStudy(int numberOfWeeks, int numberOfCredits, double classHoursPerWeek)
        {
            double hoursOfSelfStudy = 0;
            try
            {
                double credits = numberOfCredits * 10;

                hoursOfSelfStudy = (credits / numberOfWeeks) - classHoursPerWeek;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error :" + ex.Message);

            }

            return hoursOfSelfStudy;
        }
    }
}

