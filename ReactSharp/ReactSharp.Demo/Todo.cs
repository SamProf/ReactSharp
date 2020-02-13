using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactSharp.Demo
{
    public class Todo : ReactComponent
    {
        List<string> items = Enumerable.Range(0, 10).Select(i => i.ToString()).ToList();

        void Add()
        {
            SetState(() => { this.items.Add(Guid.NewGuid().ToString()); });
        }


        void Remove(string item)
        {
            SetState(() => { this.items.Remove(item); });
        }


        public override object Render()
        {
            return new ReactElement($@"
<div>
    <h4>Todo: {items.Count}</h4>
    <p>
        <button type='button' class='btn btn-primary' onclick='{new Action((Add))}'>Add item</button>
    </p>
    {items.Select(i => new ReactElement($"<div>Task - {i} <button onclick='{new Action(() => Remove(i))}'>X</button></div>"))}
   
</div>
");
        }
    }
}