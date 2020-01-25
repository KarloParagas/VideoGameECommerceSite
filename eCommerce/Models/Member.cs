using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents an individual website user
    /// </summary>
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        /// <summary>
        /// The first and last name of the Member. Ex. J Doe
        /// </summary>
        [StringLength(60)]
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "That doesn't look like an email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The username of the Member
        /// </summary>
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[\d\w]+$", ErrorMessage = "Usernames can only contain A-Z, 0-9, and underscores")]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The date of birth for the member. Time is ignored.
        /// </summary>
        //[Required] - It's already required because DateTime is a structure (it's a value type)
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)] //This ignores the time
        [DateOfBirth]
        public DateTime DateOfBirth { get; set; }
    }

    /// <summary>
    /// ViewModel for the login page
    /// </summary>
    public class LoginViewModel 
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class DateOfBirthAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get the value of DateOfBirth for the model
            DateTime dob = Convert.ToDateTime(value);

            DateTime oldestAge = DateTime.Today.AddYears(-120);
            if (dob > DateTime.Today || dob < oldestAge) //If the date of birth they supplied is in the future, or too far in the past
            {
                string errMsg = "You cannot be born in the future or more than 120 years ago";
                return new ValidationResult(errMsg);
            }
            return ValidationResult.Success;
        }
    }
}
