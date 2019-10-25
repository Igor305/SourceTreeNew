using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }
        public List<Payment> GetAllIsDeleted()
        {
            var allIsDeleted = _applicationContext.Payments.IgnoreQueryFilters().ToList();
            return allIsDeleted;
        }
        public List<Payment> GetAll()
        {
            var all = _applicationContext.Payments.ToList();
            return all;
        }
    }
}
