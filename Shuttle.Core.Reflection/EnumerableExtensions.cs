using Shuttle.Core.Contract;

namespace Shuttle.Core.Reflection;

public static class EnumerableExtensions
{
    extension(IEnumerable<object> list)
    {
        public T? Find<T>() where T : class
        {
            var matches = list.FindAll<T>().ToList();

            if (matches.Count > 1)
            {
                throw new InvalidOperationException(string.Format(Resources.EnumerableFoundTooManyException, matches.Count, typeof(T).FullName));
            }

            return matches.Count == 1 ? matches[0] : null;
        }

        public IEnumerable<T> FindAll<T>() where T : class
        {
            var type = typeof(T);

            return Guard.AgainstNull(list).Where(o => o.GetType().IsCastableTo(type)).Select(o => (T)o).ToList();
        }

        public T Get<T>() where T : class
        {
            var result = list.Find<T>();

            return result ?? throw new InvalidOperationException(string.Format(Resources.EnemerableNoMatchException, typeof(T).FullName));
        }
    }
}