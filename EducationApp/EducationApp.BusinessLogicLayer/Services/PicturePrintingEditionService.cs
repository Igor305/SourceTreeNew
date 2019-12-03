using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.PicturePrintingEdition;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PicturePrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PicturePrintingEditionService : IPicturePrintingEditionService
    {
        private readonly IMapper _mapper;
        private readonly IPicturePrintingEditionRepository _picturePrintingEditionRepository;
        public PicturePrintingEditionService(IPicturePrintingEditionRepository picturePrintingEditionRepository, IMapper mapper)
        {
            _picturePrintingEditionRepository = picturePrintingEditionRepository;
            _mapper = mapper;
        }

        public async Task<PicturePrintingEditionResponseModel> Create(CreatePicturePrintingEditionModel createPicturePrintingEditionModel)
        {
            PicturePrintingEditionResponseModel picturePrintingEditionResponseModel = new PicturePrintingEditionResponseModel();

            byte[] encodedBytes = Encoding.Unicode.GetBytes(createPicturePrintingEditionModel.Picture);
            string encodedTxt = Convert.ToBase64String(encodedBytes);
            createPicturePrintingEditionModel.Picture = encodedTxt;

            PicturePrintingEdition picturePrintingEdition = _mapper.Map<CreatePicturePrintingEditionModel, PicturePrintingEdition>(createPicturePrintingEditionModel);
            picturePrintingEdition.CreateDateTime = DateTime.Now;
            picturePrintingEdition.UpdateDateTime = DateTime.Now;

            await _picturePrintingEditionRepository.Create(picturePrintingEdition);

            PicturePrintingEditionModel picturePrintingEditionModel = _mapper.Map<PicturePrintingEdition, PicturePrintingEditionModel>(picturePrintingEdition);
            picturePrintingEditionResponseModel.PicturePrintingEditionsModels.Add(picturePrintingEditionModel);

            return picturePrintingEditionResponseModel;

        }
    }
}
