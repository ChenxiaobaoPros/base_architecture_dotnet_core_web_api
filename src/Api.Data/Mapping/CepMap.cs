using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            builder.ToTable("Cep");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c=> c.Cep);

            builder.HasOne( c => c.Municipio )
                    .WithMany(m => m.Ceps);
                           



        }
    }
}
