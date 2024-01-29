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
            await BlogGetById("01HNB5Q71843SKGY5VWWP654V4");
            //await BlogDelete("01HNB5Q71843SKGY5VWWP654V4");
            //await BlogPatch("01HNB5Q71843SKGY5VWWP654V4", "Patch", "Patch", "Patch");
            //await BlogCreate("HttpClient", "HttpClient", "HttpClient");
            //await BlogPut("01HN3GNMVGDTKYB17EGFCPCBDG", "Put", "Put", "Put");
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
                Blog_Content = content
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
        public async Task BlogPut(string id, string title, string author, string content)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7091/api/");
            var response = await client.GetAsync($"blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                BlogViewModel reqModel = new BlogViewModel
                {
                    Blog_Title = title,
                    Blog_Author = author,
                    Blog_Content = content
                };

                string jsonBlog = JsonConvert.SerializeObject(reqModel);
                HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
                var response1 = await client.PutAsync($"blog/{id}", httpContent);
                if (response1.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                    await Console.Out.WriteLineAsync(model!.Message);
                }
                else
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                    await Console.Out.WriteLineAsync(model!.Message);
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Not found.");
            }

        }
        public async Task BlogPatch(string id, string title, string author, string content)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7091/api/");
            var response = await client.GetAsync($"blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                BlogViewModel reqModel = new BlogViewModel();

                if (!string.IsNullOrEmpty(title))
                {
                    reqModel.Blog_Title = title;
                }

                if (!string.IsNullOrEmpty(author))
                {
                    reqModel.Blog_Author = author;
                }

                if (!string.IsNullOrEmpty(content))
                {
                    reqModel.Blog_Content = content;
                }
                if (string.IsNullOrEmpty(reqModel.Blog_Title)
                    && string.IsNullOrEmpty(reqModel.Blog_Author)
                    && string.IsNullOrEmpty(reqModel.Blog_Content))
                {
                    await Console.Out.WriteLineAsync("No data to update.");
                    return;
                }
                string jsonBlog = JsonConvert.SerializeObject(reqModel);
                HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, "application/json");
                var response1 = await client.PatchAsync($"blog/{id}", httpContent);
                if (response1.IsSuccessStatusCode)
                {
                    string jsonStr = await response1.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                    await Console.Out.WriteLineAsync(model!.Message);
                }
                else
                {
                    string jsonStr = await response1.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                    await Console.Out.WriteLineAsync(model!.Message);
                }
            }
        }

        public async Task BlogDelete(string id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7091/api/");
            HttpResponseMessage response = await client.DeleteAsync($"blog/{id}");
            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogDataResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }
    }
}