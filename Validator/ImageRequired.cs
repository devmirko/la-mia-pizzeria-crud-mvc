using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_razor_layout.Validator
{
    public class ImageRequired : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;

            bool containsImage = fieldValue.Contains("https://");

            if (!containsImage)
            {
                return new ValidationResult("l'immagine deve contenere un path valido");
            }
           

            return ValidationResult.Success;
        }
    }
}
