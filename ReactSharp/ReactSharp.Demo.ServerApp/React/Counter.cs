using System;
using System.Linq;

namespace ReactSharp.Demo.ServerApp.React
{
    public class Counter : ReactComponent
    {
        private int value = 0;
        private int[] items = Enumerable.Range(0, 10).ToArray();


        public override object Render()
        {
            value++;
            return new ReactElement($@"
<Fragment>
    <h4>Counter</h4>
    <p>Counter: {value}</p>    
</Fragment>
");
        }
    }
}