using System.ComponentModel.DataAnnotations;
using EasyAccess.Infrastructure.Entity;

namespace Demo.Model.EDMs
{
    public class DataCollection : AggregateBase<int>
    {
        public virtual FormConfig Subject { get; set; }

        public virtual SectionConfig Section { get; set; }

        public int GroupId { get; set; }

        [MaxLength(255)]
        public string Value { get; set; }
    }
}
