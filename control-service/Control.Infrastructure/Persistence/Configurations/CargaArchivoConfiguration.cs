using Control.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Control.Infrastructure.Data.Configurations
{
    public class CargaArchivoConfiguration : IEntityTypeConfiguration<CargaArchivo>
    {
        public void Configure(EntityTypeBuilder<CargaArchivo> builder)
        {
            builder.ToTable("CargaArchivo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.NombreArchivo).HasMaxLength(200);
            builder.Property(x => x.Usuario).HasMaxLength(150);
        }
    }
}
