using ServiceCompany.BackgroundServices;
using ServiceCompany.BusinessServices;
using ServiceCompany.Controllers;
using ServiceCompany.DbStuff;
using ServiceCompany.DbStuff.Repositories;
using ServiceCompany.DbStuff.Repositories.MongoRepositories;
using ServiceCompany.Hubs;
using ServiceCompany.Services;
using Microsoft.EntityFrameworkCore;
using ServiceCompany.Middlewares;
using ServiceCompany.ApiServices;
using ServiceCompany.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(AuthController.AUTH_KEY)
    .AddCookie(AuthController.AUTH_KEY, option =>
    {
        option.AccessDeniedPath = "/auth/deny";
        option.LoginPath = "/Auth/Login";
    });

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

builder.Services.AddHostedService<AlertMaintenance>();

builder.Services.AddControllersWithViews();

var connStringManagementCompany = builder.Configuration.GetConnectionString("ServiceCompany");

builder.Services.AddDbContext<ServiceCompanyDbContext>(x => x.UseSqlServer(connStringManagementCompany));

builder.Services.AddSignalR();

// Repositories
builder.Services.AddScoped<TaskStatusRepository>();
builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<ArticleRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserProfileRepository>();
builder.Services.AddScoped<UserTaskRepository>();
builder.Services.AddScoped<MemberRoleRepository>();
builder.Services.AddScoped<AlertRepository>();
builder.Services.AddScoped<IFileJpgRepository, FileJpgRepository>();
builder.Services.AddScoped<ITextFileRepository, TextFileRepository>();

// Services
builder.Services.AddScoped<CreateFilePathHelper>();
builder.Services.AddScoped<UploadFileHelper>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserBusinessService>();
builder.Services.AddScoped<ReflectionService>();
builder.Services.AddSingleton<WeatherViewModelBuilder>();

builder.Services.AddHttpClient<NumberApi>(cl =>
{
    cl.BaseAddress = new Uri("http://numbersapi.com");
});

builder.Services.AddHttpClient<WeatherApi>(cl =>
{
    cl.BaseAddress = new Uri("http://api.open-meteo.com");
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors();

SeedExtension.Seed(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LocalisationMiddleware>();

app.MapHub<AlertHub>("/signalr-hubs/alert");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ServiceCompany}/{action=Index}/{id?}");

app.Run();