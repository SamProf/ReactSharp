using System;
using System.Collections.Generic;
using System.Text;

namespace ReactSharp.Demo
{
    public class Counter : ReactComponent
    {
        private int value = 0;

        void Increment()
        {
            SetState(() => { value++; });
        }

        void Decrement()
        {
            SetState(() => { value--; });
        }


        public override object Render()
        {
            return new ReactElement($@"
<div>
    <h4>Counter value: {value}</h4>
    <p>
        <button type='button' class='btn btn-primary' onclick='{new Action(Increment)}'>Increment</button>
        <button type='button' class='btn btn-primary' onclick='{new Action((Decrement))}'>Decrement</button>
    </p>
</div>
");
        }
    }
}