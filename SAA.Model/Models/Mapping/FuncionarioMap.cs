using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class FuncionarioMap : EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Funcionario");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Matricula).HasColumnName("Matricula");
            this.Property(t => t.IdStatus).HasColumnName("IdStatus");

            // Relationships
            this.HasRequired(t => t.StatusFuncionario)
                .WithMany(t => t.Funcionarios)
                .HasForeignKey(d => d.IdStatus);

        }
    }
}
