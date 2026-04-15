namespace Shuttle.Reflection;

public static class ObjectExtensions
{
    extension(object o)
    {
        public void TryDispose()
        {
            if (o is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public Task TryDisposeAsync()
        {
            if (o is IAsyncDisposable asyncDisposable)
            {
                return asyncDisposable.DisposeAsync().AsTask();
            }

            o.TryDispose();

            return Task.CompletedTask;
        }
    }
}