using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class PicturePrintingEditionRepository : GenericRepository<PicturePrintingEdition>, IPicturePrintingEditionRepository
    {
        public PicturePrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
