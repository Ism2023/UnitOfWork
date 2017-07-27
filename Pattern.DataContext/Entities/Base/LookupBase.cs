using System;
using System.ComponentModel.DataAnnotations;

namespace Pattern.DataContext.Entities.Base
{
    public class LookupBase
    {
        [Key]
        public int LUCode { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();

        public DateTime Created { get; set; } = DateTime.Now;
        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }

        public DateTime LastUpdated { get; set; }

        [Required]
        [StringLength(30)]
        public string LastUpdatedBy { get; set; }

        public int ModificationNumber { get; set; } = 0;

        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}
