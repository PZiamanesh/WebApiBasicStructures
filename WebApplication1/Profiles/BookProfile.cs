using AutoMapper;
using WebApplication1.DomainModels;
using WebApplication1.ViewModels;

namespace WebApplication1.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookResult>();
            CreateMap<CreateBook, Book>();
            CreateMap<UpdateBook, Book>();
        }
    }
}
