using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CSharpGracefulShutdownExample;

var host = new HostBuilder()
    .ConfigureServices((hostContext, services) => services.AddHostedService<MyHost>())
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();
