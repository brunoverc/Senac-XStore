using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XStore.Domain.Entities;
using XStore.Domain.Shared.Parameters;

namespace XStore.Infra.Data.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(TableNames.TbAddress); //Define o nome da tabela
            builder.Property(a => a.Street).HasMaxLength(200).IsRequired();//Tamanho máximo de 200 e obrigatória
            builder.Property(a => a.Number).HasMaxLength(5);
            builder.Property(a => a.Complement).HasMaxLength(200);
            builder.Property(a => a.Neighborhood).HasMaxLength(100).IsRequired();
            builder.Property(a => a.City).HasMaxLength(50).IsRequired();
            builder.Property(a => a.State).HasMaxLength(2).IsRequired();
            builder.Ignore(a => a.Deleted);
        }
    }
}
