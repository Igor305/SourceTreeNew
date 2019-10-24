using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorResponseModel> GetAllIsDeleted();
        Task<AuthorResponseModel> GetAll();
        AuthorResponseModel Pagination(PaginationPageAuthorModel paginationPageAuthorModel);
        Task<AuthorResponseModel> FindName(GetNameAuthorModel getNameAuthorModel);
        Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel);
        Task<AuthorResponseModel> Update(UpdateAuthorModel updateAuthorModel);
        Task<AuthorResponseModel> Delete(DeleteAuthorModel deleteAuthorModel);
    }
}
