using Pattern.DataContext.Contract;
using System.Data.Entity;

namespace Pattern.DataContext.Entities.Base
{
    public partial class ContextBase : DbContext
    {
        public ContextBase(string contextName)
            : base(contextName) { }

        #region Tables

        public virtual DbSet<Widget> Widgets { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder mb)
        {

            #region Fluent Mappings

            mb.Entity<Widget>()
                .HasRequired(x => x.LU_WidgetType)
                .WithMany(x => x.Widgets)
                .HasForeignKey(x => x.WidgetTypeCode);

            #endregion

            base.OnModelCreating(mb);
        }

    }
}
