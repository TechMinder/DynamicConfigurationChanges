using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DynamicConfigurationChanges.HostedService
{
    
    public abstract class CustomHostedService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _cancellationTokenSource =
                                                       new CancellationTokenSource();

        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            _executingTask = ExecuteAsync(_cancellationTokenSource.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _cancellationTokenSource.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                                                              cancellationToken));
            }

        }

        public virtual void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
