using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STLibrary
{
    public class Module
    {
        [Key]
        public int Id { get; set; }

        [Column(name: "Semester Number")]
        public int SemesterID { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Column(name: "Number Of Credits")]
        [Required]
        public int Credits { get; set; }

        [Column(name: "Class Hours per Week")]
        [Required]
        public double ClassHoursPerWeek { get; set; }

        [Column(name: "Reminder Date")]
        public string ReminderDate { get; set; } = string.Empty;

        [Column(name: "User ID")]
        [Required]
        public string UserID { get; set; } = string.Empty;


    }
}