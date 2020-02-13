using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReactSharp
{
    public class ReactNode
    {
        private ReactNode()
        {
        }


        public static ReactNode Create(ReactRuntime runtime)
        {
            return new ReactNode()
            {
                Runtime = runtime,
            };
        }

        public ReactRuntime Runtime { get; set; }
        public object Data { get; set; }
        public long DomId { get; set; }
        public ReactComponent Component { get; set; }

        public IDictionary<string, object> Props { get; set; }

        public List<ReactNode> Children { get; set; }

        public void SetState(Action action)
        {
            Runtime.NodeSetState(this, action);
        }
    }
}