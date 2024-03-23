using FitTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitTech.Infrastructure.Context.Mappings
{
    public class StudentPlanMap : IEntityTypeConfiguration<StudentPlan>
    {        
        public void Configure(EntityTypeBuilder<StudentPlan> builder)
        {
            builder.Property(x => x.IsActive)
                 .IsRequired();
            builder.Property(x => x.ExpirationDate)
                    .IsRequired();
        }
    }
}
