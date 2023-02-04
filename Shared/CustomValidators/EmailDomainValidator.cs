using System.ComponentModel.DataAnnotations;

namespace BlazorEmployeeManagementApp2.Shared.CustomValidators
{
    public class EmailDomainValidator: ValidationAttribute
    {
        public string AllowedDomain { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string[] strings= value.ToString().Split('@');
            if (strings[1].ToUpper() == AllowedDomain.ToUpper()) 
            {
                return null;
            }
            return new ValidationResult($"Domain must be {AllowedDomain}", new[] { validationContext.MemberName });
        }
    }
}
