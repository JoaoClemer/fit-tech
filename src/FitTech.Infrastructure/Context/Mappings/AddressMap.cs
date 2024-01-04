using FitTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTech.Infrastructure.Context.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Street)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.PostalCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.State)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(x => x.Country)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();
        }
    }
}
