using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.Exceptions.Common;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(GenreCreateDto dto)
        {
            Genre data = _mapper.Map<Genre>(dto);

            data.CreatedAt = DateTime.Now;
            data.ModifiedAt = DateTime.Now;

            await _genreRepository.CreateAsync(data);
            await _genreRepository.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 1) throw new InvalidIdException();

            var data = await _genreRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException();

            _genreRepository.Delete(data);
            await _genreRepository.CommitAsync();
        }

        public async Task<ICollection<GenreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
        {
            var datas = await _genreRepository.GetByExpression(asNoTracking, expression, includes).ToListAsync();

            return _mapper.Map<ICollection<GenreGetDto>>(datas);
        }

        public async Task<GenreGetDto> GetById(int id)
        {
            if (id < 1) throw new InvalidIdException();

            var data = await _genreRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException();

            return _mapper.Map<GenreGetDto>(data);
        }

        public async Task<GenreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes)
        {
            var data = await _genreRepository.GetByExpression(asNoTracking, expression, includes).FirstOrDefaultAsync();

            return _mapper.Map<GenreGetDto>(data);
        }


        public async Task UpdateAsync(int id, GenreUpdateDto dto)
        {
            if (id < 1) throw new InvalidIdException();

            var data = await _genreRepository.GetByIdAsync(id);

            if (data is null) throw new EntityNotFoundException();

            _mapper.Map(dto, data);

            data.ModifiedAt = DateTime.Now;

            await _genreRepository.CommitAsync();
        }
    }
}
