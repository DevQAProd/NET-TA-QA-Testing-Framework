using Newtonsoft.Json;

namespace ApplicationName.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static T CloneJson<T>(this object @object) where T : class
        {
            if (@object == null)
                return null;

            try
            {
                var sourceObject = JsonConvert.SerializeObject(@object);
                T destinationObject = JsonConvert.DeserializeObject<T>(sourceObject);

                return destinationObject;
            }
            catch
            {
                return null;
            }
        }
    }
}
