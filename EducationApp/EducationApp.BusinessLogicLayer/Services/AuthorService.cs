using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Authors;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Author> GetAllIsDeleted()
        {
            var allIsDeleted = _authorRepository.GetAllIsDeleted();
            return allIsDeleted;
        }
        public List<Author> GetAll()
        {
            var all = _authorRepository.GetAll();
            return all;
        }
        public List<Author> Pagination(PaginationPageAuthorModel paginationPageAuthorModel)
        {
            if (paginationPageAuthorModel.Skip < 1)
            {
                paginationPageAuthorModel.Skip = 1;
            }
            var pagination = _authorRepository.Pagination();
            var count = pagination.Count();
            var items = pagination.Skip(paginationPageAuthorModel.Skip).Take(paginationPageAuthorModel.Take).ToList();

            PaginationAuthorModel paginationAuthorModel = new PaginationAuthorModel(count, paginationPageAuthorModel.Skip, paginationPageAuthorModel.Take);
            IndexViewModel viewModel = new IndexViewModel
            {
                PaginationAuthorModel = paginationAuthorModel,
                Authors = items
            };
            return items;
        }
        public IEnumerable<Author> FindName(GetNameAuthorModel getNameAuthorModel)
        {
            var all = _authorRepository.GetAll();
            var findNameAuthor = all.Where(x => x.Name == getNameAuthorModel.Name);
            return findNameAuthor;
        }
        public string Create(CreateAuthorModel createAuthorModel)
        {
            if (createAuthorModel.Name == null)
            {
                string noNull = "Name not null";
                return noNull;
            }
            var all = _authorRepository.GetAll();
            var cloneauthor = all.Any(x => x.Name == createAuthorModel.Name);
            if (cloneauthor == true)
            {
                string noNull = "There is such a name";
                return noNull;
            }
            Author author = new Author();
            author.Name = createAuthorModel.Name;
            if (createAuthorModel.DateBirth > createAuthorModel.DataDeath)
            {
                string dateNotValide = "Date vot valide";
                return dateNotValide;
            }
            if ((createAuthorModel.DateBirth > DateTime.Now) || (createAuthorModel.DataDeath > DateTime.Now))
            {
                string dateNotVanga = "The future has not come yet";
                return dateNotVanga;
            }
            var model = _mapper.Map<CreateAuthorModel>(author);
            // author.DataBirth = createAuthorModel.DateBirth;
            // author.DataDeath = createAuthorModel.DataDeath;
            author.CreateDateTime = DateTime.Now;
            author.UpdateDateTime = DateTime.Now;
            _authorRepository.Create(author);
            string status = "Добавлена новая запись";
            return status;
        }
        public string Update(UpdateAuthorModel updateAuthorModel)
        {
            var all = _authorRepository.GetAll();
            var findauthor = all.Find(x => x.Id == updateAuthorModel.Id);
            if (updateAuthorModel.Name == null)
            {
                string noNull = "Name not null";
                return noNull;
            }
            findauthor.Name = updateAuthorModel.Name;
            if (updateAuthorModel.DateBirth > updateAuthorModel.DataDeath)
            {
                string dateNotValide = "Date vot valide";
                return dateNotValide;
            }
            if ((updateAuthorModel.DateBirth > DateTime.Now) || (updateAuthorModel.DataDeath > DateTime.Now))
            {
                string dateNotVanga = "The future has not come yet";
                return dateNotVanga;
            }
            findauthor.DataBirth = updateAuthorModel.DateBirth;
            findauthor.DataDeath = updateAuthorModel.DataDeath;
            findauthor.UpdateDateTime = DateTime.Now;
            _authorRepository.Update(findauthor);
            string status = "Добавлена новая запись";
            return status;
        }
        public void Delete(DeleteAuthorModel deleteAuthorModel)
        {
            var all = _authorRepository.GetAll();
            var findauthor = all.Find(x => x.Id == deleteAuthorModel.Id);
            findauthor.IsDeleted = true;
            _authorRepository.Update(findauthor);
        }
    }
}

