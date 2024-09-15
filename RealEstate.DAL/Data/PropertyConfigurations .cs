using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.PL.ViewModels;

namespace RealEstate.DAL.Data
{
    public class PropertyConfigurations : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(255); 

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ZipCode)
                .IsRequired()
                .HasMaxLength(20); 

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); 

            builder.Property(p => p.SquareFootage)
                .IsRequired();

            builder.Property(p => p.Bedrooms)
                .IsRequired();

            builder.Property(p => p.Bathrooms)
                .IsRequired();

            builder.Property(p => p.PropertyType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ListedDate)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("text");

            builder.Property(p => p.Status)
                .HasMaxLength(50); 

            builder.Property(p => p.HasGarage)
                .IsRequired();

            builder.Property(p => p.HasPool)
                .IsRequired();

            builder.Property(p => p.IsFurnished)
                .IsRequired();

            builder.Property(p => p.AgentName)
                .HasMaxLength(100);

            builder.Property(p => p.AgentEmail)
                .HasMaxLength(150); 

            builder.Property(p => p.AgentPhone)
                .HasMaxLength(20); 

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(p => p.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()"); 

            builder.Property(p => p.UpdatedDate)
                .IsRequired(false);

            builder.Property(p => p.IpAddress)
                .HasMaxLength(45); 
        }
    }
}
