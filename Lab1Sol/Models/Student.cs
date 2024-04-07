using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1Sol.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Image { get; set; }
        [Required]
        [Range(18, 60)]
        public int Age { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Address { get; set; }
        [ForeignKey("Department")]
        [Required]
        public int DeptId { get; set; }
        public virtual Department? Department { get; set; }
    }
}
