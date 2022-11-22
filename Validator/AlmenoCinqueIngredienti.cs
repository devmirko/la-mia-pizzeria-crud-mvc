using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_razor_layout.Validator
{
    public class AlmenoCinqueIngredienti : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string fieldValue = (string)value;


            if (fieldValue == null || !fieldValue.Trim().Contains(' '))
            {
                return new ValidationResult("Il campo deve contenere almeno cinque ingredienti");
            }
            string[] fieldValueLenght = fieldValue.Split(' ');
            if (fieldValueLenght.Length < 5)
            {
                return new ValidationResult("Il campo deve contenere almeno cinque ingredienti");
            }

            return ValidationResult.Success;
        }
    }
}
