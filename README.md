# WeatherApi
* 获取今天的天气情况且带天气图标、空气质量、湿度、紫外线、日出日落时间等信息的webapi接口。

* vs 2017 asp.net core 2.1 版本，支持跨平台部署：`windows`、`macOS`、`linux`平台部署。

* 接口路径 `ip:port/api/weather/xxx`, xxx为地名全拼，端口默认`5000`，ip和port请在`WeatherApi/hosting.json`文件中自己配置。

* 返回结果示例
```
{
    "region": "上海",
    "day": "2018年06月14日",
    "week": "星期四",
    "lunarDay": "戊戌年五月初一",
    "nowTemperature": "30", //当前气温
    "lowTemperature": "23", //最低气温
    "highTemperature": "30", //最高气温
    "code": "100",
    "describe": "晴",
    "image": "/Images/晴.png", //彩色天气图标
    "whiteImage": "/Images/weather_icon_white/100.png", //白色天气图标
    "blackImage": "/Images/weather_icon/100.png", //黑色天气图标
    "aqiValue": "52",
    "aqiLevel": "2",
    "aqiDescribe": "良",
    "particulateMatter": "29", //PM
    "humidity": "49%", //湿度
    "wind": "东南风 3级",
    "ultravioletRays": "强", //紫外线
    "sunrise": "日出: 04:49",
    "sunset": "日落: 18:59"
}
```
* 如果不想自己部署 可以使用我部署好的。`http://caff.ml:5000/api/weather/xxx`, xxx为地名全拼。

* 一些城市全拼的拼音是一样的，如：`巫溪`,`无锡`的全拼都是`wuxi`，这时候你可以尝试`wuxi1`来调用。即全拼后加数字`1`,`2`等来获取天气情况。

* 如果使用过程有问题请提交`issues`。

