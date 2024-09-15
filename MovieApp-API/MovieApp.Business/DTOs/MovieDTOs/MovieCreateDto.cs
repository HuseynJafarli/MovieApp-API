using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MovieApp.Business.DTOs.MovieDTOs
{
    public record MovieCreateDto(string Title, string Desc, bool IsDeleted, int GenreId, IFormFile ImageFile);

    public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
    {
        public MovieCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Not empty")
                .NotNull().WithMessage("Not null")
                .MinimumLength(2).WithMessage("Min length must be 1")
                .MaximumLength(200).WithMessage("Length must be less than 200");

            RuleFor(x => x.Desc)
                .NotNull().When(x => !x.IsDeleted).WithMessage("If movie is active description cannot be null")
                .MaximumLength(800).WithMessage("Length must be less than 800");

            RuleFor(x => x.IsDeleted).NotNull();

            RuleFor(x => x.GenreId).NotNull().NotEmpty();

            RuleFor(x => x.ImageFile)
           .Must(x => x.ContentType == "image/png" || x.ContentType == "image/jpeg")
           .WithMessage("Image's content type must png/jpeg")
           .Must(x => x.Length < 2 * 1024 * 1024);
        }
    }
}