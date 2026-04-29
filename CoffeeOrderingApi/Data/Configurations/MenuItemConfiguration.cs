using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeOrderingApiWithCQRSandMediatR.Data.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
           builder.HasKey(m => m.Id);

              builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Category)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(m => m.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.Property(m=> m.IsAvailable)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(m => m.OrderItems)
                .WithOne(oi => oi.MenuItem)
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
