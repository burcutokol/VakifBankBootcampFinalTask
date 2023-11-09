using BaseProject.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProject.Entites
{
    [Table("Message", Schema = "dbo")]
    public class Message : BaseModel
    {
        public int SenderId { get; set; }
        public int RecevierId { get; set; }
        public string MessageContent { get; set; }

    }
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.MessageContent).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.RecevierId).IsRequired();
        }
    }
}
