using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.Helpers
{
    public class PasswordValidation: ValidationAttribute
    {
        private bool bol = true;
        private string Punctuation = "!@#$%^&*_";

        public override bool IsValid(object? value)
        {
            string password = Convert.ToString(value);
            return bol && password.Any(char.IsDigit) && password.Length <= 20 && password.Length >= 12 && password.Any(char.IsUpper) && password.IndexOfAny(Punctuation.ToCharArray()) >= 0;
        }
    }
}
