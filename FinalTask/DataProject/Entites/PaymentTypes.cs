using BaseProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProject.Entites
{
    [Table("PaymentTypes", Schema = "dbo")]
    public class PaymentTypes : BaseModel
    {
        public string PaymentMethodName {  get; set; }
        public string Description { get; set; }
    }
    public class PaymentTypesConfiguration : IEntityTypeConfiguration<PaymentTypes>
    {
        public void Configure(EntityTypeBuilder<PaymentTypes> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.PaymentMethodName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);

        }
    }
}
