using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STLibrary
{
    public class Week
    {
        [Key]
        public int Id { get; set; }

        [Column(name: "Semester Number")]
        public int SemesterID { get; set; }

        [Column(name: "Module Code")]
        public string Code { get; set; } = null!;

        [Column(name: "Hours Spent")]
        [Required]
        public double HoursSpentWorking { get; set; }

        [Column(name: "Start Date")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Column(name: "User ID")]
        [Required]
        public string UserID { get; set; } = string.Empty;
    }
}
