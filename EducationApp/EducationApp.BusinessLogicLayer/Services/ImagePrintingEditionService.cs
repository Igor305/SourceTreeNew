using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.ImagePrintingEdition;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.ImagePrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class ImagePrintingEditionService : IImagePrintingEditionService
    {
        private readonly IMapper _mapper;
        private readonly IImagePrintingEditionRepository _imagePrintingEditionRepository;
        public ImagePrintingEditionService(IImagePrintingEditionRepository imagePrintingEditionRepository, IMapper mapper)
        {
            _imagePrintingEditionRepository = imagePrintingEditionRepository;
            _mapper = mapper;
        }

        public async Task<ImagePrintingEditionResponseModel> Create(CreateImagePrintingEditionModel createImagePrintingEditionModel)
        {
            ImagePrintingEditionResponseModel imagePrintingEditionResponseModel = new ImagePrintingEditionResponseModel();

            ImagePrintingEdition imagePrintingEdition = _mapper.Map<CreateImagePrintingEditionModel, ImagePrintingEdition>(createImagePrintingEditionModel);
            imagePrintingEdition.CreateDateTime = DateTime.Now;
            imagePrintingEdition.UpdateDateTime = DateTime.Now;

            await _imagePrintingEditionRepository.Create(imagePrintingEdition);

            ImagePrintingEditionModel imagePrintingEditionModel = _mapper.Map<ImagePrintingEdition, ImagePrintingEditionModel>(imagePrintingEdition);
            imagePrintingEditionResponseModel.ImagePrintingEditionsModels.Add(imagePrintingEditionModel);

            return imagePrintingEditionResponseModel;

        }

        public async Task<ImagePrintingEditionResponseModel> Delete(Guid id)
        {
            ImagePrintingEditionResponseModel ImagePrintingEditionResponseModel = await DeleteValidate(id);

            ImagePrintingEdition ImagePrintingEdition = await _imagePrintingEditionRepository.GetById(id);
            await _imagePrintingEditionRepository.Delete(ImagePrintingEdition);

            return ImagePrintingEditionResponseModel;
        }

        private async Task<ImagePrintingEditionResponseModel> DeleteValidate(Guid id)
        {
            ImagePrintingEditionResponseModel ImagePrintingEditionResponseModel = new ImagePrintingEditionResponseModel();

            ImagePrintingEdition ImagePrintingEdition = await _imagePrintingEditionRepository.GetById(id);
            bool isExists = ImagePrintingEdition != null;

            if (!isExists)
            {
                ImagePrintingEditionResponseModel.Error.Add(ResponseConstants.Null);
            }
            ImagePrintingEditionResponseModel.Status = isExists;
            ImagePrintingEditionResponseModel.Message = ImagePrintingEditionResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return ImagePrintingEditionResponseModel;
        }
    }
}
