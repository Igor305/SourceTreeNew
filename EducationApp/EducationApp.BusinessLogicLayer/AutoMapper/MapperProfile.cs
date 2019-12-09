using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ImagePrintingEdition;
using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Account;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.ImagePrintingEdition;
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
            CreateMap<PrintingEditionModel, PrintingEdition>();
            CreateMap<PrintingEdition, PrintingEditionModel>();
            CreateMap<PrintingEdition, CreatePrintingEditionModel>();
            CreateMap<CreatePrintingEditionModel, PrintingEdition>();
            CreateMap<ImagePrintingEdition, CreateImagePrintingEditionModel>();
            CreateMap<CreateImagePrintingEditionModel, ImagePrintingEdition>();
            CreateMap<ImagePrintingEditionModel, ImagePrintingEdition>();
            CreateMap<ImagePrintingEdition, ImagePrintingEditionModel>();
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
            CreateMap<CreateUserModel, User>();
            CreateMap<User, CreateUserModel>();
            CreateMap<Role, RoleAccountModel>();
            CreateMap<RoleAccountModel,Role>();
            CreateMap<OrderItem, OrderItemModel>();
            CreateMap<OrderItemModel, OrderItem>();
            CreateMap<Payment, PaymentModel>();
            CreateMap<PaymentModel,Payment>();
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel,Order>();
        }
    }
}
