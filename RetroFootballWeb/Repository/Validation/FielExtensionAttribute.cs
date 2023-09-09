using System.ComponentModel.DataAnnotations;

namespace RetroFootballWeb.Repository.Validation
{
    public class FielExtensionAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "png", "jpeg" };

                bool result = extensions.Any(f => extension.EndsWith(f));

                if (!result)
                {
                    return new ValidationResult("Allowed are jpg, png, jpeg");
                }
            }
            return ValidationResult.Success;
        }
    }
}
