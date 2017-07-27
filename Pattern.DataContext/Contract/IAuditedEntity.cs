using System;

namespace Pattern.DataContext.Contract
{
    public interface IAuditedEntity
    {
        string CreatedBy { get; set; }
        DateTime Created { get; set; }
        Guid CreatedById { get; set; }
        string LastUpdatedBy { get; set; }
        DateTime LastUpdated { get; set; }
        Guid LastUpdatedById { get; set; }
        int ModificationNumber { get; set; }
    }
}
