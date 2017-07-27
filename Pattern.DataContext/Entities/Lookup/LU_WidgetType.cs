using Pattern.DataContext.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pattern.DataContext.Entities.Lookup
{
    [Table("Schema.LU_WidgetType")]
    public class LU_WidgetType : LookupBase
    {
        public virtual ICollection<Widget> Widgets { get; set; }
    }
}
