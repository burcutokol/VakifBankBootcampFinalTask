using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProject.Entites
{
    [Table("Bill", Schema = "dbo")]
    public class Bill : BaseModel
    {
        public int BillId { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public double TotalAmount { get; set; }

    }
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.BillId).IsRequired();
            builder.HasIndex(x => x.BillId).IsUnique(true);
            builder.Property(x => x.TotalAmount).IsRequired().HasDefaultValue(0.0);

            builder.HasIndex(x => x.PaymentId);
        }
    }
}
