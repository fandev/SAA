using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class AplicacaoUrlMap : EntityTypeConfiguration<AplicacaoUrl>
    {
        public AplicacaoUrlMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("AplicacaoUrl");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdAplicacao).HasColumnName("IdAplicacao");
            this.Property(t => t.Url).HasColumnName("Url");

            // Relationships
            this.HasRequired(t => t.Aplicacao)
                .WithMany(t => t.AplicacaoUrls)
                .HasForeignKey(d => d.IdAplicacao);

        }
    }
}
