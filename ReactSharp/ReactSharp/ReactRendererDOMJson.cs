using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ReactSharp
{
    public class ReactRendererDOMJson : IReactRendererDOM
    {
        private JsonTextWriter writer;
        private long lastId = 0;

        public ReactRendererDOMJson(TextWriter textWriter)
        {
            this.writer = new JsonTextWriter(textWriter);
            // writer.Formatting = Formatting.Indented;
        }

        public void Start()
        {
            writer.WriteStartObject();
            writer.WritePropertyName("c");
            writer.WriteStartArray();
        }

        public void End()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        public long CreateElement(string type, IEnumerable<KeyValuePair<string, object>> props, long parentDomId,
            long beforeDomId)
        {
            var id = ++lastId;

            writer.WriteStartObject();

            writer.WritePropertyName("c");
            writer.WriteValue(1);

            writer.WritePropertyName("t");
            writer.WriteValue(type);

            writer.WritePropertyName("i");
            writer.WriteValue(id);

            if (parentDomId != 0)
            {
                writer.WritePropertyName("p");
                writer.WriteValue(parentDomId);
            }

            // if (afterDomId != 0)
            // {
            //     writer.WritePropertyName("n");
            //     writer.WriteValue(afterDomId);
            // }

            if (props.Any())
            {
                writer.WritePropertyName("a");
                writer.WriteStartObject();
                foreach (var prop in props)
                {
                    writer.WritePropertyName(prop.Key);
                    writer.WriteValue(prop.Value?.ToString());
                }

                writer.WriteEndObject();
            }

            writer.WriteEndObject();

            return id;
        }

        public long CreateText(string value, long parentDomId)
        {
            var id = ++lastId;

            writer.WriteStartObject();

            writer.WritePropertyName("c");
            writer.WriteValue(2);

            writer.WritePropertyName("v");
            writer.WriteValue(value);

            writer.WritePropertyName("i");
            writer.WriteValue(id);

            writer.WritePropertyName("p");
            writer.WriteValue(parentDomId);

            // writer.WritePropertyName("n");
            // writer.WriteValue(afterDomId);

            writer.WriteEndObject();

            return id;
        }

        public void Remove(long id)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("c");
            writer.WriteValue(3);

            writer.WritePropertyName("i");
            writer.WriteValue(id);

            writer.WriteEndObject();
        }

        public void SetProps(long id, Dictionary<string, object> props)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("c");
            writer.WriteValue(4);

            writer.WritePropertyName("i");
            writer.WriteValue(id);

            if (props.Any())
            {
                writer.WritePropertyName("a");
                writer.WriteStartObject();
                foreach (var prop in props)
                {
                    writer.WritePropertyName(prop.Key);
                    writer.WriteValue(prop.Value?.ToString());
                }

                writer.WriteEndObject();
            }

            writer.WriteEndObject();
        }

        public void SetText(long id, string value)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("c");
            writer.WriteValue(5);

            writer.WritePropertyName("v");
            writer.WriteValue(value);

            writer.WritePropertyName("i");
            writer.WriteValue(id);
            writer.WriteEndObject();
        }
    }
}