using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IorwoodDemo.Areas.JWTAuth.Helpers
{
    public class clearRefreshTokenCache : IHostedService, IDisposable
    {

        private Timer _timer;
        private readonly JwtAuthManager _jwtAuthManager;
        public clearRefreshTokenCache(JwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }
        

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _jwtAuthManager.RemoveExpiredRefreshTokens(DateTime.Now);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
