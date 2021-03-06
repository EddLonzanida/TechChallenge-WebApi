using System.Collections.Generic;
using AspNetCoreRateLimit;
using Eml.ConfigParser;
using Microsoft.Extensions.Configuration;

namespace TechChallengeAspNetCore.ApiHost.Configurations
{
    public class RateLimitsConfig : ConfigBase<List<RateLimitRule>, RateLimitsConfig>
    {
        public RateLimitsConfig(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
