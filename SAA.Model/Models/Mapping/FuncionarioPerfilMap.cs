using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class FuncionarioPerfilMap : EntityTypeConfiguration<FuncionarioPerfil>
    {
        public FuncionarioPerfilMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FuncionarioPerfil");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFuncionario).HasColumnName("IdFuncionario");
            this.Property(t => t.IdPerfilAplicacao).HasColumnName("IdPerfilAplicacao");

            // Relationships
            this.HasRequired(t => t.Funcionario)
                .WithMany(t => t.FuncionarioPerfils)
                .HasForeignKey(d => d.IdFuncionario);
            this.HasRequired(t => t.PerfilAplicacao)
                .WithMany(t => t.FuncionarioPerfils)
                .HasForeignKey(d => d.IdPerfilAplicacao);

        }
    }
}
