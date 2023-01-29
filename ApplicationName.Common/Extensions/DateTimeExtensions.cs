namespace ApplicationName.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToMySqlDbDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static DateTime ToMySqlDbDateTime(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToMySqlDbDateTimeString());
        }

        public static string ToMySqlDbDateTimeWith3FractionalPointsString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }

        public static DateTime ToMySqlDbDateTimeWith3FractionalPoints(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToMySqlDbDateTimeWith3FractionalPointsString());
        }

        public static string ToMySqlDbDateTimeWith6FractionalPointsString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffff");
        }

        public static DateTime ToMySqlDbDateTimeWith6FractionalPoints(this DateTime dateTime)
        {
            return DateTime.Parse(dateTime.ToMySqlDbDateTimeWith6FractionalPointsString());
        }
    }
}
