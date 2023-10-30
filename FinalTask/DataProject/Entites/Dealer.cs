using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataProject.Entites
{
    [Table("Dealer", Schema = "dbo")]
    public class Dealer : BaseModel
    {
        public int DealerId { get; set; }
        public int UserLoginId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double ProfitMargin { get; set; }
        public double Limit { get; set; }
        public virtual User User { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Report> Reports { get; set; }
        public virtual List<Bill> Bills { get; set; }
    }
    public class DealerConfiguration : IEntityTypeConfiguration<Dealer>
    {
        public void Configure(EntityTypeBuilder<Dealer> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.DealerId).IsRequired();
            builder.HasIndex(x => x.DealerId).IsUnique(true);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ProfitMargin).IsRequired().HasPrecision(10, 2).HasDefaultValue(0.0);
            builder.Property(x => x.Limit).IsRequired().HasPrecision(10, 2).HasDefaultValue(0.0);


            builder.HasMany(x => x.Reports)
                .WithOne(x => x.Dealer)
                .HasForeignKey(x => x.DealerId)
                .IsRequired(false);
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Dealer)
                .HasForeignKey(x => x.DealerId)
                .IsRequired(false);
            builder.HasMany(x => x.Bills)
                .WithOne(x => x.Dealer)
                .HasForeignKey(x => x.DealerId)
            .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithOne(x => x.Dealer)
                .HasForeignKey<Dealer>()
                .IsRequired();
        }

    }
}

