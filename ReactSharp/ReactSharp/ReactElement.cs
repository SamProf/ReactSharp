using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ReactSharp
{
    public class ReactElement
    {
        public object Type { get; private set; }
        public IDictionary<string, object> Props { get; private set; }

        protected void Init(object type, IDictionary<string, object> attributes, object[] children)
        {
            this.Type = type;
            this.Props = attributes ?? new Dictionary<string, object>();
            this.Props["Children"] = children;
        }

        public ReactElement(object type, IDictionary<string, object> attributes, params object[] children)
        {
            Init(type, attributes, children);
        }


        public ReactElement(FormattableString templateString) : this(
            ReactElementTemplateParser.Get(templateString), templateString.GetArguments())
        {
        }

        public ReactElement(ReactElementTemplate template, object[] data)
        {
            Init(template.Type.Evaluator(data),
                template.Props.ToDictionary(i => i.Name, i => i.Evaluator(data)),
                template.Children.Select(i => i.Evaluator(data)).ToArray());
        }
    }
}