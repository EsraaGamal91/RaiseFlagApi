using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RaiseFlagApi.Startup))]

namespace RaiseFlagApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            var pathJson = @"C:\Users\Esraa\Documents\Visual Studio 2015\Projects\RaiseFlagApi\RaiseFlagApi\Models\CurrentUsers.json";
            /*var builder = new ConfigurationBuilder();
       .SetBasePath(env.ContentRootPath)
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();*/
        }
    }
}
