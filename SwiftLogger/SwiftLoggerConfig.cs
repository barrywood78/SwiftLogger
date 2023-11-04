﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftLogger
{
    public class SwiftLoggerConfig
    {
        private readonly List<ILogger> _loggers = new List<ILogger>();

        public LoggerDestinations LogTo => new LoggerDestinations(this);

        internal void AddLogger(ILogger logger)
        {
            _loggers.Add(logger);
        }

 
        public SwiftLogger Build()
        {
            return new SwiftLogger(_loggers);
        }
    }

}