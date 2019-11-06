using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext aplicationContext) : base(aplicationContext)
        {
        }

        public async Task<List<Order>> GetAll()
        {
            List<Order> orders = await _applicationContext.Orders.IgnoreQueryFilters().ToListAsync();
            return orders;
        }

        public async Task<List<Order>> GetAllWithoutRemove()
        {
            List<Order> orders = await _applicationContext.Orders.ToListAsync();
            return orders;
        }

        public async Task <List<Order>> Pagination(int Skip, int Take)
        {
            List<Order> orders = await _applicationContext.Orders.ToListAsync();
            List<Order> paginationOrders = orders.Skip(Skip).Take(Take).ToList();
            return paginationOrders;
        }

        public async Task<bool> CheckById(Guid id)
        {
            bool order = await _applicationContext.Orders.AnyAsync(x => x.Id == id);
            return order;
        }
    }
}
