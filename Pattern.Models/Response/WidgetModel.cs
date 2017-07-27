using Pattern.Models.Base;

namespace Pattern.Models.Response
{
    public class WidgetModel : EntityModelBase
    {
        public string Name { get; set; }

        #region Lookups

        public string WidgetTypeCode { get; set; }
        public string WidgetTypeCodeValue { get; set; }
        public string WidgetTypeCodeDescription { get; set; }

        #endregion
    }
}
