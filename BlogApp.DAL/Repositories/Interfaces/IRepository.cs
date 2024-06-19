using BlogApp.Core.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    public DbSet<TEntity> Table { get;}
    public IQueryable<TEntity> GetAll();
    public IQueryable<TEntity> FindAll(Expression<Func<TEntity , bool>> expression);
    public Task<TEntity> FindByIdAsync(int id);
    public Task<TEntity> GetSingleAsync(Expression<Func<TEntity , bool>> expression);
    public Task<bool> IsExistAsync(Expression<Func<TEntity , bool>> expression);
    public Task CreateAsync(TEntity entity);
    public Task SaveAsync();    
    public void Delete(TEntity entity);
    public Task DeleteAsync(int id);
}
