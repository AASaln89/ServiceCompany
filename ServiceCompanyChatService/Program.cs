using ServiceCompanyChatService.DbStuff;
using ServiceCompanyChatService.SignalRHubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.SetIsOriginAllowed(url => true);
        policy.AllowCredentials();
    });
});

builder.Services.AddSignalR();

var connStringChat = builder.Configuration.GetConnectionString("ServiceCompanyChat");

builder.Services.AddDbContext<ChatDbContext>(x => x.UseSqlServer(connStringChat));

var app = builder.Build();

app.UseCors();

app.MapHub<BlogHub>("/chat");

app.MapGet("/", () => "Hello World!");

app.Run();
