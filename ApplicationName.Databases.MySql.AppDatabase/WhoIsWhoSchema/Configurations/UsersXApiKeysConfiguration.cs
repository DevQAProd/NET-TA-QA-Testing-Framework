using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Common.Extensions;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.WhoIsWhoSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationName.Databases.MySql.AppDatabase.WhoIsWhoSchema.Configurations
{
    internal class UsersXApiKeysConfiguration : IEntityTypeConfiguration<UserXApiKeyEntityModel>
    {
        public void Configure(EntityTypeBuilder<UserXApiKeyEntityModel> builder)
        {
            builder.ToTable(WhoIsWhoTable.UsersXApiKeys.GetDescriptionAttribute())
                .HasKey(e => e.UserName);

            builder.Property(e => e.UserName)
                .HasColumnName("UserName")
                .HasColumnType(MySqlDbColumnType.VarChar128.GetDescriptionAttribute())
                .IsRequired();

            builder.Property(e => e.XApiKey)
                .HasColumnName("XApiKey")
                .HasColumnType(MySqlDbColumnType.VarChar512.GetDescriptionAttribute());

            builder.HasIndex(x => x.XApiKey)
                .HasDatabaseName("UX_Users_X_Api_Keys_XApiKey")
                .IsUnique();

            builder.Property(e => e.ValidFrom)
                .HasColumnName("ValidFrom")
                .HasColumnType(MySqlDbColumnType.DateTime6.GetDescriptionAttribute());

            builder.Property(e => e.ValidTo)
                .HasColumnName("ValidTo")
                .HasColumnType(MySqlDbColumnType.DateTime6.GetDescriptionAttribute());

            builder.HasOne(y => y.UserPasswordEntityModel)
                .WithMany()
                .HasForeignKey(z => z.UserName);
        }
    }
}
