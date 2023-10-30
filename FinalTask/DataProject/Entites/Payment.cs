using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProject.Entites
{
    [Table("Payment", Schema = "dbo")]
    public class Payment : BaseModel
    { 
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string PaymentType { get; set; }
        public double PaidAmount { get; set; }
        public string PaymentStatus { get; set; }

    }
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.OrderId).IsRequired();

            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.PaymentType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PaidAmount).IsRequired().HasDefaultValue(0.0).HasPrecision(10, 2);
            builder.Property(x => x.PaymentStatus).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.PaymentId);
        }
    }
}
