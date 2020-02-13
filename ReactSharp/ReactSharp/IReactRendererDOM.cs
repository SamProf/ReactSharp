using System.Collections.Generic;

namespace ReactSharp
{
    public interface IReactRendererDOM
    {
        void Start();


        void End();


        long CreateElement(string type, IEnumerable<KeyValuePair<string, object>> props, long parentDomId, long beforeDomId);
        long CreateText(string value, long parentDomId);
        void Remove(long domId);
        void SetProps(long id, Dictionary<string,object> props);
        void SetText(long id, string value);
    }
}