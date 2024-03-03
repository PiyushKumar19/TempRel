namespace TempRel.Middlewares
{
    public class Debouncer
    {
        private readonly TimeSpan _interval;
        private CancellationTokenSource _cts;

        public Debouncer(TimeSpan interval)
        {
            _interval = interval;
            _cts = new CancellationTokenSource();
        }

        public void Debounce(Action action)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();

            Task.Delay(_interval, _cts.Token)
                .ContinueWith(d =>
                {
                    if (!d.IsCanceled)
                    {
                        action();
                    }
                });
        }
    }
}
