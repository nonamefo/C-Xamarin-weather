using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Погода
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public class WeatherData
        {
            public List
<Weather> weather
            { get; set; }
        }

        public class Weather
        {
            public string main { get; set; }
        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                string mainWeather;
                string temp;
                string feels;
                string humidity;
                string pressure;
                string wind;
                string city = nameInput.Text.Trim();

                HttpClient client = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=f6559f86e9d00d7777ea7524394f4f31&units=metric";
                string response = await client.GetStringAsync(url);

                var data = JsonConvert.DeserializeObject<WeatherData>(response);
                var json = JObject.Parse(response);

                mainWeather = data.weather[0].main;
                temp = json["main"]["temp"].ToString();
                feels = json["main"]["feels_like"].ToString();
                humidity = json["main"]["humidity"].ToString();
                pressure = json["main"]["pressure"].ToString();
                wind = json["wind"]["speed"].ToString();

                string end = "temperature: " + temp
                        + "\n" + "feels like: " + feels
                        + "\n" + "description: " + mainWeather
                        + "\n" + "humidity: " + humidity + "%"
                        + "\n" + "pressure: " + pressure
                        + "\n" + "wind" + wind;

                await DisplayAlert("Info", end, "Got it");
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("ERROR", "The city was not found", "Got it");
                nameInput.Text = null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", "somthing went rong\nsorryfor this mistack", "Got it");
                nameInput.Text = null;
            }
        }

    }
}
