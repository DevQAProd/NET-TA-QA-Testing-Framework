namespace ApplicationName.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddOrReplace<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 value)
        {
            if (dict != null && key != null)
            {
                if (dict.ContainsKey(key))
                    dict[key] = value;
                else
                    dict.Add(key, value);
            }
        }

        public static void AddOrReplace<T1, T2>(this Dictionary<T1, T2> dict, params KeyValuePair<T1, T2>[] keyValuePairs)
        {
            if (dict != null && keyValuePairs?.Length > 0)
            {
                foreach (var keyValuePair in keyValuePairs)
                    dict.AddOrReplace(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}
