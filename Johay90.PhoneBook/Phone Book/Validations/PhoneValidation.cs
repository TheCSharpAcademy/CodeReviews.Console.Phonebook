using FluentValidation;

public class PhoneValidation : AbstractValidator<PhoneNumber>
{
    public PhoneValidation()
    {
        RuleFor(e => e.Number)
            .NotEmpty()
            .MinimumLength(10).WithMessage("Phone number must be at least 10 characters long.")
            .MaximumLength(15).WithMessage("Phone Number must not exceed 15 characters.")
            // Regex pattern matches phone numbers that may contain digits (0-9),
            // plus sign (+) for international numbers, spaces, parentheses, and hyphens.
            .Matches(@"^[0-9+\s()\-]+$").WithMessage("Phone number contains invalid characters.");
    }
}