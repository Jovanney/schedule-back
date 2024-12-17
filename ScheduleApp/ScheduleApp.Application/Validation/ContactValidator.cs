using FluentValidation;
using ScheduleApp.Domain.Entities;

namespace ScheduleApp.Application.Validation;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(contact => contact.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(contact => contact.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(contact => contact.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .When(contact => !string.IsNullOrEmpty(contact.PhoneNumber))
            .WithMessage("Phone number must be in a valid format (E.164 standard).");
    }
}
