using System;
using Microsoft.Extensions.Logging;
using NLogLogger = NLog.ILogger;
using NLog;
using TaskManager.Core.Interface.Logger;

namespace TaskManager.Infrastructure.Services
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly NLogLogger logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}

