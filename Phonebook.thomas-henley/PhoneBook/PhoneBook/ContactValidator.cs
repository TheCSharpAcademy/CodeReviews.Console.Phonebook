using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace PhoneBook;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Email address must contain an @ sign")
            .When(c => !c.Email.IsNullOrEmpty());
        
        RuleFor(c => c.Phone)
            .PhoneIsNumeric()
            .PhoneIs10Digits()
            .When(c => !c.Phone.IsNullOrEmpty());
    }
}

public static class CustomValidators
{
    public static IRuleBuilderOptions<Contact, string> PhoneIs10Digits(this IRuleBuilder<Contact, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(phone => phone.Length == 10)
            .WithMessage("Phone number must be 10 digits");
    }

    public static IRuleBuilderOptions<Contact, string> PhoneIsNumeric(this IRuleBuilder<Contact, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(phone => phone.All(char.IsDigit))
            .WithMessage("Phone number must contain only numeric characters");
    }
}