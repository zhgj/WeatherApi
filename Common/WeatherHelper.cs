using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class WeatherHelper
    {
        private static HttpClient client = null;

        private Dictionary<string, string> dict = new Dictionary<string, string>();
        private Dictionary<string, string> describe_code = new Dictionary<string, string>();
        private List<string> userAgentList = new List<string>();
        public WeatherHelper()
        {
            #region 初始化天气代码对应天气描述和图标
            dict.Add("0", "100;晴;Sunny/Clear");
            dict.Add("1", "100;晴;Sunny/Clear");
            dict.Add("2", "100;晴;Sunny/Clear");
            dict.Add("3", "100;晴;Sunny/Clear");
            dict.Add("4", "101;多云;Cloudy");
            dict.Add("7", "101;多云;Cloudy");
            dict.Add("8", "101;多云;Cloudy");
            dict.Add("102", "102;少云;Few Clouds");
            dict.Add("5", "103;晴间多云;Partly Cloudy");
            dict.Add("6", "103;晴间多云;Partly Cloudy");
            dict.Add("9", "104;阴;Overcast");
            dict.Add("32", "200;有风;Windy");
            dict.Add("201", "201;平静;Calm");
            dict.Add("202", "202;微风;Light Breeze");
            dict.Add("203", "203;和风;Moderate/Gentle Breeze");
            dict.Add("204", "204;清风;Fresh Breeze");
            dict.Add("205", "205;强风/劲风;Strong Breeze");
            dict.Add("206", "206;疾风;High Wind, Near Gale");
            dict.Add("33", "207;大风;Gale");
            dict.Add("208", "208;烈风;Strong Gale");
            dict.Add("209", "209;风暴;Storm");
            dict.Add("210", "210;狂爆风;Violent Storm");
            dict.Add("34", "211;飓风;Hurricane");
            dict.Add("36", "212;龙卷风;Tornado");
            dict.Add("35", "213;热带风暴;Tropical Storm");
            dict.Add("10", "300;阵雨;Shower Rain");
            dict.Add("301", "301;强阵雨;Heavy Shower Rain");
            dict.Add("11", "302;雷阵雨;Thundershower");
            dict.Add("303", "303;强雷阵雨;Heavy Thunderstorm");
            dict.Add("12", "304;雷阵雨伴有冰雹;Hail");
            dict.Add("13", "305;小雨;Light Rain");
            dict.Add("14", "306;中雨;Moderate Rain");
            dict.Add("15", "307;大雨;Heavy Rain");
            dict.Add("308", "308;极端降雨;Extreme Rain");
            dict.Add("309", "309;毛毛雨/细雨;Drizzle Rain");
            dict.Add("16", "310;暴雨;Storm");
            dict.Add("17", "311;大暴雨;Heavy Storm");
            dict.Add("18", "312;特大暴雨;Severe Storm");
            dict.Add("19", "313;冻雨;Freezing Rain");
            dict.Add("22", "400;小雪;Light Snow");
            dict.Add("23", "401;中雪;Moderate Snow");
            dict.Add("24", "402;大雪;Heavy Snow");
            dict.Add("25", "403;暴雪;Snowstorm");
            dict.Add("20", "404;雨夹雪;Sleet");
            dict.Add("405", "405;雨雪天气;Rain And Snow");
            dict.Add("406", "406;阵雨夹雪;Shower Snow");
            dict.Add("21", "407;阵雪;Snow Flurry");
            dict.Add("500", "500;薄雾;Mist");
            dict.Add("30", "501;雾;Foggy");
            dict.Add("31", "502;霾;Haze");
            dict.Add("27", "503;扬沙;Sand");
            dict.Add("26", "504;浮尘;Dust");
            dict.Add("28", "507;沙尘暴;Duststorm");
            dict.Add("29", "508;强沙尘暴;Sandstorm");
            dict.Add("38", "900;热;Hot");
            dict.Add("37", "901;冷;Cold");
            dict.Add("99", "999;未知;Unknown");
            #endregion

            foreach (var value in dict.Values)
            {
                string[] describe = value.Split(';');
                if (!describe_code.ContainsKey(describe[1]))
                    describe_code.Add(describe[1], describe[0]);
            }

            userAgentList.AddRange(new string[] {
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36 Edg/125.0.0.0",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.82 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Firefox/78.0",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.96 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36 Edg/86.0.622.38",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.93 Safari/537.36",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Firefox/89.0",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.107 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"});
        }
        public Dictionary<string, string> GetTodayWeather()
        {
            var url = "http://tj.nineton.cn/Heart/index/all?city=CHAH000000";

            string html3 = Encoding.UTF8.GetString(client.GetByteArrayAsync(url).Result);
            client.Dispose();
            //html3 = ToChinese(html3);

            var jObject = JObject.Parse(html3);
            var nowObject = jObject["weather"][0]["now"];

            Dictionary<string, string> weatherDict = new Dictionary<string, string>();
            weatherDict.Add("temperature", nowObject["temperature"].ToString());
            weatherDict.Add("describe", nowObject["text"].ToString());
            weatherDict.Add("code", nowObject["code"].ToString());
            string code = nowObject["code"].ToString();
            if (dict.ContainsKey(code))
                weatherDict.Add("image", "/Images/weather_icon_white/" + dict[code].Split(';')[0] + ".png");
            else
                weatherDict.Add("image", "/Images/weather_icon_white/999.png");
            var airQualityObject = nowObject["air_quality"]["city"];

            weatherDict.Add("aqiValue", airQualityObject["aqi"].ToString());
            weatherDict.Add("aqiLevel", "0");
            weatherDict.Add("aqiDescribe", airQualityObject["quality"].ToString());

            return weatherDict;
        }

        public Dictionary<string, string> GetTodayAQI(string aqiUrl)
        {
            if (client == null)
                client = new HttpClient();
            string html5 = Encoding.UTF8.GetString(client.GetByteArrayAsync(aqiUrl).Result);
            string patternAQI = "<script type=\"text/javascript\">(.*?)</script>";
            Dictionary<string, string> aqiDict = new Dictionary<string, string>();
            MatchCollection mcs = Regex.Matches(html5, patternAQI, RegexOptions.Singleline);
            foreach (Match m in mcs)
            {
                string if_else = m.Groups[1].Value;
                if (if_else.Contains("if") && if_else.Contains("else"))
                {
                    string[] if_elseArr = if_else.Split(new string[] { "else" }, StringSplitOptions.RemoveEmptyEntries);
                    if (if_elseArr.Length == 2)
                    {
                        string[] aqiArr = if_elseArr[1].Trim('\r', '\n', ' ', '{', '}').Split(';');
                        for (int i = 0; i < aqiArr.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(aqiArr[i]))
                            {
                                string[] aqi = aqiArr[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                                aqiDict.Add(aqi[0].Replace("var", "").Trim(), aqi[1].Trim('"', ' '));
                            }
                        }
                    }
                    break;
                }
            }
            return aqiDict;
        }

        public Dictionary<string, string> GetTodayWeather2(string weatherUrl,string weatherImagePath)
        {
            var web = new HtmlWeb();
            Random random = new Random();
            int randomNumber = random.Next(0, 12);
            web.UserAgent = userAgentList[randomNumber];
            var doc = web.Load(weatherUrl, "POST");

            HtmlNode weather_info = doc.QuerySelector(".weather_info");
            
            HtmlNode week = weather_info.QuerySelector(".week");
            string[] date = week.InnerText.Trim().Split("　", StringSplitOptions.RemoveEmptyEntries);

            HtmlNode weather = weather_info.QuerySelector(".weather");
            Dictionary<string, string> weatherDict = new Dictionary<string, string>();
            HtmlNode name = weather_info.QuerySelector(".name");
            string region = name.Descendants("h1").FirstOrDefault().InnerText.Trim().Replace("天气", "");
            weatherDict.Add("region", region);
            weatherDict.Add("day", date[0]);
            weatherDict.Add("week", date[1]);
            weatherDict.Add("lunarDay",date[2]);
            string img_url = weather.Descendants("img").Select(x => x.Attributes["src"].Value).FirstOrDefault();
            if (!img_url.Contains("http:"))
            {
                if (img_url.StartsWith("//"))
                    img_url = "http:" + img_url;
                else if (img_url.StartsWith("www"))
                    img_url = "http://" + img_url;
            }
            weatherDict.Add("image", img_url);
            var tempAndDescribe = weather.Descendants("b");
            string now_temperature = "";
            string temp_describe = "";
            if (tempAndDescribe.Count() > 0)
            {
                now_temperature = tempAndDescribe.FirstOrDefault().InnerText.Trim();
                temp_describe = tempAndDescribe.LastOrDefault().InnerText.Trim();
            }
            string highAndlow_temp = weather.Descendants("span").Count() > 0 ? weather.Descendants("span").FirstOrDefault().InnerHtml.Split(new string[] { "</b>" }, StringSplitOptions.RemoveEmptyEntries)[1] : "";
            string[] high_low = highAndlow_temp.Replace("℃", "").Replace(" ", "").Split('~');
            string low = "";
            string high = "";
            if (high_low.Length == 2)
            {
                low = high_low[0];
                high = high_low[1];
            }

            weatherDict.Add("nowTemperature", now_temperature);
            weatherDict.Add("lowTemperature", low);
            weatherDict.Add("highTemperature", high);
            weatherDict.Add("describe", temp_describe);
            string[] td_keys = temp_describe.Split('转');
            string td_key = td_keys.Count() > 1 ? td_keys[1] : td_keys[0];
            if (describe_code.ContainsKey(td_key))
            {
                string code = describe_code[td_key];
                weatherDict.Add("code", code);
                weatherDict.Add("whiteImage", "/Images/weather_icon_white/" + code + ".png");
                weatherDict.Add("blackImage", "/Images/weather_icon/" + code + ".png");
            }
            else
            {
                weatherDict.Add("code", "999");
                weatherDict.Add("whiteImage", "/Images/weather_icon_white/999.png");
                weatherDict.Add("blackImage", "/Images/weather_icon/999.png");
            }

            weatherDict.Add("aqiValue", "0");
            weatherDict.Add("aqiLevel", "0");
            HtmlNode kongqi = weather_info.QuerySelector(".kongqi");
            string aqiDescribe = kongqi.Descendants("h5").Count() > 0 ? kongqi.Descendants("h5").FirstOrDefault().InnerText.Trim().Split(':', '：')[1].Trim() : "";
            weatherDict.Add("aqiDescribe", aqiDescribe);
            string pm = kongqi.Descendants("h6").Count() > 0 ? kongqi.Descendants("h6").FirstOrDefault().InnerText.Trim().Split(':', '：')[1].Trim() : "";
            weatherDict.Add("particulateMatter", pm);
            var kongqi_descendants = kongqi.Descendants("span").ToList();
            string[] sun = kongqi_descendants.FirstOrDefault().InnerHtml.Split(new string[] { "<br/>", "<br>" }, StringSplitOptions.RemoveEmptyEntries);
            string sunrise = kongqi_descendants.Count > 0 ? sun[0].Trim() : "";
            weatherDict.Add("sunrise", sunrise);
            string sunset = kongqi_descendants.Count > 0 ? sun[1].Trim() : "";
            weatherDict.Add("sunset", sunset);

            HtmlNode shidu = weather_info.QuerySelector(".shidu");
            var shidu_descendants = shidu.Descendants("b").ToList();
            int shidu_count = shidu_descendants.Count;
            string humidity = shidu_count > 0 ? shidu_descendants[0].InnerText.Trim().Split(':', '：')[1].Trim() : "";
            weatherDict.Add("humidity", humidity);
            string wind = shidu_count > 0 ? shidu_descendants[1].InnerText.Trim().Split(':', '：')[1].Trim() : "";
            weatherDict.Add("wind", wind);
            string ultravioletRays = shidu_count > 0 ? shidu_descendants[2].InnerText.Trim().Split(':', '：')[1].Trim() : "";
            weatherDict.Add("ultravioletRays", ultravioletRays);

            try
            {
                if (client == null)
                    client = new HttpClient();
                byte[] imageArr = client.GetByteArrayAsync(img_url).Result;
                File.WriteAllBytesAsync(weatherImagePath + temp_describe + ".png", imageArr);
                if (weatherDict.ContainsKey("image"))
                    weatherDict["image"] = "/Images/" + temp_describe + ".png";
            }
            catch (Exception e) { }

            return weatherDict;
        }



        private static string ToChinese(string text)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (i + 2 > text.Length)
                {
                    string t2 = text.Substring(i, text.Length - i);
                    sb.Append(t2);
                    break;
                }
                else
                {
                    string t1 = text.Substring(i, 2);
                    if (t1 == @"\u")
                    {
                        if (i + 6 <= text.Length)
                        {
                            string t2 = text.Substring(i, 6);
                            i = i + 6 - 1;
                            string word = t2.Substring(2);
                            byte[] codes = new byte[2];
                            int code = Convert.ToInt32(word.Substring(0, 2), 16);
                            int code2 = Convert.ToInt32(word.Substring(2), 16);
                            codes[0] = (byte)code2;
                            codes[1] = (byte)code;
                            sb.Append(Encoding.Unicode.GetString(codes));
                        }
                        else
                        {
                            string t2 = text.Substring(i, text.Length - i);
                            sb.Append(t2);
                            break;
                        }

                    }
                    else
                    {
                        sb.Append(text.Substring(i, 1));
                    }
                }

            }

            return sb.ToString();
        }
    }
}
