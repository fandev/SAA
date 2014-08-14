using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class UsuarioAplicacaoMap : EntityTypeConfiguration<UsuarioAplicacao>
    {
        public UsuarioAplicacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UsuarioAplicacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdAplicacao).HasColumnName("IdAplicacao");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.LogOut).HasColumnName("LogOut");
            this.Property(t => t.DataRegistro).HasColumnName("DataRegistro");

            // Relationships
            this.HasRequired(t => t.Aplicacao)
                .WithMany(t => t.UsuarioAplicacaos)
                .HasForeignKey(d => d.IdAplicacao);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.UsuarioAplicacaos)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
