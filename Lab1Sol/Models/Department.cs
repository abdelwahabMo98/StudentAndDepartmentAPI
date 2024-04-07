using System.ComponentModel.DataAnnotations;

namespace Lab1Sol.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        [MinLength(3)]
        public string MangerName { get; set; }
        [RegularExpression("(EG|USA)")]
        public string Location { get; set; }
        [DateInPast]
        public DateTime OpenDate { get; set; }

        public virtual List<Student>? Students { get; set; }
    }
}
