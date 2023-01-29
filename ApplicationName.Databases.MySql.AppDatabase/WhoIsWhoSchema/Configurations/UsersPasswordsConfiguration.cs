using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Common.Extensions;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema.Configurations
{
    internal class UsersPasswordsConfiguration : IEntityTypeConfiguration<UserPasswordEntityModel>
    {
        public void Configure(EntityTypeBuilder<UserPasswordEntityModel> builder)
        {
            builder.ToTable(WhoIsWhoTable.UsersPasswords.GetDescriptionAttribute())
                .HasKey(e => e.UserName);

            builder.Property(e => e.UserName)
                .HasColumnName("UserName")
                .HasColumnType(MySqlDbColumnType.VarChar128.GetDescriptionAttribute())
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnName("Password")
                .HasColumnType(MySqlDbColumnType.VarChar512.GetDescriptionAttribute());

            builder.Property(e => e.ValidFrom)
                .HasColumnName("ValidFrom")
                .HasColumnType(MySqlDbColumnType.DateTime6.GetDescriptionAttribute());

            builder.Property(e => e.ValidTo)
                .HasColumnName("ValidTo")
                .HasColumnType(MySqlDbColumnType.DateTime6.GetDescriptionAttribute());
        }
    }
}
