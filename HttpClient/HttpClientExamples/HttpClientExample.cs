using HttpClientExamples.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientExamples.HttpClientExample
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            await BlogGetList();
        }
        public async Task BlogGetList()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7091/api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr);
                foreach (var item in model!)
                {
                    await Console.Out.WriteLineAsync(item.Blog_Id);
                    await Console.Out.WriteLineAsync(item.Blog_Title);
                    await Console.Out.WriteLineAsync(item.Blog_Author);
                    await Console.Out.WriteLineAsync(item.Blog_Content);
                }
            }

            //public async Task BlogGetbyId()
            //{

            //}
            //public async Task BlogCreate()
            //{

            //}
            //public async Task BlogPut()
            //{

            //}
            //public async Task BlogPatch()
            //{

            //}
            //public async Task BlogDelete()
            //{

            //}
        }
    }
}