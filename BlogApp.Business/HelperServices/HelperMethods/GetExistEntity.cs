using BlogApp.Business.Exceptions.Common;
using BlogApp.Core.Entities;
using BlogApp.Core.Entities.Commons;
using BlogApp.DAL.Repositories.Implements;
using BlogApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.HelperServices.HelperMethods
{
    public class GetExistEntity<T> where T : BaseEntity , new()
    {
        readonly  IRepository<T> _repo;

        //public Task<> GetExistEntityCheck( int id) 
        //{
        //    if (id <= 0) throw new NegativeIdException();
        //    var entity =  _repo.FindByIdAsync(id);
        //    if (entity is null) throw new NotFoundException<T>();
        //    return entity;
        //}
    }
}
