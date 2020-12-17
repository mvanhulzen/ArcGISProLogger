using EsriNL.ProLogger.Properties;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsriNL.ProLogger
{
    class LoggingLogic
    {
        private LoggingLogic()
        {
            Initialize();
        }

        private static LoggingLogic _instance;

        public static LoggingLogic Instance {
            get {
                if (_instance == null)
                {
                    _instance = new LoggingLogic();
                }
                return _instance;
            }
        }
      



        internal void Initialize()
        {
            DefineLogger();
        }
        private static ILog _log = LogManager.GetLogger(typeof(EsriNL.ProLogger.Module1));

        private void DefineLogger()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            IEnumerable<IAppender>ap = hierarchy.GetAppenders().Where(f => f.Name == "ProLoggerFileAppender");

            if (ap.Count() != 0)
            {
                IAppender fa = ap.First();
                fa.Close();

            }

            if (Properties.ProLoggerSettings.Default.SaveToLogFileSetting)
            {
                PatternLayout patternLayout = new PatternLayout();
                patternLayout.ConversionPattern = "%date [%thread] %-5level - %message%newline";
                patternLayout.ActivateOptions();

                string logFilePath = ProLoggerSettings.Default.LogFilePathSetting;
                logFilePath = logFilePath.Replace("[date]", DateTime.Now.ToString("yyyMMdd-HHmmss"));

                log4net.Appender.FileAppender f = new log4net.Appender.FileAppender();
                f.Layout = patternLayout;
                f.File = logFilePath;
                f.AppendToFile = true;
                
                f.Name = "ProLoggerFileAppender";
                f.ActivateOptions();


                hierarchy.Root.AddAppender(f);

                hierarchy.Root.Level = Level.Debug;
                hierarchy.Configured = true;

               
            }
        }

        internal void LogMessage(string message, log4net.Core.Level level)
        {
            
            LogMessage logMessage = new LogMessage
            {
                LogLevel = level,
                Message = message
            };

            LoggingEventData logdata = new LoggingEventData()
            {
                Message = message,
                Level = level,
                TimeStampUtc = DateTime.UtcNow
            };

            if (LoggerPaneViewModel.Instance != null)
            {
                LoggerPaneViewModel.Instance.AddMessage(logMessage);
            }
            if (_log != null)
            {
                LoggingEvent le = new LoggingEvent(logdata);
                _log.Logger.Log(le);
                
            }
        }
    }
}
