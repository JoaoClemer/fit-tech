using FitTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTech.Infrastructure.Context.Mappings
{
    internal class PlanMap : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PlanType)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.CreateDate)
                .IsRequired();
        }
    }
}
