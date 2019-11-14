using Estacionamento.DataBase.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Entities.Mapping
{
    public class TabelaPrecoMap : EstacionamentoDataTypeMapConfiguration<TabelaPreco>
    {
        public override void Configure(EntityTypeBuilder<TabelaPreco> builder)
        {
            builder.ToTable($"{nameof(TabelaPreco)}s");
            builder.HasKey(k => k.Id);

            builder.HasMany(a => a.Movimentacoes)
                   .WithOne(a => a.TabelaPreco)
                   .HasForeignKey(fk => fk.TabelaPrecoId);
                   
            base.Configure(builder);


        }
    }
}
