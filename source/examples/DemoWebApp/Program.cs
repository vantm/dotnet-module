using Modular.WebHost;
using DemoWebApp;

var app = new MinimalApiWebApplication(args);

app.AddModule<TestWebModule>();

await app.RunAsync();
