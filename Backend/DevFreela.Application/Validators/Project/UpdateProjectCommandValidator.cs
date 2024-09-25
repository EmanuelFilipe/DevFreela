using DevFreela.Application.Commands.Project.UpdateProject;
using FluentValidation;

namespace DevFreela.Application.Validators.Project
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().NotNull()
                .MaximumLength(255)
                .WithMessage("tamanho maximo do description é 255 caracteres");

            RuleFor(p => p.Title)
                .NotEmpty().NotNull()
                .MaximumLength(30) // Must: return True or False
                .WithMessage("tamanho maximo de title é 30 caracteres");
        }
    }
}
