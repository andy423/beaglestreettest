using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeagleStreetTest.Domain.OpenWeather
{
    public class OpenWeatherResponse : ResponseBase
    {
        public string LocationName { get; set; }
        public double TemperatureCurrent { get; set; }
        public double TemperatureMaximum { get; set; }
        public double TemperatureMinimum { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
