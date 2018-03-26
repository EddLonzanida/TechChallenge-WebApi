using System.Collections.Generic;
using Eml.ConfigParser;
using Microsoft.Extensions.Configuration;

namespace TechChallengeAspNetCore.ApiHost.Configurations
{
    public class WhiteListConfig : ConfigBase<List<string>, WhiteListConfig>
    {
        public WhiteListConfig(IConfiguration configuration) : base(configuration)
        {
        }
    }
}

