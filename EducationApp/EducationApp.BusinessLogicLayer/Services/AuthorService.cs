﻿using AutoMapper;
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

            List<Author> allIsDeleted = await _authorRepository.GetAll();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(allIsDeleted);
            authorResponseModel.AuthorModel = authorModel;
            authorResponseModel.Messege = ResponseConstants.Successfully;
            authorResponseModel.Status = true;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> GetAllWithoutRemove()
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            List<Author> all = await _authorRepository.GetAllWithoutRemove();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(all);
            authorResponseModel.AuthorModel = authorModel;
            authorResponseModel.Messege = ResponseConstants.Successfully;
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

            bool IsWarning = (paginationAuthorModel.Skip < 0 || paginationAuthorModel.Take < 0);

            if (IsWarning)
            {
                authorResponseModel.Warning.Add(ResponseConstants.WaringlessThanZero);
            }
            authorResponseModel.Messege = ResponseConstants.Successfully;
            authorResponseModel.Status = true;

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
            bool isExistAuthor = await _authorRepository.CheckById(id);
            if (!isExistAuthor)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            authorResponseModel.Status = isExistAuthor;
            authorResponseModel.Messege = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

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

            bool IsError = (string.IsNullOrEmpty(getNameAuthorModel.FirstName) || string.IsNullOrEmpty(getNameAuthorModel.LastName));
            if (IsError)
            {
                authorResponseModel.Error.Add(ResponseConstants.Null);
            }
            authorResponseModel.Status = !IsError;
            authorResponseModel.Messege = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }

        public AuthorResponseModel Filter(FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = ValidateFilter(filtrationAuthorModel);

            List<Author> authors = _authorRepository.Filter(filtrationAuthorModel.FirstName, filtrationAuthorModel.LastName, filtrationAuthorModel.DateBirthFrom, filtrationAuthorModel.DateBirthTo, filtrationAuthorModel.DateDeathFrom, filtrationAuthorModel.DateDeathTo);
            List<AuthorModel> authorModels = _mapper.Map<List<Author>, List<AuthorModel>>(authors);
            authorResponseModel.AuthorModel = authorModels;

            return authorResponseModel;
        }

        private AuthorResponseModel ValidateFilter(FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            bool FullName = (!string.IsNullOrEmpty(filtrationAuthorModel.FirstName) || !string.IsNullOrEmpty(filtrationAuthorModel.LastName));
            bool DateBirth = (filtrationAuthorModel.DateBirthFrom != null || filtrationAuthorModel.DateBirthTo != null);
            bool DateDeath = (filtrationAuthorModel.DateDeathFrom != null || filtrationAuthorModel.DateDeathTo != null);
            bool IsWarning = FullName && DateBirth && DateDeath;

            if (IsWarning)
            {
                authorResponseModel.Warning.Add(ResponseConstants.Null);
            }
            authorResponseModel.Status = true;
            authorResponseModel.Messege = ResponseConstants.Successfully;

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

        private async Task<AuthorResponseModel> ValidateCreate(CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();

            bool nameAuthorIsExist = await _authorRepository.CheckByName(createAuthorModel.FirstName, createAuthorModel.LastName);
            bool isErrorOfNull = string.IsNullOrEmpty(createAuthorModel.FirstName) || string.IsNullOrEmpty(createAuthorModel.LastName);
            bool isErrorOfDate = createAuthorModel.DateBirth > createAuthorModel.DateDeath || createAuthorModel.DateBirth > DateTime.Now || (createAuthorModel.DateDeath >= DateTime.Now);

            bool isError = isErrorOfNull || isErrorOfDate || nameAuthorIsExist;
            bool isWarning = createAuthorModel.DateBirth == null;

            if (isErrorOfNull)
            {
                authorResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (isErrorOfDate)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            if (nameAuthorIsExist)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorClone);
            }
            if (isWarning)
            {
                authorResponseModel.Warning.Add(ResponseConstants.Null);
            }

            authorResponseModel.Status = !isError;
            authorResponseModel.Messege = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Update(Guid id, UpdateAuthorModel updateAuthorModel)
        {
            AuthorResponseModel authorResponseModel = await ValidateUpdate(id, updateAuthorModel);
            if (authorResponseModel.Status)
            {
                Author findauthor = await _authorRepository.GetById(id);
                _mapper.Map(updateAuthorModel, findauthor);
                findauthor.UpdateDateTime = DateTime.Now;
                await _authorRepository.Update(findauthor);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findauthor);
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }

        private async Task<AuthorResponseModel> ValidateUpdate(Guid id, UpdateAuthorModel updateAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if ((string.IsNullOrEmpty(updateAuthorModel.FirstName)) || (string.IsNullOrEmpty(updateAuthorModel.LastName)))
            {
                authorResponseModel.Error.Add(ResponseConstants.Null);
            }
            if (updateAuthorModel.DateBirth == null)
            {
                authorResponseModel.Warning.Add(ResponseConstants.Null);
            }
            if ((updateAuthorModel.DateBirth > updateAuthorModel.DateDeath) || (updateAuthorModel.DateBirth > DateTime.Now))
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            if (updateAuthorModel.DateDeath >= DateTime.Now)
            {
                updateAuthorModel.DateDeath = null;
            }

            bool isExistAuthor = await _authorRepository.CheckById(id);
            if (!isExistAuthor)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            bool isExistNameAuthor = await _authorRepository.CheckByName(updateAuthorModel.FirstName, updateAuthorModel.LastName);
            if (isExistNameAuthor)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorClone);
            }
            if (authorResponseModel.Error.Count == 0)
            {
                authorResponseModel.Status = true;
            }
            authorResponseModel.Messege = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }

        public async Task<AuthorResponseModel> Delete(Guid id)
        {

            AuthorResponseModel authorResponseModel = await ValidateDelete(id);
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
   
        private async Task<AuthorResponseModel> ValidateDelete(Guid id)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            bool isExistNameAuthor = await _authorRepository.CheckById(id);
            if (!isExistNameAuthor)
            {
                authorResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            authorResponseModel.Status = isExistNameAuthor;
            authorResponseModel.Messege = authorResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return authorResponseModel;
        }
    }
}

