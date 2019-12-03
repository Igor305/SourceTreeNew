using EducationApp.BusinessLogicLayer.Models.PicturePrintingEdition;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PicturePrintingEdition;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPicturePrintingEditionService
    {
        Task<PicturePrintingEditionResponseModel> Create(CreatePicturePrintingEditionModel createPicturePrintingEditionModel);
    }
}
