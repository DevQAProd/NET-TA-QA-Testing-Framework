using System.ComponentModel;

namespace ApplicationName.Common.Enumerations.Databases
{
    public enum MySqlDbColumnType
    {
        [Description("bigint")]
        BigInt,
        [Description("int(11)")]
        Int11,
        [Description("datetime")]
        DateTime,
        [Description("datetime(6)")]
        DateTime6,
        [Description("decimal(19,9)")]
        Decimal19_9,
        [Description("varchar(128)")]
        VarChar128,
        [Description("varchar(512)")]
        VarChar512
    }
}
