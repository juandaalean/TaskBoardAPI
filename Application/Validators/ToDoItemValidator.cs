using Application.Dtos.Tasks;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ToDoItemValidator : AbstractValidator<ToDoItemDto> 
    {
        public ToDoItemValidator()
        {
            RuleFor(ir => ir.Title)
                .NotNull()
                .MaximumLength(50);

            RuleFor(ir => ir.Description)
                .MaximumLength(200);

            RuleFor(ir => ir.StartDate)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.Now);

            RuleFor(ir => ir.LimitDate)
                .GreaterThan(DateTime.Now);
        }
    }
}
