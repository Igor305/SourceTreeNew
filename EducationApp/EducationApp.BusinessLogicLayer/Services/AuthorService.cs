using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Models.Enums;
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
            if (paginationPageAuthorModel.Take == 0)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Take is null");
            }
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
        public async Task<AuthorResponseModel> GetById(GetByIdAuthorModel getByIdAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            Author findAuthor = await _authorRepository.GetById(getByIdAuthorModel.Id);
            if (findAuthor == null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("This Id is not in database");
            }
            if (authorResponseModel.Messege == null)
            {
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findAuthor);
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> FindName(GetNameAuthorModel getNameAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            Author findNameAuthor = await _authorRepository.GetName(getNameAuthorModel.FirstName, getNameAuthorModel.LastName);
            if ((getNameAuthorModel.FirstName == null) || (getNameAuthorModel.LastName == null))
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
        public async Task<AuthorResponseModel> Filter(FiltrationAuthorModel filtrationAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if ((filtrationAuthorModel.DateBirthFirst == null)||(filtrationAuthorModel.DateBirthLast == null))
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Indicate DateBirth");
            }
            if ((filtrationAuthorModel.DateDeathFirst == null)||(filtrationAuthorModel.DateDeathLast == null))
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Indicate DateDeath");
            }
            if (authorResponseModel.Messege == null)
            {
                List<AuthorModel> authorModels = new List<AuthorModel>();
                switch (filtrationAuthorModel.NameFiltration)
                {            
                    case AuthorNameFilter.None:
                        List<Author> authors = await _authorRepository.GetAll();
                        authorModels = _mapper.Map<List<Author>,List<AuthorModel>>(authors);
                        break;
                    case AuthorNameFilter.DateBirth:
                        authors = _authorRepository.FiltrDateBirth(filtrationAuthorModel.DateBirthFirst, filtrationAuthorModel.DateBirthLast);
                        authorModels = _mapper.Map<List<Author>, List<AuthorModel>>(authors);
                        break;
                    case AuthorNameFilter.DateDeath:
                        authors = _authorRepository.FiltrDateDeath(filtrationAuthorModel.DateDeathFirst, filtrationAuthorModel.DateDeathLast);
                        authorModels = _mapper.Map<List<Author>, List<AuthorModel>>(authors);
                        break;
                    case AuthorNameFilter.DateBirthDateDeath:
                        authors = _authorRepository.FiltrDateBirthDateDeath(filtrationAuthorModel.DateBirthFirst, filtrationAuthorModel.DateBirthLast,filtrationAuthorModel.DateDeathFirst, filtrationAuthorModel.DateDeathLast);
                        authorModels = _mapper.Map<List<Author>, List<AuthorModel>>(authors);
                        break;
                    default:
                        authorResponseModel.Messege = "Error";
                        authorResponseModel.Status = false;
                        authorResponseModel.Error.Add("Invalid filtr type");
                        break;
                }
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel = authorModels;
            }
            return authorResponseModel;
        }
        public async Task<AuthorResponseModel> Create(CreateAuthorModel createAuthorModel)
        {
            AuthorResponseModel authorResponseModel = new AuthorResponseModel();
            if ((createAuthorModel.FirstName == null) || (createAuthorModel.LastName == null))
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
            if (createAuthorModel.DateBirth == null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("DateBirth not null");
            }
            if (createAuthorModel.DateBirth > createAuthorModel.DateDeath)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Date vot valide");
            }
            if (createAuthorModel.DateBirth > DateTime.Now)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("The author has not yet been born");
            }
            if (createAuthorModel.DateDeath >= DateTime.Now)
            {
                createAuthorModel.DateDeath = null;
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
            Author findauthor = await _authorRepository.GetById(updateAuthorModel.Id);
            if (findauthor == null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("This Id is not in database");
            }
            if ((updateAuthorModel.FirstName == null) || (updateAuthorModel.LastName == null))
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Name not null");
            }
            if (updateAuthorModel.DateBirth == null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("DateBirth not null");
            }
            if (updateAuthorModel.DateBirth > updateAuthorModel.DateDeath)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("Date vot valide");
            }
            if (updateAuthorModel.DateBirth > DateTime.Now)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("The author has not yet been born");
            }
            if (updateAuthorModel.DateDeath >= DateTime.Now)
            {
                updateAuthorModel.DateDeath = null;
            }          
            if (authorResponseModel.Messege == null)
            {
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
            if (findauthor == null)
            {
                authorResponseModel.Messege = "Error";
                authorResponseModel.Status = false;
                authorResponseModel.Error.Add("This Id is not in database");
            }
            if (authorResponseModel.Messege == null)
            {
                findauthor.IsDeleted = true;
                await _authorRepository.Update(findauthor);
                AuthorModel authorModel = _mapper.Map<Author, AuthorModel>(findauthor);
                authorResponseModel.Messege = "Successfully";
                authorResponseModel.Status = true;
                authorResponseModel.AuthorModel.Add(authorModel);
            }
            return authorResponseModel;
        }
    }
}

