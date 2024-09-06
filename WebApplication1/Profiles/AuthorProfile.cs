using AutoMapper;
using WebApplication1.DomainModels;
using WebApplication1.ViewModels;

namespace WebApplication1.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorResult>();
            CreateMap<CreateAuthor, Author>();
        }
    }
}
