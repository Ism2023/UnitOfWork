using System;

namespace Pattern.Models.Base
{
    public class EntityModelBase
    {
        public Guid? GUID { get; set; } = Guid.NewGuid();
        public int? UID { get; set; }
        public int? Ident
        {
            get
            {
                return UID;
            }
        }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid SystemIdent { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
