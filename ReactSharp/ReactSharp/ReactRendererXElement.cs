using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ReactSharp
{
    public class ReactRendererXElement : IReactRendererDOM
    {
        private XElement root;
        private long lastId = 0;
        Dictionary<long, XNode> dic = new Dictionary<long, XNode>();

        public ReactRendererXElement(XElement root)
        {
            dic[0] = root;
        }

        public void Start()
        {
        }

        public void End()
        {
        }


        protected void ApplyProps(XElement element, IEnumerable<KeyValuePair<string, object>> props)
        {
            if (props.Any())
            {
                foreach (var prop in props)
                {
                    if (prop.Value is Delegate)
                    {
                    }
                    else
                    {
                        element.SetAttributeValue(prop.Key, prop.Value?.ToString());
                    }
                }
            }
        }

        public long CreateElement(string type, IEnumerable<KeyValuePair<string, object>> props, long parentDomId,
            long beforeDomId)
        {
            var id = ++lastId;

            var element = new XElement(type);
            ApplyProps(element, props);
            ((XElement) dic[parentDomId]).Add(element);
            dic[id] = element;
            return id;
        }

        public long CreateText(string value, long parentDomId, long beforeDomId)
        {
            var id = ++lastId;

            var element = new XText(value);

            ((XElement) dic[parentDomId]).Add(element);
            
            dic[id] = element;

            return id;
        }

        public void Remove(long id)
        {
            throw new NotImplementedException();
        }

        public void SetProps(long id, Dictionary<string, object> props)
        {
            throw new NotImplementedException();
        }

        public void SetText(long id, string value)
        {
            throw new NotImplementedException();
        }
    }
}