using Estacionamento.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.DataBase.Configuration
{
    public class EstacionamentoDataTypeMapConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        #region Utilities
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
        }

        #endregion

        #region Methods

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //add custom configuration
            PostConfigure(builder);
        }

        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        #endregion
    }
}
