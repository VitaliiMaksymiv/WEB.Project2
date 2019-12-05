namespace Librairie.MappingProfiles
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Librairie.Models;
    using Librairie.ViewModels;

    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<User, UserVM>()
                .ForMember(dto => dto.Role, prop => prop.Ignore())
                .ForMember(dto => dto.Books, prop => prop.MapFrom(model => model.UserBooks.Select(q => q.Book)))
                .ReverseMap()
                .ForMember(model => model.UserBooks, prop => prop.MapFrom(dto => dto.Books.Select(q => new UserBook() { BookId = q.Id, UserId = dto.Id })));

            CreateMap<Book, BookVM>()
                .ForMember(dto => dto.Authors, prop => prop.MapFrom(model => model.AuthorBooks.Select(q => q.Author)))
                .ForMember(dto => dto.Users, prop => prop.MapFrom(model => model.UserBooks.Select(q => q.User)))
                .ReverseMap()
                .ForMember(model => model.UserBooks, prop => prop.MapFrom(dto => dto.Users.Select(q => new UserBook() { UserId = q.Id, BookId = dto.Id })));

            CreateMap<Author, AuthorVM>()
                .ForMember(dto => dto.Books, prop => prop.MapFrom(model => model.AuthorBooks.Select(q => q.Book)))
                .ReverseMap();

            CreateMap<SignUpVM, User>()
                .ForMember(model => model.Email, prop => prop.MapFrom(model => model.Email))
                .ForMember(model => model.UserName, prop => prop.MapFrom(model => model.Name))
                .ForAllOtherMembers(prop => prop.Ignore());

            CreateMap<IEnumerable<BookVM>, ShopVM>()
                .ForMember(dto => dto.ShopItems, prop => prop.MapFrom(model => model))
                .ForAllOtherMembers(prop => prop.Ignore());
        }
    }
}
