using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreDemos.Configuration.Controllers
{
    [Route("api/[controller]")]
    public class SettingsController : Controller
    {
        [HttpGet]
        public MySettings Get()
        {
            return appSettings;
        }
        
        //IOptions<> will hold values at application startup
        //public SettingsController(IOptions<MySettings> applicationSettings)
        //{
        //    appSettings = applicationSettings.Value;
        //}

        //IOptionsSnapshot<> will reflect updated values that change while the application is running
        //Requires .NET Core 1.1
        public SettingsController(IOptionsSnapshot<MySettings> applicationSettings)
        {
            appSettings = applicationSettings.Value;
        }

        MySettings appSettings;
    }
}
