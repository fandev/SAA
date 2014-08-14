using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class AplicacaoMap : EntityTypeConfiguration<Aplicacao>
    {
        public AplicacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AppId)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.AppKey)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Aplicacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AppId).HasColumnName("AppId");
            this.Property(t => t.AppKey).HasColumnName("AppKey");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao");
            this.Property(t => t.DataExpiracao).HasColumnName("DataExpiracao");
            this.Property(t => t.IdTipoAplicacao).HasColumnName("IdTipoAplicacao");

            // Relationships
            this.HasRequired(t => t.TipoAplicacao)
                .WithMany(t => t.Aplicacaos)
                .HasForeignKey(d => d.IdTipoAplicacao);

        }
    }
}
