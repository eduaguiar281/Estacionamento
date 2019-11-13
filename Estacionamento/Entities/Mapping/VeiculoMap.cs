using Estacionamento.DataBase.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Entities.Mapping
{
    public class VeiculoMap :EstacionamentoDataTypeMapConfiguration<Veiculo>
    {
        public override void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable($"{nameof(Veiculo)}s");
            builder.HasKey(k => k.Id);

            builder.HasMany(a => a.Movimentacoes)
                   .WithOne(a => a.Veiculo)
                   .HasForeignKey(fk => fk.VeiculoId);

            base.Configure(builder);
        }
    }
}
