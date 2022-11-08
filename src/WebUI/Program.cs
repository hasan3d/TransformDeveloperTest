using TransformDeveloperTest.Application;
using TransformDeveloperTest.Application.Common.Models;
using TransformDeveloperTest.Infrastructure;
using WebUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();
builder.Services.Configure<TflOptions>(builder.Configuration.GetSection("Tfl"));

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddWebUIServices();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");

app.Run();