using System.Collections;
using System.Collections.Generic;

namespace ReactSharp
{
    public static class DictionaryExtension
    {
        public static T GetOrDefault<T>(this IDictionary<string, object> dic, string key, T def = default(T))
        {
            object v;
            if (!dic.TryGetValue(key, out v))
            {
                v = def;
            }

            return (T) v;
        }
    }
}