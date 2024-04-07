using System.ComponentModel.DataAnnotations;

namespace Lab1Sol.Models
{
    public class DateInPastAttribute : ValidationAttribute
    {
        override protected ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null) return new ValidationResult("Date is required");
            if (value is DateTime date)
            {
                if (date.Year < DateTime.Now.Year)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Date must be in the past");
        }
    }
}
