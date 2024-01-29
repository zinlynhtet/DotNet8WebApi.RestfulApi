using HttpClientExamples.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HttpClientExamples.HttpClientExample
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            //await BlogGetList();
            await BlogGetById("01HN3GNMVGDTKYB17EGFCPCBDG");
            await BlogCreate("HttpClient", "HttpClient", "HttpClient");
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

        }
        public async Task BlogGetById(string id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7091/api/blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                var item = model.Data;
                await Console.Out.WriteLineAsync(item.Blog_Id);
                await Console.Out.WriteLineAsync(item.Blog_Title);
                await Console.Out.WriteLineAsync(item.Blog_Author);
                await Console.Out.WriteLineAsync(item.Blog_Content);
            }
            else
            {
                await Console.Out.WriteLineAsync("No data found.");
            }

        }
        public async Task BlogCreate(string title, string author, string content)
        {
            BlogViewModel reqModel = new BlogViewModel
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content= content
            };
            string jsonBlog = JsonConvert.SerializeObject(reqModel);
            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7091/api/");
            var response = await client.PostAsync("blog", httpContent);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                await Console.Out.WriteLineAsync(model!.Message);
            }
            
        }
    }
}