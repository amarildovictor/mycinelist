using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.Domain.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private Timer? _timer = null;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(async (e) => await DoWork(e), null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

            return Task.CompletedTask;
        }

        private async Task DoWork(object? state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IMovieDowloadYearControlService movieDowloadYearControlService;
                movieDowloadYearControlService = scope.ServiceProvider.GetRequiredService<IMovieDowloadYearControlService>();
                
                var count = Interlocked.Increment(ref executionCount);

                await movieDowloadYearControlService.StartUpdateMovieCatalog();

                _logger.LogInformation(
                    "Timed Hosted Service is working. Count: {Count}", count);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}