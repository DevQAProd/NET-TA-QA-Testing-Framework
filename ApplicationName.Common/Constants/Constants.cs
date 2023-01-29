namespace ApplicationName.Common.Constants
{
    public class Constants
    {
        public const string ReauthenticationBoundaryName = "Reauthentication Boundary";
        public static readonly TimeSpan ReauthenticationBoundaryTimespan = new TimeSpan(days: 0, hours: 0, minutes: 5, seconds: 0);
        public const string XApiKeyName = "X-API-Key";
    }
}
