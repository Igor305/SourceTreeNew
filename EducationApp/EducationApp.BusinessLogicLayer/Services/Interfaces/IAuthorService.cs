using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorResponseModel> GetAll();
        Task<AuthorResponseModel> GetAllWithoutRemove();
        Task<AuthorResponseModel> Pagination(PaginationAuthorModel paginationAuthorModel);
        Task<AuthorResponseModel> GetById(GetByIdAuthorModel getByIdAuthorModel);
        Task<AuthorResponseModel> GetByFullName(GetNameAuthorModel getNameAuthorModel);
        AuthorResponseModel Filter(FiltrationAuthorModel filtrationAuthorModel);
        Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel);
        Task<AuthorResponseModel> Update(UpdateAuthorModel updateAuthorModel);
        Task<AuthorResponseModel> Delete(DeleteAuthorModel deleteAuthorModel);
    }
}
