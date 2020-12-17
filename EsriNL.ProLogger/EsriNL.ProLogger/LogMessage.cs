using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsriNL.ProLogger
{
    public enum LogLevel { FATAL = 0, ERROR = 10, WARN = 20, INFO = 30, DEBUG = 40 };

    class LogMessage
    {
        public LogMessage()
        {
            this.LogDateTime = DateTime.Now;
        }


        public DateTime LogDateTime { get; set; }

        public string Message { get; set; }

        public log4net.Core.Level LogLevel { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1} - {2}", LogDateTime.ToString("yyyyMMdd - HHmmss"), LogLevel, Message);
        }
    }
}
