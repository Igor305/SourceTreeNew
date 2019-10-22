using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        List<Order> GetAllIsDeleted();
        List<Order> GetAll();
        IQueryable<Order> Pagination();
    }
}
