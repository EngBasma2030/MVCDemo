using Demo.DAL.Data.Repositries.Interfacies;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Repositries.Classes
{
    public class GenericRepository<TEntity>(AppDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public void Add(TEntity Entity) // object member method
        {
            _dbContext.Set<TEntity>().Add(Entity); // added
           // _dbContext.Add(Entity);
            //return _dbContext.SaveChanges(); // update database
        }

        public void Delate(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Remove(Entity); // deleted local 
            //return _dbContext.SaveChanges(); // update database
        }

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).ToList();
            }
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();

        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id); // find by primary Key
            // find<TEntity>(id)
        }

        public void Update(TEntity Entity)
        {
            _dbContext.Set<TEntity>().Update(Entity); // update local [modified]
            //return _dbContext.SaveChanges(); // update database
        }
    }
}
