using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext aplicationContext) : base(aplicationContext)
        {
        }
        public List<Order> GetAllIsDeleted()
        {
            var allIsDeleted = _applicationContext.Orders.IgnoreQueryFilters().ToList();
            return allIsDeleted;
        }
        public List<Order> GetAll()
        {
            var all = _applicationContext.Orders.ToList();
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
