using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Email Address")]
        [EmailAddress]
        [Required]
        public string EmailAdress { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression("(09)[0-9]{9}", ErrorMessage = "This is not a valid phone number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Department Id")]
        public int DeptId { get; set; }

        [DisplayName("Department")]
        public Department? Department { get; set; }
       
        public Employee() { }

        public Employee(int id, string fullName, string emailAdress, DateTime dOB, string phoneNumber, int deptId)
        {
            Id = id;
            FullName = fullName;
            EmailAdress = emailAdress;
            DOB = dOB;
            PhoneNumber = phoneNumber;
            DeptId = deptId;
          
        }
    }
}
