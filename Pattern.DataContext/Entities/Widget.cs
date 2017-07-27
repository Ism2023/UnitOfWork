using Pattern.DataContext.Attributes;
using Pattern.DataContext.Entities.Base;
using Pattern.DataContext.Entities.Lookup;
using Pattern.DataContext.Functions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pattern.DataContext.Entities
{
    [Table("Schema.Widget")]
    public class Widget : EntityBase, IEquatable<Widget>
    {
        #region Equatable

        private static readonly Equater<Widget> Eq;

        static Widget()
        {
            Eq = new Equater<Widget>();
            Eq.AddEquaterFunc(x => x.Name);
            Eq.AddEquaterFunc(x => x.WidgetTypeCode);
        }

        public bool Equals(Widget other)
        {
            return Eq.Equals(this, other);
        }
        public override int GetHashCode()
        {
            return Eq.GetHashCode();
        }

        #endregion

        [Cloneable]
        public string Name { get; set; }

        #region Lookups

        [Cloneable]
        public int WidgetTypeCode { get; set; }
        public virtual LU_WidgetType LU_WidgetType { get; set; }

        #endregion
    }
}
