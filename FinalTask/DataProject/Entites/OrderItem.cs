using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataProject.Entites
{
    [Table("OrderItem", Schema = "dbo")]
    public class OrderItem : BaseModel
    {
        public int OrderId { get; set; } 
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }    
        public int Count { get; set; }
        public double TotalAmount { get; set; }
    }
    public class OrderItemConfigure : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();

            builder.Property(x => x.Count).IsRequired().HasDefaultValue(1);
            builder.Property(x => x.TotalAmount).IsRequired().HasDefaultValue(0.0);



        }
    }
}
