namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    public class CustomDbParameter
    {
        public CustomDbParameter() { }

        public CustomDbParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public string ParameterName { get; set; }

        public object Value { get; set; }
    }
}
