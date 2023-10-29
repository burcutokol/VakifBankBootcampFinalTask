using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProject.Entites
{
    [Table("Order", Schema = "dbo")]
    public class Order : BaseModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public int DealerId {  get; set; }
        public virtual Dealer Dealer { get; set; }

        public virtual List<OrderItem> Items { get; set; }
        public double TotalAmount { get; set; }
        

    }
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(x => x.OrderId).IsRequired();
            builder.HasIndex(x => x.OrderId).IsUnique();
            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.TotalAmount).IsRequired().HasPrecision(10, 2);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(50);

            builder.Property(x => x.PaymentId).IsRequired();

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired(true);

            builder.HasOne(x => x.Payment)
            .WithOne(x => x.Order)
            .HasForeignKey<Payment>().IsRequired(true);


        }
    }
}
