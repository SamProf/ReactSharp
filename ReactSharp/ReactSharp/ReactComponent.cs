using System;
using System.Collections.Generic;

namespace ReactSharp
{
    public abstract class ReactComponent
    {
        internal ReactNode Node { get; set; }
        protected IDictionary<string, object> Props { get; set; }


        protected void SetState(Action action)
        {
            Node.SetState(action);
        }

        public abstract object Render();

        public virtual void ComponentWillMount()
        {
        }

        public virtual void SetProps(IDictionary<string, object> props)
        {
            this.Props = props;
        }
    }
}