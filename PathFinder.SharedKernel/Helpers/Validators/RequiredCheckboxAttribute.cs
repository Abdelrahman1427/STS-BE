using System.ComponentModel.DataAnnotations;

namespace STS.SharedKernel.Helpers.Validators
{
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field , AllowMultiple = false)]
    public class RequiredCheckboxAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool checkBoxValue = (bool)value;

            if (checkBoxValue == false)
                return new ValidationResult(ErrorMessageString);

            return ValidationResult.Success;
        }
    }
}
