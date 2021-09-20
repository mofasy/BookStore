using AutoMapper;
using BookStore.API.DTOs.Book;
using BookStore.API.DTOs.Category;
using BookStore.Domain.Models;

namespace BookStore.API.Configuration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Category, CategoryAddDTO>().ReverseMap();
            CreateMap<Category, CategoryEditDTO>().ReverseMap();
            CreateMap<Category, CategoryResultDTO>().ReverseMap();
            CreateMap<Book, BookAddDTO>().ReverseMap();
            CreateMap<Book, BookEditDTO>().ReverseMap();
            CreateMap<Book, BookResultDTO>().ReverseMap();
        }
    }
}
