using Shuttle.Contract;

namespace Shuttle.Reflection;

public class ExceptionRaisedEventArgs(string methodName, Exception exception) : EventArgs
{
    public Exception Exception { get; } = Guard.AgainstNull(exception);
    public string MethodName { get; } = Guard.AgainstEmpty(methodName);
}