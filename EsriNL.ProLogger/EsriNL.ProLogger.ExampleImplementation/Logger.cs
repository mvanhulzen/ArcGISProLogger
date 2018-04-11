using ArcGIS.Desktop.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EsriNL.ProLogger.ExampleImplementation
{
    class Logger
    {
        #region members
        private MethodInfo _logExceptionMethod;
        private MethodInfo _logErrorMethod;
        private MethodInfo _logWarningMethod;
        private MethodInfo _logInfoMethod;
        private MethodInfo _logDebugMethod;

        private ArcGIS.Desktop.Framework.Contracts.Module _logModule;
        private static Logger _instance;
        #endregion

        #region constructor e.d.
        private Logger()
        {
            // find in the application the addin named EsriNL_ProDebugger_Module
            _logModule = FrameworkApplication.FindModule("EsriNL_ProLogger_Module");
            if (_logModule != null)
            {
                var type = _logModule.GetType();

                _logExceptionMethod = type.GetMethod("LogException");
                _logErrorMethod = type.GetMethod("LogError");
                _logWarningMethod = type.GetMethod("LogWarning");
                _logInfoMethod = type.GetMethod("LogInfo");
                _logDebugMethod = type.GetMethod("LogDebug");
            }
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }
        #endregion

        #region public methods

        public static void LogDebug(string message)
        {
            LogMessage(Instance._logDebugMethod, message);
        }
        public static void LogInfo(string message)
        {
            LogMessage(Instance._logInfoMethod, message);
        }
        public static void LogWarning(string message)
        {
            LogMessage(Instance._logWarningMethod, message);
        }
        public static void LogError(string message)
        {
            LogMessage(Instance._logErrorMethod, message);
        }

        public static void LogException(Exception ex, string message = "")
        {
            LogMessage(Instance._logExceptionMethod, ex, message);
        }

        #endregion

        #region private methods
        private static void LogMessage(MethodInfo logMethod, params object[] prm)
        {
            // dankzij reflection hebben we nu toegang tot de methoden van de logdebug
            if (Instance._logDebugMethod != null && Instance._logModule != null)
            {
                logMethod.Invoke(Instance._logModule, prm);
            }
        }
        #endregion
    }
}
