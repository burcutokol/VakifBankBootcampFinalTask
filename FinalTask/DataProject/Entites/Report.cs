using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    [Table("Report", Schema = "dbo")]
    public class Report : BaseModel
    {
        public int DealerId { get; set; }
        public virtual Dealer Dealer{get; set;}
        public string Type { get; set; }
        public string Description { get; set; }

    }
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasIndex(x => x.DealerId);

            builder.Property(x => x.Type).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(1000);
        }
    }
}
