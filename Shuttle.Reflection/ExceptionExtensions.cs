using System.Text;
using Shuttle.Contract;

namespace Shuttle.Reflection;

public static class ExceptionExtensions
{
    extension(Exception ex)
    {
        public string AllMessages()
        {
            Guard.AgainstNull(ex);

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
            Guard.AgainstNull(ex);

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

            while (exception.GetType() == trim && exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            return exception.GetType() == trim 
                ? throw new InvalidOperationException($"The entire exception chain consists of '{trim.FullName}' exceptions.") 
                : exception;
        }
    }
}