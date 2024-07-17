using BlogApp.Core.Entities.Commons;
using BlogApp.DAL.Contexts;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Implements;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{
    readonly AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task CreateAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await FindByIdAsync(id);
        Table.Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        return _getIncludes(Table,includes).Where(expression);
    }

    public async Task<TEntity> FindByIdAsync(int id, params string[] includes)
    {
        if (includes.Length == 0)
        {
            return await Table.FindAsync(id);
        }
        var query = Table.AsQueryable();
        return await _getIncludes(query,includes).SingleOrDefaultAsync(x=>x.Id == id);
    }

    public IQueryable<TEntity> GetAll(params string[] includes)
    {
        var query = Table.AsQueryable();
        return _getIncludes(query, includes);
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
    {
        return await _getIncludes(Table,includes).SingleOrDefaultAsync(expression);
    }

    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async void ToggleDelete(TEntity entity)
    {
        entity.IsDeleted = !entity.IsDeleted;
    }

    public async void SoftDelete(TEntity entity)
    {
        entity.IsDeleted = true;
    }

    public async void ReverseSoftDelete(TEntity entity)
    {
        entity.IsDeleted = false;
    }
    private IQueryable<TEntity> _getIncludes(IQueryable<TEntity> query,params string[] includes)
    {
        foreach (var item in includes)
        {
            query = query.Include(item);
        }
        return query;
    }
}
