using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Hash)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IP)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.UserAgent)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("Token");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdUsuario).HasColumnName("IdUsuario");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.DataCriacao).HasColumnName("DataCriacao");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.UserAgent).HasColumnName("UserAgent");

            // Relationships
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Tokens)
                .HasForeignKey(d => d.IdUsuario);

        }
    }
}
