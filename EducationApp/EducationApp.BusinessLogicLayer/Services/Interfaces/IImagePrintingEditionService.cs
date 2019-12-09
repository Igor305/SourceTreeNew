using EducationApp.BusinessLogicLayer.Models.ImagePrintingEdition;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.ImagePrintingEdition;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IImagePrintingEditionService
    {
        Task<ImagePrintingEditionResponseModel> Create(CreateImagePrintingEditionModel createImagePrintingEditionModel);
        Task<ImagePrintingEditionResponseModel> Delete(Guid id);
    }
}
