using Estacionamento.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.DataBase.DataContext
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase;

        int SaveChanges();

        string GenerateCreateScript();

        IQueryable<TEntity> Query<TEntity>() where TEntity : EntityBase;

        void Detach<TEntity>(TEntity entity) where TEntity : EntityBase;

    }
}
