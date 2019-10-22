using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        List<Author> GetAll();
        List<Author> GetAllIsDeleted();
        List<Author> Pagination(PaginationPageAuthorModel paginationPageAuthorModel);
        IEnumerable<Author> FindName(GetNameAuthorModel getNameAuthorModel);
        string Create(CreateAuthorModel createAuthorModel);
        string Update(UpdateAuthorModel updateAuthorModel);
        void Delete(DeleteAuthorModel deleteAuthorModel);
    }
}
