namespace RunTaskInBackground.Demo
{
    public class SomeService : IDisposable
    {
        private readonly ILogger<SomeService> _logger;
        private bool disposedValue;

        public string From { get; set; } = null!;
        public SomeService(ILogger<SomeService> logger)
        {
            _logger = logger;
        }

        public void DoSomething()
        {
            CheckIfClosedThrowDisposed();
            _logger.LogInformation("我从{From}来的，我在干活！", From);
        }
        void CheckIfClosedThrowDisposed()
        {
            if (disposedValue)
                throw new ObjectDisposedException(null, "我歇逼了，别叫我！");
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!string.IsNullOrEmpty(From))
                    {
                        _logger.LogInformation("我从{From}来的，我歇逼了！", From);
                    }
                    else
                    {
                        _logger.LogInformation("我没有🐎，我歇逼了！");
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
