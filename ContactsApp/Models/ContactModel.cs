
using System.ComponentModel.DataAnnotations;


namespace ContactsApp.Models
{
    /// <summary>
    /// Contact Model
    /// Describes Contact
    /// </summary>
    public class ContactModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; } 

        [Required]
        [StringLength(50)]
        public string Company { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Last Date Contacted")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateContacted { get; set; }

        [StringLength(250)]
        public string Comments { get; set; } 


   
    }



}
