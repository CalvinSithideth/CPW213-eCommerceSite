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
        public int MemberID { get; set; }

        /// <summary>
        /// The first and last name of the Member.
        /// ex. J Doe
        /// </summary>
        [Required]
        [StringLength(60)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "That doesn't look like an email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The username of the Member.
        /// ie. The name they use to log-in
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
        /// The date of birth for the member.
        /// Time is ignored.
        /// </summary>
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        // Make custom attribute in order to do a dynamic
        // date range
        //[Range(typeof(DateTime),
        //    DateTime.Today.AddYears(-120).ToShortDateString(),
        //    DateTime.Today.ToShortDateString())]
        // [Required] - It's already required because
        // DateTime is a structure (it's a value type)
        public DateTime DateOfBirth { get; set; }
    }
}
