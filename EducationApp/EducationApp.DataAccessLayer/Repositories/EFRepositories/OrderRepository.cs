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
        public async Task<List<Order>> GetAllIsDeleted()
        {
            List<Order> allIsDeleted = await _applicationContext.Orders.IgnoreQueryFilters().ToListAsync();
            return allIsDeleted;
        }
        public async Task<List<Order>> GetAll()
        {
            List<Order> all = await _applicationContext.Orders.ToListAsync();
            return all;
        }
        public IQueryable<Order> Pagination()
        {
            IQueryable<Order> orders = _applicationContext.Orders
                .Include(x => x.Payment)
                .Include(x => x.OrderItem)
                .Include(x => x.User);
            return orders;
        }
    }
}
