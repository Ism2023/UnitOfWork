using Pattern.DataContext.Contract;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pattern.DataContext.Entities.Base
{
    public class EntityBase : IAuditedEntity
    {
        [Key]
        public int UID { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();

        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public Guid CreatedById { get; set; }

        public DateTime LastUpdated { get; set; }
        [Required]
        public Guid LastUpdatedById { get; set; }
        [Required]
        [StringLength(50)]
        public string LastUpdatedBy { get; set; }

        public int ModificationNumber { get; set; } = 0;

        public DateTime? Deleted { get; set; }
        [StringLength(50)]
        public string DeletedBy { get; set; }
        public Guid? DeletedById { get; set; }
    }
}
