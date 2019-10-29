using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        public async Task<List<OrderItem>> GetAllIsDeleted()
        {
            List<OrderItem> orderItems = await _applicationContext.OrderItems.IgnoreQueryFilters().ToListAsync();
            return orderItems;
        }
        public async Task<List<OrderItem>> GetAll()
        {
            List<OrderItem> orderItems = await _applicationContext.OrderItems.ToListAsync();
            return orderItems;
        }
    }
}
