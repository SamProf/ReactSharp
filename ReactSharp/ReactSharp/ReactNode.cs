using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReactSharp
{
    public class ReactNode
    {
        public object Data { get; set; }
        public long DomId { get; set; }
        public ReactComponent Component { get; set; }

        public IDictionary<string, object> Props { get; set; }

        public List<ReactNode> Children { get; set; }
    }
}