using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SAA.Model.Models.Mapping
{
    public class UserTokenAppTokenMap : EntityTypeConfiguration<UserTokenAppToken>
    {
        public UserTokenAppTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserTokenAppToken");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdUserToken).HasColumnName("IdUserToken");
            this.Property(t => t.IdAplicacao).HasColumnName("IdAplicacao");
            this.Property(t => t.DataExpiracao).HasColumnName("DataExpiracao");
            this.Property(t => t.DataRegistro).HasColumnName("DataRegistro");

            // Relationships
            this.HasRequired(t => t.Aplicacao)
                .WithMany(t => t.UserTokenAppTokens)
                .HasForeignKey(d => d.IdAplicacao);
            this.HasRequired(t => t.Token)
                .WithMany(t => t.UserTokenAppTokens)
                .HasForeignKey(d => d.IdUserToken);

        }
    }
}
