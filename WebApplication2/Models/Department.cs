using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Department")]
        public string Name { get; set; }
        public List<Employee>? Employees { get; set; }

        public Department()
        {}

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
