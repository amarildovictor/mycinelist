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
        private Timer? _timerUpdateByYear = null;
        private Timer? _timerUpdateUpcoming = null;
        private bool toExecuteTimers = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development" ||
                                       Environment.GetEnvironmentVariable("TO_EXECUTE_TIMERS") == "true";

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timerUpdateByYear = new Timer(async (e) => await DoWorkUpdateByYear(e), null, TimeSpan.Zero, TimeSpan.FromDays(30));

            _timerUpdateUpcoming = new Timer(async (e) => await DoWorkUpdateUpcoming(e), null, TimeSpan.Zero, TimeSpan.FromDays(20));

            return Task.CompletedTask;
        }

        private async Task DoWorkUpdateByYear(object? state)
        {
            if (!toExecuteTimers) return;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IMovieDowloadYearControlService movieDowloadYearControlService;
                movieDowloadYearControlService = scope.ServiceProvider.GetRequiredService<IMovieDowloadYearControlService>();
                
                var count = Interlocked.Increment(ref executionCount);

                await movieDowloadYearControlService.StartUpdateMovieCatalog();

                _logger.LogInformation(
                    "Timed Hosted Service is working to update by year. Count: {Count}", count);
            }
        }

        private async Task DoWorkUpdateUpcoming(object? state)
        {
            if (!toExecuteTimers) return;

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IMovieDowloadYearControlService movieDowloadYearControlService;
                movieDowloadYearControlService = scope.ServiceProvider.GetRequiredService<IMovieDowloadYearControlService>();
                
                var count = Interlocked.Increment(ref executionCount);

                //await movieDowloadYearControlService.StartUpdateUpcoming();

                _logger.LogInformation(
                    "Timed Hosted Service is working to update upcoming. Count: {Count}", count);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timerUpdateByYear?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timerUpdateByYear?.Dispose();
        }
    }
}