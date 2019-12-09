using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class ImagePrintingEditionRepository : GenericRepository<ImagePrintingEdition>, IImagePrintingEditionRepository
    {
        public ImagePrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
