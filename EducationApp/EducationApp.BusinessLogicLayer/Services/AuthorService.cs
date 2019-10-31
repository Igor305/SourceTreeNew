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
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            List<Author> allIsDeleted = await _authorRepository.GetAll();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(allIsDeleted);
            authorResponseModel.AuthorModel = authorModel;
            authorResponseModel.Messege = responseConstants.Successfully;
            authorResponseModel.Status = true;
            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> GetAllWithoutRemove()
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            List<Author> all = await _authorRepository.GetAllWithoutRemove();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(all);
            authorResponseModel.AuthorModel = authorModel;
            authorResponseModel.Messege = responseConstants.Successfully;
            authorResponseModel.Status = true;
            return authorResponseModel;
        }

        private AuthorResponseModel ValidatePagination(PaginationAuthorModel paginationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            if (paginationAuthorModel.Take == 0)
            {
                authorResponseModel.Warning.Add(responseConstants.Null);
            }
            if (paginationAuthorModel.Skip < 0 || paginationAuthorModel.Take < 0)
            {
                authorResponseModel.Warning.Add(responseConstants.WaringlessThanZero);
            }
            authorResponseModel.Messege = responseConstants.Successfully;
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

        private async Task<AuthorResponseModel> ValidateGetById(GetByIdAuthorModel getByIdAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            Author findAuthor = await _authorRepository.GetById(getByIdAuthorModel.Id);
            if (findAuthor == null)
            {
                authorResponseModel.Error.Add(responseConstants.ErrorId);
            }
            if (authorResponseModel.Error.Count == 0)
            {
                authorResponseModel.Status = true;
            }
            authorResponseModel.Messege = authorResponseModel.Status == true ? responseConstants.Successfully : responseConstants.Error;
            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> GetById(GetByIdAuthorModel getByIdAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateGetById(getByIdAuthorModel);
            if (authorResponseModel.Status == true)
            {
                Author findAuthor = await _authorRepository.GetById(getByIdAuthorModel.Id);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findAuthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }

        private AuthorResponseModel ValidateGetByFullName(GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            if ((string.IsNullOrEmpty(getNameAuthorModel.FirstName)) || (string.IsNullOrEmpty(getNameAuthorModel.LastName)))
            {
                authorResponseModel.Error.Add(responseConstants.Null);
            }
            if (authorResponseModel.Error.Count == 0)
            {
                authorResponseModel.Status = true;
            }
            authorResponseModel.Messege = authorResponseModel.Status == true ? responseConstants.Successfully : responseConstants.Error;
            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> GetByFullName(GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = ValidateGetByFullName(getNameAuthorModel);
            if (authorResponseModel.Status == true)
            {
                Author findNameAuthor = await _authorRepository.GetByFullName(getNameAuthorModel.FirstName, getNameAuthorModel.LastName);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findNameAuthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }

        private AuthorResponseModel ValidateFilter(FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            if ((string.IsNullOrEmpty(filtrationAuthorModel.FirstName)) || (string.IsNullOrEmpty(filtrationAuthorModel.LastName)) || (filtrationAuthorModel.DateBirthFirst == null) || (filtrationAuthorModel.DateBirthLast == null) || (filtrationAuthorModel.DateDeathFirst == null) || (filtrationAuthorModel.DateDeathLast == null))
            {
                authorResponseModel.Warning.Add(responseConstants.Null);
            }
            authorResponseModel.Messege = responseConstants.Successfully;
            authorResponseModel.Status = true;
            return authorResponseModel;
        }

        public AuthorResponseModel Filter(FiltrationAuthorModel filtrationAuthorModel)
        {
            ResponseConstants responseConstants = new ResponseConstants();
            AuthorResponseModel authorResponseModel = ValidateFilter(filtrationAuthorModel);
            List<Author> authors = _authorRepository.Filter(filtrationAuthorModel.FirstName, filtrationAuthorModel.LastName, filtrationAuthorModel.DateBirthFirst, filtrationAuthorModel.DateBirthLast, filtrationAuthorModel.DateDeathFirst, filtrationAuthorModel.DateDeathLast);
            List<AuthorModel> authorModels = _mapper.Map<List<Author>, List<AuthorModel>>(authors);
            authorResponseModel.AuthorModel = authorModels;
            return authorResponseModel;
        }

        private async Task<AuthorResponseModel> ValidateCreate(CreateAuthorModel createAuthorModel)
        {
            ResponseConstants responseConstants = new ResponseConstants();
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if ((string.IsNullOrEmpty(createAuthorModel.FirstName)) || (string.IsNullOrEmpty(createAuthorModel.LastName)))
            {
                authorResponseModel.Error.Add(responseConstants.Null);
            }
            if (createAuthorModel.DateBirth == null)
            {
                authorResponseModel.Warning.Add(responseConstants.Null);
            }
            if ((createAuthorModel.DateBirth > createAuthorModel.DateDeath) || (createAuthorModel.DateBirth > DateTime.Now))
            {
                authorResponseModel.Error.Add(responseConstants.ErrorDate);
            }
            if (createAuthorModel.DateDeath >= DateTime.Now)
            {
                createAuthorModel.DateDeath = null;
            }
            Author findNameAuthor = await _authorRepository.GetByFullName(createAuthorModel.FirstName, createAuthorModel.LastName);
            if (findNameAuthor != null)
            {
                authorResponseModel.Error.Add(responseConstants.ErrorClone);
            }
            if (authorResponseModel.Error.Count == 0)
            {
                authorResponseModel.Status = true;
            }
            authorResponseModel.Messege = authorResponseModel.Status == true ? responseConstants.Successfully : responseConstants.Error;
            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateCreate(createAuthorModel);
            if (authorResponseModel.Status == true)
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

        private async Task<AuthorResponseModel> ValidateUpdate(UpdateAuthorModel updateAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            if ((string.IsNullOrEmpty(updateAuthorModel.FirstName)) || (string.IsNullOrEmpty(updateAuthorModel.LastName)))
            {
                authorResponseModel.Error.Add(responseConstants.Null);
            }
            if (updateAuthorModel.DateBirth == null)
            {
                authorResponseModel.Warning.Add(responseConstants.Null);
            }
            if ((updateAuthorModel.DateBirth > updateAuthorModel.DateDeath) || (updateAuthorModel.DateBirth > DateTime.Now))
            {
                authorResponseModel.Error.Add(responseConstants.ErrorDate);
            }
            if (updateAuthorModel.DateDeath >= DateTime.Now)
            {
                updateAuthorModel.DateDeath = null;
            }
            Author findauthor = await _authorRepository.GetById(updateAuthorModel.Id);
            if (findauthor == null)
            {
                authorResponseModel.Error.Add(responseConstants.ErrorId);
            }
            if (authorResponseModel.Error.Count == 0)
            {
                authorResponseModel.Status = true;
            }
            authorResponseModel.Messege = authorResponseModel.Status == true ? responseConstants.Successfully : responseConstants.Error;
            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Update(UpdateAuthorModel updateAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateUpdate(updateAuthorModel);
            if (authorResponseModel.Status == true)
            {
                Author findauthor = await _authorRepository.GetById(updateAuthorModel.Id);
                _mapper.Map(updateAuthorModel, findauthor);
                findauthor.UpdateDateTime = DateTime.Now;
                await _authorRepository.Update(findauthor);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findauthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }

        private async Task<AuthorResponseModel> ValidateDelete(DeleteAuthorModel deleteAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            ResponseConstants responseConstants = new ResponseConstants();
            Author author = await _authorRepository.GetById(deleteAuthorModel.Id);
            if (author == null)
            {
                authorResponseModel.Error.Add(responseConstants.ErrorId);
            }
            if (authorResponseModel.Error?.Count < 1)
            {
                authorResponseModel.Status = true;
            }
            authorResponseModel.Messege = authorResponseModel.Status == true ? responseConstants.Successfully : responseConstants.Error;
            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Delete(DeleteAuthorModel deleteAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (authorResponseModel.Status == true)
            {
                Author author = await _authorRepository.GetById(deleteAuthorModel.Id);
                author.IsDeleted = true;
                await _authorRepository.Update(author);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(author);
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
    }
}

