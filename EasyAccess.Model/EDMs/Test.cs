using System;
using System.ComponentModel.DataAnnotations.Schema;
using EasyAccess.Infrastructure.Entity;
using EasyAccess.Infrastructure.Util.CustomTimestamp;
using EasyAccess.Model.VOs;

namespace EasyAccess.Model.EDMs
{
    public class Test : AggregateRootBase<Test, long>
    {
        public int NonNullableInt { get; set; }
        public decimal NonNullableDecimal { get; set; }
        public float NonNullableFloat { get; set; }
        public double NonNullableDouble { get; set; }
        public byte NonNullableByte { get; set; }
        public string NonNullableString { get; set; }
        public DateTime NonNullableDateTime { get; set; }
        public Sex NonNullableSexEnum { get; set; }

        public int? NullableInt { get; set; }
        public decimal? NullableDecimal { get; set; }
        public float? NullableFloat { get; set; }
        public double? NullableDouble { get; set; }
        public byte? NullableByte { get; set; }
        public Sex? NullableSexEnum { get; set; }
        public DateTime? NullableDateTime { get; set; }


        [Column("_RowVersion")]
        [CustomTimestamp(CustomTimestampUpdateMode.GreaterThan)]
        public int RowVersion { get; set; }
    }
}
