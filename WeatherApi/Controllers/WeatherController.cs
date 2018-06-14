using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private UrlConfig Config;
        private readonly IHostingEnvironment _hostingEnvironment;
        public WeatherController(IOptions<UrlConfig> option, IHostingEnvironment hostingEnvironment)
        {
            Config = option.Value;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET api/values
        [HttpGet("{region}")]
        public Weather Get(string region)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            string path = Path.GetDirectoryName(this.GetType().Assembly.Location);
            string weatherPath = Path.Combine(path, "Images/");
            Weather weather = new Weather();
            WeatherHelper weatherHelper = new WeatherHelper();
            try
            {
                var dict1 = weatherHelper.GetTodayWeather2(string.Format(Config.Weather, region), weatherPath);
                if (dict1.Count > 0)
                {
                    weather.Region = dict1["region"];
                    weather.Day = dict1["day"];
                    weather.Week = dict1["week"];
                    weather.LunarDay = dict1["lunarDay"];
                    weather.NowTemperature = dict1["nowTemperature"];
                    weather.LowTemperture = dict1["lowTemperature"];
                    weather.HighTemperture = dict1["highTemperature"];
                    weather.Describe = dict1["describe"];
                    weather.Code = dict1["code"];
                    weather.Image = dict1["image"];
                    weather.WhiteImage = dict1["whiteImage"];
                    weather.BlackImage = dict1["blackImage"];
                    weather.AqiValue = dict1["aqiValue"];
                    weather.AqiLevel = dict1["aqiLevel"];
                    weather.AqiDescribe = dict1["aqiDescribe"];
                    weather.ParticulateMatter = dict1["particulateMatter"];
                    weather.Humidity = dict1["humidity"];
                    weather.Wind = dict1["wind"];
                    weather.UltravioletRays = dict1["ultravioletRays"];
                    weather.Sunrise = dict1["sunrise"];
                    weather.Sunset = dict1["sunset"];
                }
            }
            catch (Exception e) { }
            try
            {
                var dict2 = weatherHelper.GetTodayAQI(string.Format(Config.Aqi, region));
                if (dict2.Count > 0)
                {
                    weather.AqiValue = dict2["idx"];
                    weather.AqiLevel = dict2["aqiLevel"];
                    weather.AqiDescribe = dict2["qualityStr"];
                }
            }
            catch (Exception e) { }
            return weather;
        }
    }
}
