using System.ComponentModel;

namespace ApplicationName.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescriptionAttribute(this Enum @enum)
        {
            var member = @enum.GetType().GetMember(@enum.ToString())[0];
            var descriptionAttributes = member?.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

            if (descriptionAttributes?.Length > 0)
                return (descriptionAttributes.ElementAt(0) as DescriptionAttribute).Description;

            return @enum.ToString();
        }
    }
}
