using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

class Program
{
    public static long sum = 0;
    private static async Task SendPostRequestAsync(HttpClient client, string apiUrl, int requestNumber)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            var jsonData = new
            {
                name = "3333",
                description = "string",
                price = 0,
                amount = 0
            };

            var jsonString = JsonConvert.SerializeObject(jsonData);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            stopwatch.Stop();
            response.EnsureSuccessStatusCode();

            string responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Request {requestNumber} succeeded. Response: {responseData} {stopwatch.ElapsedMilliseconds} ms");
            sum += stopwatch.ElapsedMilliseconds;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            Console.WriteLine($"Request {requestNumber} failed. Error: {ex.Message} {stopwatch.ElapsedMilliseconds} ms");
            sum += stopwatch.ElapsedMilliseconds;
        }
    }

    private static async Task SendConcurrentRequestsAsync(string url, int numberOfRequests)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            Task[] tasks = new Task[numberOfRequests];

            for (int i = 0; i < numberOfRequests; i++)
            {
                tasks[i] = SendPostRequestAsync(httpClient, url, i + 1);
            }

            await Task.WhenAll(tasks);
            Console.WriteLine($"Total: {sum}");
            Console.WriteLine($"Avg: {sum / numberOfRequests}");
        }
    }

    static async Task Main(string[] args)
    {
        string url = "https://localhost:7212/api/Product";  
        int numberOfRequests = 10000;
        
        Console.WriteLine($"Sending {numberOfRequests} requests to {url}...");
        await SendConcurrentRequestsAsync(url, numberOfRequests);
        Console.WriteLine("All requests completed.");
    }
}