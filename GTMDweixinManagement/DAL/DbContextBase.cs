using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace GTMDweixinManagement.DAL
{
    public class DbContextBase : DbContext, IDataRepository, IDisposable
    {
        public DbContextBase(string entityConnnectionString) : base(entityConnnectionString)
        {

        }
        public void Delete<T>(T entity) where T : ModelBase.ModelBase
        {
            Entry<T>(entity).State = EntityState.Deleted;
            SaveChanges();
        }

        public List<T> FildAll<T>(Expression<Func<T, bool>> conditions = null) where T : ModelBase.ModelBase
        {
            if (conditions == null)
                return Set<T>().ToList();
            else
                return Set<T>().Where(conditions).ToList();
        }

        public T Find<T>(params object[] keyValues) where T : ModelBase.ModelBase
        {
           return Set<T>().Find(keyValues);
        }

        public T Insert<T>(T entity) where T : ModelBase.ModelBase
        {
            Set<T>().Add(entity);
            SaveChanges();
            return entity;
        }

        public T Updata<T>(T entity) where T : ModelBase.ModelBase
        {
            Set<T>().Attach(entity); 
            Entry<T>(entity).State = EntityState.Modified;
            SaveChanges();
            return entity;
        }
    }
}