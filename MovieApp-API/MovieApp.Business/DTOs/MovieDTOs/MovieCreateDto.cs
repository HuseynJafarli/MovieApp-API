using FluentValidation;

namespace MovieApp.Business.DTOs.MovieDTOs
{
    public record MovieCreateDto(string Title, string Description, bool IsDeleted);

    public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
    {
        public MovieCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Not empty")
                .NotNull().WithMessage("Not null")
                .MinimumLength(2).WithMessage("Min length must be 1")
                .MaximumLength(200).WithMessage("Length must be less than 200");

            RuleFor(x => x.Description)
                .NotNull().When(x => !x.IsDeleted).WithMessage("If movie is active description cannot be null")
                .MaximumLength(800).WithMessage("Length must be less than 800");

            RuleFor(x => x.IsDeleted).NotNull();
        }
    }
}