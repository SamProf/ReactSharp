namespace ReactSharp
{
    public delegate object ReactValueEvaluator(object[] data);


    public static class ReactValueEvaluatorFactory
    {
        public static ReactValueEvaluator CreateValue(object value)
        {
            return (data) => value;
        }

        public static ReactValueEvaluator CreateByIndex(int index)
        {
            return data => data[index];
        }

        public static ReactValueEvaluator CreateByFormat(string format)
        {
            return data => string.Format(format, data);
        }

        public static ReactValueEvaluator CreateFromTemplate(ReactElementTemplate template)
        {
            return data => new ReactElement(template, data);
        }
    }
}