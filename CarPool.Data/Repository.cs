using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Data
{
    public class Repository
    {
        CarPoolDbContext dbContext = new CarPoolDbContext();

        public void Add<T>(T tObject) where T : class
        {         
            try
            {
                dbContext.Set<T>().Add(tObject);
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<T> GetAllData<T>() where T : class
        {
            return dbContext.Set<T>().ToList<T>();
        }

        public List<T>FindAllItems<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbContext.Set<T>().Where(predicate.Compile()).ToList<T>();
        }

        public T FindItem<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return dbContext.Set<T>().FirstOrDefault(predicate.Compile());
        }

        public void Update<T>(T tObject) where T : class
        {
            dbContext.Set<T>().AddOrUpdate(tObject);
            dbContext.SaveChanges();
        }

        public int Count<T>(Expression<Func<T, bool>> predicate=null) where T : class
        {
            return dbContext.Set<T>().Where(predicate.Compile()).Count();
        }

        public void Delete<T>(T tObject) where T : class
        {
            try
            {
                dbContext.Set<T>().Remove(tObject);
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}
