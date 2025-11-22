namespace Shuttle.Core.Reflection;

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

        public async Task TryDisposeAsync()
        {
            if (o is IAsyncDisposable disposable)
            {
                await disposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                // ReSharper disable once MethodHasAsyncOverload
                o.TryDispose();
            }
        }
    }
}