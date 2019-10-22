using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        List<Payment> GetAllIsDeleted();
        List<Payment> GetAll();
    }
}
