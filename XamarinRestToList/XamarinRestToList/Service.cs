using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

namespace XamarinRestToList
{
    public class Service
    {
        private readonly Uri url;
        private HttpClient client;

        public Service()
        {
            url = new Uri("http://jsonplaceholder.typicode.com/posts");
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(3);
        }

        public async Task<List<Post>> GetPosts()
        {
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonResult = JsonConvert.DeserializeObject<List<Post>>(content);
                    return jsonResult;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}