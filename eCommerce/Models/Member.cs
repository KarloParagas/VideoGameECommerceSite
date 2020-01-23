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
        //Make custom attribute in order to do a dynamic date range
        //[Range(typeof(DateTime, DateTime.Today.AddYears(-120).ToShortDateString(), DateTime.Today.ToShortDateString()))]
        public DateTime DateOfBirth { get; set; }
    }
}
