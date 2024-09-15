using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RealEstate.DAL.Models.Home;

namespace RealEstate.DAL.Data
{
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Role)
                .HasMaxLength(50);

            builder.Property(t => t.ProfileImageUrl)
                .HasMaxLength(255);

            builder.Property(t => t.IconUrl)
                .HasMaxLength(255);

            builder.Property(t => t.Email)
                .HasMaxLength(150);

            builder.Property(t => t.Phone)
                .HasMaxLength(20);

            builder.Property(t => t.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(t => t.UpdatedDate)
                .HasDefaultValue(null);

            builder.Property(t => t.IpAddress)
                .HasMaxLength(45);

            builder.Property(t => t.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(t => t.IsHidden)
                .HasDefaultValue(false);


            builder.Property(t => t.FacebookLink)
                .HasMaxLength(255);

            builder.Property(t => t.TwitterLink)
                .HasMaxLength(255);

            builder.Property(t => t.LinkedInLink)
                .HasMaxLength(255);

            builder.Property(t => t.InstagramLink)
                .HasMaxLength(255);

            builder.Property(t => t.YouTubeLink)
                .HasMaxLength(255);
        }
    }
}
