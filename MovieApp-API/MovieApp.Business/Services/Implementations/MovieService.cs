using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Business.Exceptions.Common;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public async Task<MovieGetDto> CreateAsync(MovieCreateDto dto)
        {

            Movie movie = mapper.Map<Movie>(dto);
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
            MovieGetDto getDto = new MovieGetDto(movie.Id, movie.Title, movie.Desc, movie.IsDeleted, movie.CreatedAt, movie.ModifiedAt);

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

            ICollection<MovieGetDto> dtos = datas.Select(data => new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedAt, data.ModifiedAt)).ToList();

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

            MovieGetDto dto = new MovieGetDto(data.Id, data.Title, data.Desc, data.IsDeleted, data.CreatedAt, data.ModifiedAt);

            return dto;
        }

        public async Task UpdateAsync(int? id, MovieUpdateDto dto)
        {
            if (id < 1 || id is null) throw new InvalidIdException();

            var data = await _movieRepository.GetByIdAsync((int)id);

            if (data is null) throw new EntityNotFoundException();

            mapper.Map(dto, data);
            //data.Title = dto.Title;
            //data.Desc = dto.Description;
            //data.IsDeleted = dto.IsDeleted;
            data.ModifiedAt = DateTime.Now;

            await _movieRepository.CommitAsync();
        }
    }
}
