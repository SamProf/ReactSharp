namespace ReactSharp.Components
{
    public class ReactFragment : ReactComponent
    {
        public override object Render()
        {
            return Props["Children"];
        }
    }


    public class Fragment : ReactFragment
    {
    }
}