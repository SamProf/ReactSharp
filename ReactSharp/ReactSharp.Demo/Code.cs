namespace ReactSharp.Demo
{
    public class Code : ReactComponent
    {
        public override object Render()
        {
            return new ReactElement(
                $"<div style='white-space: pre-wrap; background: darkslategray; color: white; font-family: monospace;'>{Props.GetOrDefault<string>("Code")?.Trim()}</div>");
        }
    }
}