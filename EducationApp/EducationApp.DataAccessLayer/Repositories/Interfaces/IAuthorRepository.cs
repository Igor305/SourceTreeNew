﻿using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<List<Author>> GetAll();
        Task<List<Author>> GetAllWithoutRemove();
        Task<bool> CheckById(Guid id);
        Task<bool> CheckByName(string FirstName, string LastName);
        Task<Author> GetByFullName(string FirstName, string LastName);
        Task<List<Author>> Pagination(int Skip, int Take);
        List<Author> Filter(string FirstName, string LastName, DateTime? DateBirthFirst, DateTime? DateBirthLast, DateTime? DateDeathFirst, DateTime? DateDeathLast);
    }
}
