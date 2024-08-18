using DevFreela.Application.Commands.CreateComment;
using FluentValidation;

namespace DevFreela.Application.Validators.Comment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(p => p.Content)
                .NotEmpty().NotNull()
                .MaximumLength(255)
                .WithMessage("tamanho maximo do description é 255 caracteres");
        }
    }
}
