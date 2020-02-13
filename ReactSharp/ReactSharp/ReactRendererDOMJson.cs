using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ReactSharp
{
    public class ReactRendererDOMJson : IReactRendererDOM
    {
        public JsonTextWriter Writer { get; private set; }
        public StringWriter StringWriter { get; private set; }
        private long lastId = 0;

        

        public ReactRendererDOMJson()
        {
            // writer.Formatting = Formatting.Indented;
        }

        public void Start()
        {
            StringWriter = new StringWriter();
            Writer = new JsonTextWriter(StringWriter);
            
            Writer.WriteStartObject();
            Writer.WritePropertyName("c");
            Writer.WriteStartArray();
        }

        public void End()
        {
            Writer.WriteEndArray();
            Writer.WriteEndObject();
        }



        protected void ApplyProps(IEnumerable<KeyValuePair<string, object>> props)
        {
            if (props.Any())
            {
                Writer.WritePropertyName("a");
                Writer.WriteStartObject();
                foreach (var prop in props)
                {
                    Writer.WritePropertyName(prop.Key);
                    if (prop.Value is Delegate)
                    {
                        Writer.WriteStartObject();
                        Writer.WritePropertyName("e");
                        Writer.WriteValue(1);
                        Writer.WriteEndObject();
                    }
                    else
                    {
                        Writer.WriteValue(prop.Value?.ToString());
                    }
                    
                }

                Writer.WriteEndObject();
            }
        }
        
        public long CreateElement(string type, IEnumerable<KeyValuePair<string, object>> props, long parentDomId,
            long beforeDomId)
        {
            var id = ++lastId;

            Writer.WriteStartObject();

            Writer.WritePropertyName("c");
            Writer.WriteValue(1);

            Writer.WritePropertyName("t");
            Writer.WriteValue(type);

            Writer.WritePropertyName("i");
            Writer.WriteValue(id);

            if (parentDomId != 0)
            {
                Writer.WritePropertyName("p");
                Writer.WriteValue(parentDomId);
            }
            
            if (beforeDomId != 0)
            {
                Writer.WritePropertyName("b");
                Writer.WriteValue(beforeDomId);
            }

            // if (afterDomId != 0)
            // {
            //     writer.WritePropertyName("n");
            //     writer.WriteValue(afterDomId);
            // }

            ApplyProps(props);

            Writer.WriteEndObject();

            return id;
        }

        public long CreateText(string value, long parentDomId, long beforeDomId)
        {
            var id = ++lastId;

            Writer.WriteStartObject();

            Writer.WritePropertyName("c");
            Writer.WriteValue(2);

            Writer.WritePropertyName("v");
            Writer.WriteValue(value);

            Writer.WritePropertyName("i");
            Writer.WriteValue(id);

            if (parentDomId != 0)
            {
                Writer.WritePropertyName("p");
                Writer.WriteValue(parentDomId);
            }
            
            if (beforeDomId != 0)
            {
                Writer.WritePropertyName("b");
                Writer.WriteValue(beforeDomId);
            }

            // writer.WritePropertyName("n");
            // writer.WriteValue(afterDomId);

            Writer.WriteEndObject();

            return id;
        }

        public void Remove(long id)
        {
            Writer.WriteStartObject();

            Writer.WritePropertyName("c");
            Writer.WriteValue(3);

            Writer.WritePropertyName("i");
            Writer.WriteValue(id);

            Writer.WriteEndObject();
        }

        public void SetProps(long id, Dictionary<string, object> props)
        {
            Writer.WriteStartObject();

            Writer.WritePropertyName("c");
            Writer.WriteValue(4);

            Writer.WritePropertyName("i");
            Writer.WriteValue(id);

            ApplyProps(props);

            Writer.WriteEndObject();
        }

        public void SetText(long id, string value)
        {
            Writer.WriteStartObject();

            Writer.WritePropertyName("c");
            Writer.WriteValue(5);

            Writer.WritePropertyName("v");
            Writer.WriteValue(value);

            Writer.WritePropertyName("i");
            Writer.WriteValue(id);
            Writer.WriteEndObject();
        }
    }
}