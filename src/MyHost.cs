using Microsoft.Extensions.Hosting;

namespace CSharpGracefulShutdownExample;
internal class MyHost : IHostedService
{
    private bool _shouldStop;
    private Task? _backgroundTask;

    private readonly IHostApplicationLifetime _lifetime;

    public MyHost(IHostApplicationLifetime applicationLifetime) => 
        _lifetime = applicationLifetime;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("starting the service");

        _backgroundTask = Task.Run(async () =>
        {
            while (!_shouldStop)
            {
                Console.WriteLine("service is running");
                await Task.Delay(200);
            }

            Console.WriteLine("Background task gracefully stopped");
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("stopping the service");
        _shouldStop = true;
        await _backgroundTask!;
        Console.WriteLine("Service stopped");
    }
}
