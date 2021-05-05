#nullable disable

using System.ComponentModel.DataAnnotations;

namespace SQLBlazor.Shared
{
    public partial class Employee
    {
        public int Id { get; set; }

        [Required]
        public string EmpFirstname { get; set; }
        [Required]
        public string EmpSecondname { get; set; }
        [Required]
        public string EmpEmailAddress { get; set; }
    }
}
