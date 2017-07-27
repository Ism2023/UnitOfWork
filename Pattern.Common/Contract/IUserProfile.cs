using System;

namespace Pattern.Common.Contract
{
    public interface IUserProfile
    {
        string UserName { get; }
        Guid UserId { get; }
    }
}
