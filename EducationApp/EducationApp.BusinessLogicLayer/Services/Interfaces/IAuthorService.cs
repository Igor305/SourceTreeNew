using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorResponseModel> GetAll();
        Task<AuthorResponseModel> GetAllWithoutRemove();
        Task<AuthorResponseModel> Pagination(PaginationAuthorModel paginationAuthorModel);
        Task<AuthorResponseModel> GetById(Guid id);
        Task<AuthorResponseModel> GetByFullName(GetNameAuthorModel getNameAuthorModel);
        AuthorResponseModel Filtration(FiltrationAuthorModel filtrationAuthorModel);
        Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel);
        Task<AuthorResponseModel> Update(Guid id, CreateAuthorModel createAuthorModel);
        Task<AuthorResponseModel> Delete(Guid id);
    }
}
