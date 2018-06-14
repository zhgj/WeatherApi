namespace Model
{
    /// <summary>
    /// 天气实体
    /// </summary>
    public class Weather
    {
        /// <summary>
        /// 地名
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 年月日
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string Week { get; set; }

        /// <summary>
        /// 农历
        /// </summary>
        public string LunarDay { get; set; }

        /// <summary>
        /// 当前气温
        /// </summary>
        public string NowTemperature { get; set; }

        /// <summary>
        /// 最低气温
        /// </summary>
        public string LowTemperture { get; set; }

        /// <summary>
        /// 最高气温
        /// </summary>
        public string HighTemperture { get; set; }

        /// <summary>
        /// 天气代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 天气描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 天气图标
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 天气图标 白
        /// </summary>
        public string WhiteImage { get; set; }

        /// <summary>
        /// 天气图标 黑
        /// </summary>
        public string BlackImage { get; set; }

        /// <summary>
        /// 空气质量值
        /// </summary>
        public string AqiValue { get; set; }

        /// <summary>
        /// 空气质量等级
        /// </summary>
        public string AqiLevel { get; set; }

        /// <summary>
        /// 空气质量描述
        /// </summary>
        public string AqiDescribe { get; set; }

        /// <summary>
        /// 天气PM颗粒物
        /// </summary>
        public string ParticulateMatter { get; set; }

        /// <summary>
        /// 湿度 百分比
        /// </summary>
        public string Humidity { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        public string Wind { get; set; }

        /// <summary>
        /// 紫外线
        /// </summary>
        public string UltravioletRays { get; set; }

        /// <summary>
        /// 日出
        /// </summary>
        public string Sunrise { get; set; }

        /// <summary>
        /// 日落
        /// </summary>
        public string Sunset { get; set; }
    }
}
