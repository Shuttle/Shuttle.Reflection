using Shuttle.Core.Contract;

namespace Shuttle.Core.Reflection;

public class ExceptionRaisedEventArgs(string methodName, Exception exception) : EventArgs
{
    public Exception Exception { get; } = Guard.AgainstNull(exception);
    public string MethodName { get; } = Guard.AgainstEmpty(methodName);
}