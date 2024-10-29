using Aplication.DTO;
using FluentValidation;

namespace Aplication.Validators;
public class OrderFilterRequestValidator : AbstractValidator<OrderFilterRequest>
{
    public OrderFilterRequestValidator()
    {
        RuleFor(x => x.StartTime).NotEmpty();
        RuleFor(x => x.EndTime).NotEmpty()
            .GreaterThan(x => x.StartTime)
            .LessThan(DateTime.Now);
        RuleFor(x => x.DistrictName).NotEmpty()
            .MaximumLength(50);
    }
}