using System;
using System.Threading.Tasks;

namespace ReactSharp
{
    public class ReactRuntime
    {
        public ReactNode Root { get; set; }
        private object synObj = new object();
        private ReactRenderer _renderer;


        public Action Step { get; set; }


        protected void RunStep()
        {
            lock (synObj)
            {
                Step?.Invoke();
            }
        }

        public void Start()
        {
            RunStep();
        }
        
        
        


        protected ReactNode FindNode(long id, ReactNode node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.DomId == id)
            {
                return node;
            }

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var f = FindNode(id, child);
                    if (f != null)
                    {
                        return f;
                    }
                }
            }

            return null;
        }

        
        public void HandleEvent(long id, string eventName)
        {
            var node = FindNode(id, Root);
            var d = node.Props.GetOrDefault<Action>(eventName);
            d?.Invoke();
            RunStep();
        }

        public ReactRuntime()
        {
            Root = null;
        }

        public void NodeSetState(ReactNode node, Action action)
        {
            action();
            RunStep();
        }
    }
}