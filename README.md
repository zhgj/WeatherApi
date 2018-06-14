# WeatherApi
* 获取今天的天气、空气质量等信息接口

* asp.net core 2.1 版本

* 路径 `ip:port/api/weather/xxx`, xxx为地名全拼

* 返回结果示例
```
{
    "region": "上海",
    "day": "2018年06月14日",
    "week": "星期四",
    "lunarDay": "戊戌年五月初一",
    "nowTemperature": "30",
    "lowTemperture": "23",
    "highTemperture": "30",
    "code": "100",
    "describe": "晴",
    "image": "/Images/晴.png",
    "whiteImage": "/Images/weather_icon_white/100.png",
    "blackImage": "/Images/weather_icon/100.png",
    "aqiValue": "52",
    "aqiLevel": "2",
    "aqiDescribe": "良",
    "particulateMatter": "29",
    "humidity": "49%",
    "wind": "东南风 3级",
    "ultravioletRays": "强",
    "sunrise": "日出: 04:49",
    "sunset": "日落: 18:59"
}
```

