using Cryptocurrencies.Model;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocurrencies.Services.Http
{
    public class HttpClientHelper
    {
        public void Get<T>(string url, ref T obj)
        {
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(url).Result)
                {
                    var customerJsonString = response.Content.ReadAsStringAsync().Result;
                    var json = JObject.Parse(customerJsonString);
                    var dataArray = json["data"].ToString();
                    obj = JsonConvert.DeserializeObject<T>(dataArray);
                }
            }
        }
        public void Get<T>(string url, Action<string> action)
        {
            Task.Run(() =>
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        using (var response = client.GetAsync(url).Result)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var customerJsonString = response.Content.ReadAsStringAsync().Result;
                                var json = JObject.Parse(customerJsonString);
                                var dataArray = json["data"].ToString();
                                action?.Invoke(dataArray);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            });
        }

    }

}
