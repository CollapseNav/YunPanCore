using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Util
{
    public class ConfigurationService
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigurationService()
        {
            //ReloadOnChange = true 当appsettings.json被修改时重新加载            
            Configuration = new ConfigurationBuilder()
            .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
        }
    }
}
