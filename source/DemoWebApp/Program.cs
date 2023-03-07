using Modular;
using DemoWebApp;

var builder = WebApplication.CreateBuilder(args);

builder.AddModules();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.MapModuleEndpoints();
    
app.Run();