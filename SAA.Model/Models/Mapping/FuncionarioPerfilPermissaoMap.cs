using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class FuncionarioPerfilPermissaoMap : EntityTypeConfiguration<FuncionarioPerfilPermissao>
    {
        public FuncionarioPerfilPermissaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FuncionarioPerfilPermissao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFuncionarioPerfil).HasColumnName("IdFuncionarioPerfil");
            this.Property(t => t.IdAcao).HasColumnName("IdAcao");

            // Relationships
            this.HasRequired(t => t.Acao)
                .WithMany(t => t.FuncionarioPerfilPermissaos)
                .HasForeignKey(d => d.IdAcao);
            this.HasRequired(t => t.FuncionarioPerfil)
                .WithMany(t => t.FuncionarioPerfilPermissaos)
                .HasForeignKey(d => d.IdFuncionarioPerfil);

        }
    }
}
