using BaseProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Repository
{
    public interface IGenericRepository<TEntity > where TEntity : BaseModel
    {
        TEntity GetById(int id);
        List<TEntity> GetEntities();
        void Update(TEntity entity);    
        void Insert(TEntity entity);
        void Remove(TEntity entity);
        void RemoveById(int id);
        void Delete(TEntity entity);
        void DeleteById(int id);
    }
}
