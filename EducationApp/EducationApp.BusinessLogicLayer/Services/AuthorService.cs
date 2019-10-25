using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<AuthorResponseModel> GetAllIsDeleted()
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            List<Author> allIsDeleted = await _authorRepository.GetAllIsDeleted();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(allIsDeleted);
            authorResponseModel.Messege = "Successfully";
            authorResponseModel.Status = true;
            authorResponseModel.AuthorModel = authorModel;
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> GetAll()
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            List<Author> all = await _authorRepository.GetAll();
            List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(all);
            authorResponseModel.Messege = "Successfully";
            authorResponseModel.Status = true;
            authorResponseModel.AuthorModel = authorModel;
            return authorResponseModel;
        }
        public AuthorResponseModel Pagination(PaginationPageAuthorModel paginationPageAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if (paginationPageAuthorModel.Skip < 0)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Skip < 0");
            }
            if (paginationPageAuthorModel.Take < 0)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Take < 0");
            }
            if (authorResponseModel.Messege == null)
            {
                IQueryable<Author> pagination = _authorRepository.Pagination();
                int count = pagination.Count();
                List<Author> items = pagination.Skip(paginationPageAuthorModel.Skip).Take(paginationPageAuthorModel.Take).ToList();

                PaginationAuthorModel paginationAuthorModel = new PaginationAuthorModel(count, paginationPageAuthorModel.Skip, paginationPageAuthorModel.Take);
                IndexViewModel viewModel = new IndexViewModel
                {
                    PaginationAuthorModel = paginationAuthorModel,
                    Authors = items
                };
                List<AuthorModel> authorModel = _mapper.Map<List<Author>, List<AuthorModel>>(items);
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel = authorModel;
            }
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> FindName(GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            Author findNameAuthor = await _authorRepository.GetName(getNameAuthorModel.FirstName, getNameAuthorModel.LastName);
            if (findNameAuthor == null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("No such name");
            }
            if (authorResponseModel.Messege == null)
            {
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findNameAuthor);
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if ((createAuthorModel.FirstName == null) && (createAuthorModel.LastName == null))
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Name not null");
            }
            Author findNameAuthor = await _authorRepository.GetName(createAuthorModel.FirstName,createAuthorModel.LastName);
            if (findNameAuthor != null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("There is such a name");
            }
            if (createAuthorModel.DataBirth > createAuthorModel.DataDeath)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Date vot valide");
            }
            if (createAuthorModel.DataBirth > DateTime.Now)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("The author has not yet been born");
            }
            if (createAuthorModel.DataDeath > DateTime.Now)
            {
                createAuthorModel.DataDeath = null;
            }
            if (authorResponseModel.Messege == null)
            {
                Author author = _mapper.Map<CreateAuthorModel, Author>(createAuthorModel);
                author.CreateDateTime = DateTime.Now;
                author.UpdateDateTime = DateTime.Now;
                await _authorRepository.Create(author);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(author);
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> Update(UpdateAuthorModel updateAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if ((updateAuthorModel.FirstName == null) && (updateAuthorModel.LastName == null))
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Name not null");
            }
            if (updateAuthorModel.DataBirth > updateAuthorModel.DataDeath)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Date vot valide");
            }
            if (updateAuthorModel.DataBirth > DateTime.Now)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("The author has not yet been born");
            }
            if (updateAuthorModel.DataDeath > DateTime.Now)
            {
                updateAuthorModel.DataDeath = null;
            }
            if (authorResponseModel.Messege == null)
            {
                Author findauthor = await _authorRepository.GetById(updateAuthorModel.Id);
                _mapper.Map(updateAuthorModel, findauthor);
                findauthor.UpdateDateTime = DateTime.Now;
                await _authorRepository.Update(findauthor);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findauthor);
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> Delete(DeleteAuthorModel deleteAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            Author findauthor = await _authorRepository.GetById(deleteAuthorModel.Id);
            findauthor.IsDeleted = true;
            await _authorRepository.Update(findauthor);
            AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findauthor);
            authorResponseModel.Messege = "Successfully";
            authorResponseModel.Status = true;
            authorResponseModel.AuthorModel.Add(authorModel);
            return authorResponseModel;
        }
    }
}

