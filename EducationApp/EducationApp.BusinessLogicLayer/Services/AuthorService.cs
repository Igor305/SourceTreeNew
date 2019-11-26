using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<AuthorResponseModel> GetAll()
        {
            AuthorResponseModel authorResponseModel = ValidateOfSuccessfully(); ;

            List<Author> allIsDeleted = await _authorRepository.GetAll();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(allIsDeleted);
            authorResponseModel.AuthorModel = authorModel;

            return authorResponseModel;
        }
        
        public async Task<AuthorResponseModel> GetAllWithoutRemove()
        {
            AuthorResponseModel authorResponseModel = ValidateOfSuccessfully();

            List<Author> all = await _authorRepository.GetAllWithoutRemove();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(all);
            authorResponseModel.AuthorModel = authorModel;

            return authorResponseModel;
        }

        private AuthorResponseModel ValidateOfSuccessfully()
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            authorResponseModel.Message = ResponseConstants.Successfully;
            authorResponseModel.Status = true;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Pagination(PaginationAuthorModel paginationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = ValidatePagination(paginationAuthorModel);

            List<Author> pagination = await _authorRepository.Pagination(paginationAuthorModel.Skip, paginationAuthorModel.Take);
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(pagination);
            authorResponseModel.AuthorModel = authorModel;

            return authorResponseModel;
        }

        private AuthorResponseModel ValidatePagination(PaginationAuthorModel paginationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            bool isWarning = (paginationAuthorModel.Skip < 0 || paginationAuthorModel.Take < 0);

            if (isWarning)
            {
                authorResponseModel.Warning.Add(ResponseConstants.LessThanZero);
            }
            authorResponseModel.Status = true;
            authorResponseModel.Message = ResponseConstants.Successfully;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> GetById(Guid id)
        {
            AuthorResponseModel authorResponseModel = await ValidateGetById(id);

            if (authorResponseModel.Status)
            {
                Author findAuthor = await _authorRepository.GetById(id);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findAuthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }

            return authorResponseModel;
        }

        private async Task<AuthorResponseModel> ValidateGetById(Guid id)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            bool isExist = await _authorRepository.CheckById(id);
            if (!isExist)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            authorResponseModel.Status = isExist;
            authorResponseModel.Message = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> GetByFullName(GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = ValidateGetByFullName(getNameAuthorModel);

            if (authorResponseModel.Status)
            {
                Author findNameAuthor = await _authorRepository.GetByFullName(getNameAuthorModel.FirstName, getNameAuthorModel.LastName);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findNameAuthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }

            return authorResponseModel;
        }
        private AuthorResponseModel ValidateGetByFullName(GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            bool isError = (string.IsNullOrEmpty(getNameAuthorModel.FirstName) || string.IsNullOrEmpty(getNameAuthorModel.LastName));
            if (isError)
            {
                authorResponseModel.Error.Add(ResponseConstants.Null);
            }
            authorResponseModel.Status = !isError;
            authorResponseModel.Message = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }

        public AuthorResponseModel Filtration(FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = ValidateFiltration(filtrationAuthorModel);

            List<Author> authors = _authorRepository.Filtration(filtrationAuthorModel.FirstName, filtrationAuthorModel.LastName, filtrationAuthorModel.DateBirthFrom, filtrationAuthorModel.DateBirthTo, filtrationAuthorModel.DateDeathFrom, filtrationAuthorModel.DateDeathTo);
            List<AuthorModel> authorModels = _mapper.Map<List<Author>, List<AuthorModel>>(authors);
            authorResponseModel.AuthorModel = authorModels;

            return authorResponseModel;
        }

        private AuthorResponseModel ValidateFiltration(FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            bool FullName = (!string.IsNullOrEmpty(filtrationAuthorModel.FirstName) || !string.IsNullOrEmpty(filtrationAuthorModel.LastName));
            bool DateBirth = (filtrationAuthorModel.DateBirthFrom != null || filtrationAuthorModel.DateBirthTo != null);
            bool DateDeath = (filtrationAuthorModel.DateDeathFrom != null || filtrationAuthorModel.DateDeathTo != null);
            bool isWarning = FullName && DateBirth && DateDeath;

            if (isWarning)
            {
                authorResponseModel.Warning.Add(ResponseConstants.Null);
            }
            authorResponseModel.Status = true;
            authorResponseModel.Message = ResponseConstants.Successfully;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateCreate(createAuthorModel);

            if (authorResponseModel.Status)
            {
                Author author = _mapper.Map<CreateAuthorModel, Author>(createAuthorModel);
                author.CreateDateTime = DateTime.Now;
                author.UpdateDateTime = DateTime.Now;
                await _authorRepository.Create(author);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(author);
                authorResponseModel.AuthorModel.Add(authorModel);
            }

            return authorResponseModel;
        }

        private async Task<AuthorResponseModel> ValidateCreate(CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            bool isExistName = await _authorRepository.CheckByName(createAuthorModel.FirstName, createAuthorModel.LastName);
            bool isErrorOfNull = string.IsNullOrEmpty(createAuthorModel.FirstName) || string.IsNullOrEmpty(createAuthorModel.LastName);
            bool isErrorOfDate = createAuthorModel.DateBirth > createAuthorModel.DateDeath || createAuthorModel.DateBirth > DateTime.Now || (createAuthorModel.DateDeath >= DateTime.Now);

            bool isError = isErrorOfNull || isErrorOfDate || isExistName;

            bool isWarningOfNull = createAuthorModel.DateBirth == null;

            if (isErrorOfNull)
            {
                authorResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (isErrorOfDate)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            if (isExistName)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorClone);
            }
            if (isWarningOfNull)
            {
                authorResponseModel.Warning.Add(ResponseConstants.Null);
            }

            authorResponseModel.Status = !isError;
            authorResponseModel.Message = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Update(Guid id, CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateUpdate(id, createAuthorModel);

            if (authorResponseModel.Status)
            {
                Author findauthor = await _authorRepository.GetById(id);
                _mapper.Map(createAuthorModel, findauthor);
                findauthor.UpdateDateTime = DateTime.Now;
                await _authorRepository.Update(findauthor);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findauthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }

            return authorResponseModel;
        }

        private async Task<AuthorResponseModel> ValidateUpdate(Guid id, CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateGetById(id);
            authorResponseModel = await ValidateCreate(createAuthorModel);

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Delete(Guid id)
        {

            AuthorResponseModel authorResponseModel = await ValidateGetById(id);

            if (authorResponseModel.Status)
            {
                Author author = await _authorRepository.GetById(id);
                author.IsDeleted = true;
                await _authorRepository.Update(author);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(author);
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
    }
}