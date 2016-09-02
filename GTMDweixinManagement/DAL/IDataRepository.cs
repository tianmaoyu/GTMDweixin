using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GTMDweixinManagement.ModelBase;

namespace GTMDweixinManagement.DAL
{
    public interface IDataRepository
    {
        T Updata<T>(T entity) where T : ModelBase.ModelBase;
        T Insert<T>(T entity) where T : ModelBase.ModelBase;
        void Delete<T>(T entity) where T : ModelBase.ModelBase;
        T Find<T>(params object[] keyValues) where T : ModelBase.ModelBase;
        List<T> FildAll<T>(Expression<Func<T, bool>> conditions = null) where T : ModelBase.ModelBase;
        
    }
}
