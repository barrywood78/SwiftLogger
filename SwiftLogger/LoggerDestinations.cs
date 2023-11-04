using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftLogger
{
    public class LoggerDestinations
    {
        private readonly SwiftLoggerConfig _parent;

        internal LoggerDestinations(SwiftLoggerConfig parent)
        {
            _parent = parent;
        }

        public SwiftLoggerConfig Console(ConsoleLoggerConfig config = null)
        {
            _parent.AddLogger(new ConsoleLogger(config));
            return _parent;
        }

        // ... other methods for File, etc.
    }

}
