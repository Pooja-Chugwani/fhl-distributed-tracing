using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;

namespace TracingApplication.Controllers
{
 
    public class ValuesController : ApiController
    {
       


        private static HttpClient shareClient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true")
        };

        static ActivitySource source = new ActivitySource("Parent Service");

        // GET api/values
        public string Get()
        {
            using (Activity activity = source.StartActivity("Get Values Call"))
            {
                using (var client = new HttpClient( new HttpClientHandler { })) 
                {
                    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true");
         
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Result: " + result);
                    return result;
                }
            }
            
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
