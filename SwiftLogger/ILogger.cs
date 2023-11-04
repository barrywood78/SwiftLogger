using SwiftLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftLogger
{
    public interface ILogger
    {
        void Log(LogEvent logEvent);
    }

}
