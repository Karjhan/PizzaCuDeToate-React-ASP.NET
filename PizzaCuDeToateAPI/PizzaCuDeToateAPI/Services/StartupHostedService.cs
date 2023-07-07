using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PizzaCuDeToateAPI.Services;

public class StartupHostedService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        CommandRunner.StartCmd("D:\\Programs\\Stripe CLI", "stripe listen --forward-to https://localhost:44388/api/stripe/webhook --skip-verify");
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        CommandRunner.StopCmd();
        
        return Task.CompletedTask;
    }
}