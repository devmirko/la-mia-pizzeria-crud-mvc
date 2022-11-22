using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_razor_layout.Validator
{
    public class PrezzoPizza : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            double fieldValue = (double)value;

            if (fieldValue <= 0 )
            {
                return new ValidationResult("Inserire un prezzo valido");
            }

            return ValidationResult.Success;
        }
    }
}
