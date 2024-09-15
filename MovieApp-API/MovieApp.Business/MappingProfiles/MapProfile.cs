using AutoMapper;
using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Business.DTOs.MovieDTOs;
using MovieApp.Core.Entities;

namespace MovieApp.Business.MappingProfiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Movie, MovieGetDto>().ReverseMap();
            CreateMap<MovieCreateDto, Movie>().ReverseMap();
            CreateMap<MovieUpdateDto, Movie>().ReverseMap();


            CreateMap<Genre, GenreGetDto>().ReverseMap();
            CreateMap<Genre, GenreCreateDto>().ReverseMap();
            CreateMap<Genre, GenreUpdateDto>().ReverseMap();
        }
    }
}
