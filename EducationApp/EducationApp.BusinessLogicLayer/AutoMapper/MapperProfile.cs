using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Account;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.User;
using EducationApp.BusinessLogicLayer.Models.User;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AuthorModel, Author>();
            CreateMap<Author, AuthorModel>();
            CreateMap<Author, CreateAuthorModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, UpdateAuthorModel>();
            CreateMap<UpdateAuthorModel, Author>();
            CreateMap<PrintingEditionModel, PrintingEdition>();
            CreateMap<PrintingEdition, PrintingEditionModel>();
            CreateMap<PrintingEdition, CreatePrintingEditionModel>();
            CreateMap<CreatePrintingEditionModel, PrintingEdition>();
            CreateMap<UpdatePrintingEditionModel, PrintingEdition>();
            CreateMap<PrintingEdition, UpdatePrintingEditionModel>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<CreateUserModel, User>();
            CreateMap<User, CreateUserModel>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<User, UpdateUserModel>();
            CreateMap<Role, RoleAccountModel>();
            CreateMap<RoleAccountModel,Role>();
        }
    }
}
