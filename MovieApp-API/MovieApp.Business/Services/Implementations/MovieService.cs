using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions.Common;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Business.Utilities;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper mapper;
        private readonly IGenreService genreService;
        private readonly IWebHostEnvironment env;

        public MovieService(IMovieRepository movieRepository, IMapper mapper, IGenreService genreService, IWebHostEnvironment env)
        {
            _movieRepository = movieRepository;
            this.mapper = mapper;
            this.genreService = genreService;
            this.env = env;
        }

        public async Task<MovieGetDto> CreateAsync(MovieCreateDto dto)
        {

            if (!await genreService.IsExistAsync(x => x.Id == dto.GenreId && x.IsDeleted == false)) throw new EntityNotFoundException();
            Movie movie = mapper.Map<Movie>(dto);
            string imageUrl = dto.ImageFile.SaveFile(env.WebRootPath, "Uploads");
            movie.MovieImages = new List<MovieImage>();
            MovieImage movieImage = new MovieImage()
            {
                ImageUrl = imageUrl,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                IsDeleted = false
            };

            movie.MovieImages.Add(movieImage);
            movie.CreatedAt = DateTime.Now;
            movie.ModifiedAt = DateTime.Now;

            //Movie movie = new Movie()
            //{
            //    ModifiedAt = DateTime.Now,
            //    CreatedAt = DateTime.Now,
            //    IsDeleted = false,
            //    Title = dto.Title,
            //    Desc = dto.Description
            //};

            await _movieRepository.CreateAsync(movie);
            await _movieRepository.CommitAsync();
            MovieGetDto getDto = new MovieGetDto(movie.Id, movie.Title, movie.Desc, movie.IsDeleted, movie.CreatedAt, movie.ModifiedAt, movie.GenreId);

            return getDto;
        }
        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new InvalidIdException();
            var data = await _movieRepository.GetByIdAsync(id);
            if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");
            _movieRepository.Delete(data);
            await _movieRepository.CommitAsync();
        }

        public async Task<ICollection<MovieGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
        {
            var datas = await _movieRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();

            ICollection<MovieGetDto> dtos = datas.Select(data => new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedAt, data.ModifiedAt, data.GenreId)).ToList();

            return dtos;
        }

        public async Task<MovieGetDto> GetById(int id)
        {
            if (id < 1) throw new InvalidIdException();
            var data = await _movieRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");

            //MovieGetDto dto = new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedAt, data.ModifiedAt);

            MovieGetDto dto = mapper.Map<MovieGetDto>(data);

            return dto;
        }

        public async Task<MovieGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Movie, bool>>? expression = null, params string[] includes)
        {
            var data = await _movieRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();
            if (data is null) throw new EntityNotFoundException(404, "EntityNotFound");

            MovieGetDto dto = new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedAt, data.ModifiedAt, data.GenreId);

            return dto;
        }

        public async Task UpdateAsync(int? id, MovieUpdateDto dto)
        {
            if (!await genreService.IsExistAsync(x => x.Id == dto.GenreId && x.IsDeleted == false)) throw new EntityNotFoundException();
            if (id < 1 || id is null) throw new InvalidIdException();

            var data = await _movieRepository.GetByIdAsync((int)id);

            if (data is null) throw new EntityNotFoundException();

            string imageUrl = dto.ImageFile.SaveFile(env.WebRootPath, "Uploads");
            data.MovieImages = new List<MovieImage>();
            MovieImage movieImage = new MovieImage()
            {
                ImageUrl = imageUrl,
                ModifiedAt = DateTime.Now,
                IsDeleted = false
            };

            data.MovieImages.Add(movieImage);

            mapper.Map(dto, data);

            data.ModifiedAt = DateTime.Now;

            await _movieRepository.CommitAsync();
        }
    }
}
