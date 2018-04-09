using Eml.ConfigParser;
using Eml.ConfigParser.Parsers;
using Microsoft.Extensions.Configuration;

namespace TechChallengeAspNetCore.Contracts.Configurations
{
    public class TechChallengeAspNetCoreDbConnectionString : ConfigBase<string, TechChallengeAspNetCoreDbConnectionString>
    {
        public TechChallengeAspNetCoreDbConnectionString(IConfiguration configuration, IConfigParser customParser = null)
            : base(configuration, customParser)
        {
        }
    }
}
