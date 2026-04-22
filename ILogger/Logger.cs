using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace ILogger
{  
    public class AppLogger : IAppLogger, IDisposable
    {
        private readonly Serilog.Core.Logger _logger;

        public AppLogger(Serilog.Core.Logger logger)
        {
            _logger = logger;
        }

        public void Information(string message, params object[] args) =>
            _logger.Information(message, args);

        public void Warning(string message, params object[] args) =>
            _logger.Warning(message, args);

        public void Error(string message, Exception? ex = null, params object[] args) =>
            _logger.Error(ex, message, args);

        public void Debug(string message, params object[] args) =>
            _logger.Debug(message, args);

        public void Fatal(string message, Exception? ex = null, params object[] args) =>
            _logger.Fatal(ex, message, args);

        public IAppLogger ForContext(string propertyName, object value) => new AppLogger((Serilog.Core.Logger)_logger);
            


        public void Dispose() => _logger.Dispose();
    }
    public static class AppLoggerFactory
    {
        public static AppLogger Create(
            string logPath = "logs/market.log",
            string appName = "Market",
            IEnumerable<ILogEventEnricher>? customEnrichers = null)
        {
            var config = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} " +
                                    "{Properties:j}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    path: logPath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,       // max 30 days
                    fileSizeLimitBytes: 10_000_000,   // max 10mb faili
                    rollOnFileSizeLimit: true,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] " +
                                    "[{MachineName}] [{ThreadId}] [{SourceContext}] " +
                                    "{Message:lj}{NewLine}{Exception} {RequestId} {CorrelationId}"
                )
                .Enrich.WithMachineName()          // Computer Name
                .Enrich.WithThreadId()             // Thread ID
                .Enrich.WithThreadName()           // Thread Name
                .Enrich.WithProcessId()            // Process ID
                .Enrich.WithProcessName()          // Process Name
                .Enrich.WithEnvironmentName()      // Development/Production
                .Enrich.WithProperty("AppName", appName)    // App Name
                .Enrich.WithProperty("AppVersion",
                    Assembly.GetExecutingAssembly()
                        .GetName().Version?.ToString() ?? "unknown");

            // todo: gasarchevia safuzlianad custom enrichers tu daemata
            if (customEnrichers != null)
            {
                foreach (var enricher in customEnrichers)
                    config.Enrich.With(enricher);
            }

            return new AppLogger(config.CreateLogger());
        }
    }


}
