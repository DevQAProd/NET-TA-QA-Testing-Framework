using System.ComponentModel;

namespace ApplicationName.Common.Enumerations.Files
{
    public enum FileExtension
    {
        [Description(".csv")]
        Csv,
        [Description(".xls")]
        Xls,
        [Description(".xlsx")]
        Xlsx
    }
}
