using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace STLibrary
{
    public class Login
    {
        //Dotnet-Bot (no date) DbSet class (system.data.entity), DbSet Class (System.Data.Entity) | Microsoft Learn.
        //Available at: https://learn.microsoft.com/en-us/dotnet/api/system.data.entity.dbset-1?view=entity-framework-6.2.0 (Accessed: November 14, 2022)

        [Key]
        public string Id { get; set; }

        [Column(name: "Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter Your Correct Password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


    }
}
