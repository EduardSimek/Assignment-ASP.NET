using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Please enter an ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a surname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        public string Email { get; set; }
        

        [Range(18,99,ErrorMessage = "Age must be between 18 and 99")]
        public int Age { get; set; }

       /* [Required(ErrorMessage = "Please enter a salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Please enter a working department")]
        public string Department { get; set; }
       */
    }
}
