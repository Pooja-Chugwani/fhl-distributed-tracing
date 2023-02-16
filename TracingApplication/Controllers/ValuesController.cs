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

        static ActivitySource source = new ActivitySource("TracingApplication");

        // GET api/values
        public string Get()
        {
            //using (Activity activity = source.StartActivity("Get Values Call"))
            //{
            string result;
            using (var client = new HttpClient(new HttpClientHandler { }))
            {
                client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current_weather=true");
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Result: " + result);


            }


            Activity.Current?.SetTag("extension", "docx");
            string result2;
            using (var client = new HttpClient(new HttpClientHandler { }))
            {
                client.BaseAddress =
                    new Uri("http://localhost:26976/analyze?profile=Spo&provider=url&extension=jpg&docId=https://images.ctfassets.net/hrltx12pl8hq/4f6DfV5DbqaQUSw0uo0mWi/6fbcf889bdef65c5b92ffee86b13fc44/shutterstock_376532611.jpg?fit=fill&w=800&h=300");
                //new Uri("http://localhost:26976/analyze?profile=Spo&provider=url&extension=docx&docId=https://metastoragecentralus.blob.core.windows.net/testfiles/Office/MediaAnalysisAndTransformsInSPO.docx");
                HttpResponseMessage response2 = client.GetAsync("").Result;
                response2.EnsureSuccessStatusCode();
                result2 = response2.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Result: " + result2);

                //}

                return result + result2;
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
