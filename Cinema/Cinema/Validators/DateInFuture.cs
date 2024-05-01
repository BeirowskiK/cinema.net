using System.ComponentModel.DataAnnotations;
using System;

namespace Cinema.Validators
{
    public class DateInFuture: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime inputDate = (DateTime)value;

                if (inputDate < DateTime.Now.Date)
                {
                    return new ValidationResult("Data musi być większa niż obecny dzień.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
   