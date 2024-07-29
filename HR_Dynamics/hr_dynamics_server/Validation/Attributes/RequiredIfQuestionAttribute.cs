using System.ComponentModel.DataAnnotations;

namespace hr_dynamics_server.Validation.Attributes
{
    public class RequiredIfQuestionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}
