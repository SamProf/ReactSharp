using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactSharp
{
    public class ReactElementTemplate
    {
        public ReactElementTemplateType Type { get; set; }
        public List<ReactElementTemplateProp> Props { get; set; }
        public List<ReactElementTemplateChild> Children { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            ToString(sb, "");
            return sb.ToString();
        }

        public void ToString(StringBuilder sb, string level)
        {
            sb.Append(level);
            sb.Append(Type.Value);
            foreach (var prop in Props)
            {
                sb.Append($" {prop.Name}={prop.Value}");
            }

            sb.AppendLine();
            var childLevel = level + "   ";
            foreach (var child in Children)
            {
                if (child.Value is ReactElementTemplate t)
                {
                    t.ToString(sb, childLevel);
                }
                else
                {
                    sb.Append(childLevel);
                    sb.Append($"\"{child.Value}\"");
                    sb.AppendLine();
                }
            }
        }
    }


    public class ReactElementTemplateProp
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ReactValueEvaluator Evaluator { get; set; }
    }

    public class ReactElementTemplateType
    {
        public string Value { get; set; }
        public ReactValueEvaluator Evaluator { get; set; }
    }

    public class ReactElementTemplateChild
    {
        public object Value { get; set; }
        public ReactValueEvaluator Evaluator { get; set; }
    }
}