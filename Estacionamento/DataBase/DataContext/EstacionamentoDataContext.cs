using Estacionamento.DataBase.Configuration;
using Estacionamento.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Estacionamento.DataBase.DataContext
{
    public class EstacionamentoDataContext: DbContext, IDataContext
    {
        public EstacionamentoDataContext(DbContextOptions<EstacionamentoDataContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EstacionamentoDataTypeMapConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(typeConfiguration);
                configuration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }


        public virtual void Detach<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }

        public virtual string GenerateCreateScript()
        {
            return Database.GenerateCreateScript();
        }

        
        public new virtual IQueryable<TEntity> Query<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>().AsQueryable();
        }

        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }

    }
}
