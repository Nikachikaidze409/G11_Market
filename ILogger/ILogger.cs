namespace ILogger
{
    public interface IAppLogger
    {
        void Information(string message, params object[] args);
        void Warning(string message, params object[] args);
        void Error(string message, Exception? ex = null, params object[] args);
        void Debug(string message, params object[] args);
        void Fatal(string message, Exception? ex = null, params object[] args);
        IAppLogger ForContext(string propertyName, object value);
    }
}
