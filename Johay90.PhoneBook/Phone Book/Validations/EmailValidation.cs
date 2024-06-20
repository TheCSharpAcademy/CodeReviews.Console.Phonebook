using FluentValidation;

public class EmailValidation : AbstractValidator<Email>
{
    public EmailValidation()
    {
        RuleFor(e => e.EmailAddress)
            .NotEmpty()
            .EmailAddress().WithMessage("Must be a valid Emaill Address.");
    }
}
