using BaseProject.Model;
using DataProject.Context;
using Microsoft.EntityFrameworkCore;

namespace DataProject.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
    {
        private readonly DbContextClass dbContext;
        public GenericRepository(DbContextClass dbContext) 
        {
            this.dbContext = dbContext;
        }

        public void RemoveById(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            dbContext.Set<TEntity>().Remove(entity);
        }
        public void Remove(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public TEntity GetById(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            return entity;

        }

        public List<TEntity> GetEntities()
        {
            return dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public void Insert(TEntity entity)
        {
            entity.InsertDate = DateTime.UtcNow;
            dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public void DeleteById(int id)
        {
            var entity = dbContext.Set<TEntity>().Find(id);
            entity.IsActive = false;
            entity.UpdateDate = DateTime.UtcNow;
            dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.IsActive = false;
            entity.UpdateDate = DateTime.UtcNow;
            dbContext.Set<TEntity>().Update(entity);

        }

        
    }
}
