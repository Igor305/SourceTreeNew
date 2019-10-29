using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
        public async Task<List<Payment>> GetAllIsDeleted()
        {
            List<Payment> payments = await _applicationContext.Payments.IgnoreQueryFilters().ToListAsync();
            return payments;
        }
        public async Task<List<Payment>> GetAll()
        {
            List<Payment> payments = await _applicationContext.Payments.ToListAsync();
            return payments;
        }
    }
}
