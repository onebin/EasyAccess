using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAccess.Infrastructure.Util.CustomTimestamp
{
    public class CustomDbParameter
    {
        public CustomDbParameter() { }

        public CustomDbParameter(string parameterName, string value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public string ParameterName { get; set; }


        public string Value { get; set; }
    }
}
