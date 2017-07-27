using Pattern.Common.Contract;
using System;
using System.Configuration;

namespace Pattern.Common.Implementation
{
    public class SystemSettings : ISystemSettings
    {
        Guid ISystemSettings.SystemIdent
        {
            get
            {
                return Guid.Parse(ConfigurationManager.AppSettings["SystemIdentifier"]);
            }
        }

        public string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}
