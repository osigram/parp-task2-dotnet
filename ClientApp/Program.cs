using System.Text;

const int testSize = 20000;
const int stringSize = 20;
const string path = "http://localhost:57309";

var client = new HttpClient();
var testArray = GenerateTestStrings();

var start = DateTime.Now;
Parallel.ForEach(testArray, test => GetAsync(test).Wait());
Console.WriteLine(DateTime.Now.Subtract(start));

async Task<string> GetAsync(string data)
{
    using HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
        
    HttpRequestMessage requestMessage = new HttpRequestMessage() 
    { 
        Content = content,
        Method = HttpMethod.Post,
        RequestUri = new Uri(path)
    };
        
    using HttpResponseMessage response = await client.SendAsync(requestMessage);

    return await response.Content.ReadAsStringAsync();
}

string[] GenerateTestStrings()
{
    var result = new string[testSize];
    var str = new StringBuilder(stringSize);
    for (var i = 0; i < testSize; i++)
    {
        str.Append("{\"Message\": \"");
        for (var j = 0; j < stringSize; j++)
        {
            str.Append((char)97+new Random().Next(24));
        }

        str.Append("\"}");

        result[i] = str.ToString();
        str.Clear();
    }

    return result;
}
