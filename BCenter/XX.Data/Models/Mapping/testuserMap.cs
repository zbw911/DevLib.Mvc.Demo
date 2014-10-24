using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace XX.Data.Models.Mapping
{
    public class testuserMap : EntityTypeConfiguration<testuser>
    {
        public testuserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.uid, t.username });

            // Properties
            this.Property(t => t.uid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.username)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("testuser");
            this.Property(t => t.uid).HasColumnName("uid");
            this.Property(t => t.username).HasColumnName("username");
            this.Property(t => t.createtime).HasColumnName("createtime");
        }
    }
}
