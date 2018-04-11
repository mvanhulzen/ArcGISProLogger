using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;

namespace EsriNL.ProLogger
{
    internal class Module1 : Module
    {
        private static Module1 _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static Module1 Current
        {
            get
            {
                return _this ?? (_this = (Module1)FrameworkApplication.FindModule("EsriNL_ProLogger_Module"));
            }
        }

        public void LogInfo(string message)
        {
            LogMessage(message, LogLevel.INFO);
        }

        public void LogDebug(string message)
        {
            LogMessage(message, LogLevel.DEBUG);
        }

        public void LogWarning(string message)
        {
            LogMessage(message, LogLevel.WARN);
        }

        public void LogError(string message)
        {
            LogMessage(message, LogLevel.ERROR);
        }

        public void LogException(Exception ex, string message = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                LogMessage(message, LogLevel.FATAL);
            }

            string exceptionMessage = $@"{ex.Message}\r\n{ex.StackTrace}";
            LogMessage(exceptionMessage, LogLevel.FATAL);
            if (ex.InnerException != null)
            {
                LogException(ex.InnerException, "");
            }
        }

        private void LogMessage(string message, LogLevel level)
        {
            LogMessage logMessage = new LogMessage
            {
                LogLevel = level,
                Message = message
            };
            if (LoggerPaneViewModel.Instance != null)
            {
                LoggerPaneViewModel.Instance.AddMessage(logMessage);
            }
        }

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }

        #endregion Overrides

    }
}
