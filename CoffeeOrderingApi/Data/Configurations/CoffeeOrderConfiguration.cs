using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeOrderingApiWithCQRSandMediatR.Data.Configurations
{
    public class CoffeeOrderConfiguration : IEntityTypeConfiguration<CoffeeOrder>
    {
        public void Configure(EntityTypeBuilder<CoffeeOrder> builder)
        {
            builder.HasKey(o => o.Id);


            builder.Property(o => o.Status)
                .IsRequired()
                .HasMaxLength(30);
            
            builder.Property(o => o.CreatedAt)
                 .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.CoffeeOrders)
                .HasForeignKey(o => o.CustomerId);

            builder.HasMany(o => o.OrderItems)
               .WithOne(oi => oi.CoffeeOrder)
               .HasForeignKey(oi => oi.CoffeeOrderId);
                
        }
    }
}
