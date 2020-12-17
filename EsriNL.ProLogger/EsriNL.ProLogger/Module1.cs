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

        protected override bool Initialize()
        {
            //LoggingLogic.Instance.Initialize();
            LogInfo("===============================");
            LogInfo("Start EsriNL.ProLogger v0.1.0.2");
            return base.Initialize();
        }

        public void LogInfo(string message)
        {
            LoggingLogic.Instance.LogMessage(message, log4net.Core.Level.Info);
        }

        public void LogDebug(string message)
        {
            LoggingLogic.Instance.LogMessage(message, log4net.Core.Level.Debug);
        }

        public void LogWarning(string message)
        {
            LoggingLogic.Instance.LogMessage(message, log4net.Core.Level.Warn);
        }

        public void LogError(string message)
        {
            LoggingLogic.Instance.LogMessage(message, log4net.Core.Level.Error);
        }

        public void LogException(Exception ex, string message = null)
        {
            if (!string.IsNullOrEmpty(message))
            {
                LoggingLogic.Instance.LogMessage(message, log4net.Core.Level.Fatal);
            }

            string exceptionMessage = $@"{ex.Message}\r\n{ex.StackTrace}";
            LoggingLogic.Instance.LogMessage(exceptionMessage, log4net.Core.Level.Fatal);
            if (ex.InnerException != null)
            {
                LogException(ex.InnerException, "");
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
