using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
     public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Column(TypeName = "varchar")]
        [DisplayName("First Name")] // Hide the name of the Field
        [Required(ErrorMessage = "This Field is Required")] // Field is made Required 
        [StringLength(30)]   // Maximum characters = 30
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "This field should Start with an Uppercase and contain only letters")]
        public string FirstName { get; set; }
        [Column(TypeName = "varchar")]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This Field is Required")]
        [StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "This field should Start with an Uppercase and contain only letters")]
        public string LastName { get; set; }
        [Column(TypeName = "DateTime")]
        [DisplayName("Date of Enrollment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "This Field is Required")]
        public DateTime EnrollmentDate { get; set; }
        [DisplayName("Student Name")]
        public string FullName { get => FirstName + " " + LastName; }
        //Nav Property
        public ICollection<Enrollment> Enrollments { get; set; }
    }

}