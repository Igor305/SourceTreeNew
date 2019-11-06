using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetAll();
        Task<List<Order>> GetAllWithoutRemove();
        Task<List<Order>> Pagination(int Skip, int Take);
        Task<bool> CheckById(Guid id);
    }
}
