using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStore.Domain.Entities;
using XStore.Domain.Shared.Parameters;
using XStore.Core.DomainObjects;

namespace XStore.Infra.Data.Mapping
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(TableNames.TbClient); //Define o nome da tabela
            //Name => Tamanho máximo de 100, Obrigatório
            //Email => Tamanho máximo de 100, Obrigatório
            //Cpf => Tamanho máximo de 14, Obrigatório
            
            
            //Active
            //AddressId

            builder.Ignore(a => a.Deleted);
        }
    }
}
