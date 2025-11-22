using System.Text;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Reflection;

public static class ExceptionExtensions
{
    extension(Exception ex)
    {
        public string AllMessages()
        {
            var messages = new StringBuilder();

            var enumerator = ex;

            while (enumerator != null)
            {
                messages.Append($"{(messages.Length > 0 ? " / " : string.Empty)}{enumerator.Message}");

                enumerator = enumerator.InnerException;
            }

            return messages.ToString();
        }

        public bool Contains<T>() where T : Exception
        {
            return ex.Find<T>() != null;
        }

        public T? Find<T>() where T : Exception
        {
            var enumerator = ex;

            while (enumerator != null)
            {
                if (enumerator is T result)
                {
                    return result;
                }

                enumerator = enumerator.InnerException;
            }

            return null;
        }

        public Exception TrimLeading<T>() where T : Exception
        {
            var trim = typeof(T);

            var exception = Guard.AgainstNull(ex);

            while (exception.GetType() == trim)
            {
                if (exception.InnerException == null)
                {
                    break;
                }

                exception = exception.InnerException;
            }

            return exception;
        }
    }
}