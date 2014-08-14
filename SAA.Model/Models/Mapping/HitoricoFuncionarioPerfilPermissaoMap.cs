using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class HitoricoFuncionarioPerfilPermissaoMap : EntityTypeConfiguration<HitoricoFuncionarioPerfilPermissao>
    {
        public HitoricoFuncionarioPerfilPermissaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("HitoricoFuncionarioPerfilPermissao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFuncionario).HasColumnName("IdFuncionario");
            this.Property(t => t.IdFuncionarioPerfilTransacao).HasColumnName("IdFuncionarioPerfilTransacao");
            this.Property(t => t.DataRegistro).HasColumnName("DataRegistro");

            // Relationships
            this.HasRequired(t => t.Funcionario)
                .WithMany(t => t.HitoricoFuncionarioPerfilPermissaos)
                .HasForeignKey(d => d.IdFuncionario);
            this.HasRequired(t => t.FuncionarioPerfilPermissao)
                .WithMany(t => t.HitoricoFuncionarioPerfilPermissaos)
                .HasForeignKey(d => d.IdFuncionarioPerfilTransacao);

        }
    }
}
