using FluentValidation;

public class NameValidation : AbstractValidator<Contact>
{
    public NameValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("The name property should not be empty.");
    }
}
