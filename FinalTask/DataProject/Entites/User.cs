using System;
using System.ComponentModel.DataAnnotations.Schema;
using BaseProject.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProject.Entites
{
    [Table("User", Schema = "dbo")]
    public class User : BaseModel
    {
        public int UserLoginId { get; set; }
        public string UserName { get; set; }    
        public string Password { get; set; }
        public string Email { get; set; }

        public string? Role { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int PasswordRetryCount { get; set; }
        public int Status { get; set; } 
        public Dealer Dealer { get; set; }
        
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.UserLoginId).IsRequired();
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Role).IsRequired().HasMaxLength(10);
            builder.Property(x => x.LastActivityDate).IsRequired(); //TODO : defaultvalue
            builder.Property(x => x.PasswordRetryCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Status).IsRequired().HasDefaultValue(1); //locked => 0 
                                                                                //unlocked => 1
        }
    }
}
