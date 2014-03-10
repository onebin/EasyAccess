using System.Collections.Generic;
using EasyAccess.Infrastructure.Util.EnumDescription;

namespace EasyAccess.Infrastructure.Util.EasyUi
{
    public class EasyUiCombobox
    {
        private readonly ICollection<Dictionary<string, object>> _options = new List<Dictionary<string, object>>();

        public EasyUiCombobox SetOptions<TKey, TVal>(Dictionary<TKey, TVal> options)
        {
            foreach (var option in options)
            {
                var item = new Dictionary<string, object>
                {
                    { "text", option.Key.ToString() },
                    { "value", option.Value.ToString() }
                };
                _options.Add(item);
            }
            return this;
        }

        public EasyUiCombobox SetOptions(IEnumDescription[] options)
        {
            foreach (var option in options)
            {
                var item = new Dictionary<string, object>
                {
                    { "text", option.Description },
                    { "value", option.Value }
                };
                _options.Add(item);
            }
            return this;
        }

        public object GetSerializableObj()
        {
            return new {  _options };
        }
    }
}
