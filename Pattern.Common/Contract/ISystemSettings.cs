using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern.Common.Contract
{
    public interface ISystemSettings
    {
        string GetConnectionString(string key);
        string Get(string key);
        Guid SystemIdent { get; }
    }
}
