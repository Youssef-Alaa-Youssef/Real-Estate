using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.DAL.Models.Home;
using System;

namespace RealEstate.DAL.Data
{
    public class ContactConfigurations : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Message)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()"); 

            builder.Property(c => c.UpdatedDate)
                .IsRequired(false);

            builder.Property(c => c.IpAddress)
                .HasMaxLength(45); 
        }
    }
}
