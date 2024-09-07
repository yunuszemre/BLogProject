using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArcBLogProject.Entities
{
    public static class BlogConfiguration
    {
        public static string GetConStr()
        {
            var s = Directory.GetCurrentDirectory();
            //C:\Users\Emre\source\repos\OnionArcBLogProject\OnionArcBLogProject\OnionArcBLogProject.WebUI
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../OnionArcBLogProject"));
            configurationManager.AddJsonFile("appsettings.json", false, reloadOnChange: true);
            return configurationManager.GetConnectionString("localDb");
        }
    }
}
