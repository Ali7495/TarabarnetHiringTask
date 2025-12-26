using FluentValidation;

namespace PhoneBook.Application;

public sealed class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Tags).NotNull()
        .Must(t => t.Length > 0).WithMessage("The tags list cannot be empty.");

        RuleForEach(x => x.Tags)
            .NotNull().MaximumLength(50);
    }
}
