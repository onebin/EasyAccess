using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class FormConfig : AggregateRootBase<FormConfig, int>
    {
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Memo { get; set; }

        public virtual List<DataCollection> DataCollections { get; set; }
    }
}
