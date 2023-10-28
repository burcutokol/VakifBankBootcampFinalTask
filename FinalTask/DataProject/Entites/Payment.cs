using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    [Table("Payment", Schema = "dbo")]
    public class Payment : BaseModel
    { 
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentType { get; set; }
        public int BillId { get; set; }
        public virtual Bill Bill {get; set; }
        public double PaidAmount { get; set; }

    }
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.PaymentId).IsRequired();
            builder.Property(x => x.PaymentType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PaidAmount).IsRequired().HasDefaultValue(0.0).HasPrecision(10, 2);

            builder.HasIndex(x => x.PaymentId);
        }
    }
}
