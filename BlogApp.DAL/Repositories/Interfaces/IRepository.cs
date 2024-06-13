using BlogApp.Core.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity, new()
{
    public IQueryable<TEntity> GetAll();
    public DbSet<TEntity> Table { get;}
}
