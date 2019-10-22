using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Author, AuthorsModel>();
            CreateMap<AuthorsModel, Author>();
            CreateMap<PrintingEdition, PrintingEditionModel>();
            CreateMap<PrintingEditionModel, PrintingEdition>();
            CreateMap<UpdatePrintingEditionModel, PrintingEdition>();
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
