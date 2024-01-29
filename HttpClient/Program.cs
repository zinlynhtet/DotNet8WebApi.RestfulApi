using HttpClientExamples.HttpClientExample;

Console.WriteLine("Please wait for api...");
Console.ReadKey();

HttpClientExample httpClient = new HttpClientExample();
await httpClient.Run();

Console.ReadKey();
