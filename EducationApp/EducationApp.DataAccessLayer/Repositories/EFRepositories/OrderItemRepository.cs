using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
        public List<OrderItem> GetAllIsDeleted()
        {
            var allIsDeleted = _applicationContext.OrderItems.IgnoreQueryFilters().ToList();
            return allIsDeleted;
        }
        public List<OrderItem> GetAll()
        {
            var all = _applicationContext.OrderItems.ToList();
            return all;
        }
    }
}
