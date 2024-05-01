using System.ComponentModel.DataAnnotations;
using System;

namespace Cinema.Validators
{
    public class TimeInRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
                if (value != null)
                {
                    DateTime inputDateTime;

                    if (value is DateTime)
                    {
                        inputDateTime = (DateTime)value;
                    }
                    else
                    {
                        if (DateTime.TryParse(value.ToString(), out inputDateTime))
                        {
                        }
                        else
                        {
                            return new ValidationResult("Nieprawidłowy format czasu.");
                        }
                    }

                    TimeSpan inputTime = inputDateTime.TimeOfDay;

                    TimeSpan startTime = new TimeSpan(8, 0, 0); // 12:00
                    TimeSpan endTime = new TimeSpan(23, 0, 0);   // 23:00

                    if (inputTime < startTime || inputTime > endTime)
                    {
                        return new ValidationResult("Godzina musi być w zakresie od 12:00 do 23:00.");
                    }
                }

                return ValidationResult.Success;
        }
    }
}
