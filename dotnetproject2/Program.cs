using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", ([FromBody]Request request)=>request.Message);
app.Run();

class Request
{
    public string Message { get; set; }
}