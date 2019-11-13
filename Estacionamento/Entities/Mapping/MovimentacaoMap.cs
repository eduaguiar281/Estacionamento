using Estacionamento.DataBase.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Entities.Mapping
{
    public class MovimentacaoMap: EstacionamentoDataTypeMapConfiguration<Movimentacao>
    {
        public override void Configure(EntityTypeBuilder<Movimentacao> builder)
        {
            builder.ToTable("Movimentacoes");
            builder.HasKey(k => k.Id);

            builder.HasOne(s => s.Veiculo)
                   .WithMany(s => s.Movimentacoes)
                   .HasForeignKey(fk => fk.VeiculoId);

            builder.HasOne(s => s.TabelaPreco)
                   .WithMany(s => s.Movimentacoes)
                   .HasForeignKey(fk => fk.TabelaPrecoId);


            base.Configure(builder);
        }
    }
}
