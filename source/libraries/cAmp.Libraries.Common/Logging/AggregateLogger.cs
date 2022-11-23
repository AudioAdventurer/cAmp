using System;
using System.Collections.Generic;
using System.Linq;
using cAmp.Libraries.Common.Interfaces;

namespace cAmp.Libraries.Common.Logging
{
    public class AggregateLogger : IcAmpLogger
    {
        private readonly IEnumerable<IcAmpLogger> _loggers;

        public AggregateLogger(IEnumerable<IcAmpLogger> loggers)
        {
            _loggers = loggers;
        }

        public AggregateLogger(params IcAmpLogger[] loggers)
        {
            _loggers = loggers.ToList();
        }

        public void Verbose(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Verbose(message);
            }
        }

        public void Debug(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Debug(message);
            }
        }

        public void Info(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Info(message);
            }
        }

        public void Warning(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Warning(message);
            }
        }

        public void Error(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Error(message);
            }
        }

        public void Error(Exception ex)
        {
            foreach (var logger in _loggers)
            {
                logger.Error(ex);
            }
        }

        public void Fatal(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Fatal(message);
            }
        }
    }
}
