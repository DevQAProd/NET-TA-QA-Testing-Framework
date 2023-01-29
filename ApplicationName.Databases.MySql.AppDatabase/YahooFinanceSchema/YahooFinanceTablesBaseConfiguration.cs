using ApplicationName.Common.Enumerations.Databases;
using ApplicationName.Common.Extensions;
using ApplicationName.ModelsAndFacilities.Models.EntityModels.YahooFinanceSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicationName.Databases.MySql.AppDatabase.YahooFinanceSchema
{
    internal class YahooFinanceTablesBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : QuoteEntityModel
    {
        private readonly YahooFinanceTable _table;
        private readonly MySqlDbColumnType _pricesColumnType;

        public YahooFinanceTablesBaseConfiguration(YahooFinanceTable table, MySqlDbColumnType pricesColumnType)
        {
            _table = table;
            _pricesColumnType = pricesColumnType;
        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(_table.GetDescriptionAttribute())
                .HasKey(e => e.Date);

            builder.Property(e => e.Date)
                .HasColumnName("Date")
                .HasColumnType(MySqlDbColumnType.DateTime.GetDescriptionAttribute())
                .IsRequired();

            builder.Property(e => e.Open)
                .HasColumnName("Open")
                .HasColumnType(_pricesColumnType.GetDescriptionAttribute());

            builder.Property(e => e.High)
                .HasColumnName("High")
                .HasColumnType(_pricesColumnType.GetDescriptionAttribute());

            builder.Property(e => e.Low)
                .HasColumnName("Low")
                .HasColumnType(_pricesColumnType.GetDescriptionAttribute());

            builder.Property(e => e.Close)
                .HasColumnName("Close")
                .HasColumnType(_pricesColumnType.GetDescriptionAttribute());

            builder.Property(e => e.AdjClose)
                .HasColumnName("AdjClose")
                .HasColumnType(_pricesColumnType.GetDescriptionAttribute());

            builder.Property(e => e.Volume)
                .HasColumnName("Volume")
                .HasColumnType(MySqlDbColumnType.BigInt.GetDescriptionAttribute());
        }
    }
}
