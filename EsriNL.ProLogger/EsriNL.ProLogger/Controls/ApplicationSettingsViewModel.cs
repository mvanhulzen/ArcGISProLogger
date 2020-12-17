using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

namespace EsriNL.ProLogger.Controls
{
    internal class ApplicationSettingsViewModel : Page
    {
      

        
        /// <summary>
        /// Called when the page is destroyed.
        /// </summary>
        protected override void Uninitialize()
        {
        }

        /// <summary>
        /// Text shown inside the page hosted by the property sheet
        /// </summary>
        public string DataUIContent
        {
            get
            {
                return base.Data[0] as string;
            }
            set
            {
                SetProperty(ref base.Data[0], value, () => DataUIContent);
            }
        }
        private bool _origSaveToLogFile;
        private bool _origOtherSetting;
        private string _origLogFilePath;

        private bool _saveToLogFile;
        public bool SaveToLogFile
        {
            get { return _saveToLogFile; }
            set
            {
                if (SetProperty(ref _saveToLogFile, value, () => SaveToLogFile))
                {
                    base.IsModified = true;
                }
                
            }
        }

        private string _logFilePath;
        public string LogFilePath
        {
            get { return _logFilePath; }
            set
            {
                if (SetProperty(ref _logFilePath, value, () => LogFilePath))
                    base.IsModified = true;
            }
        }

        private bool _otherSetting;
        public bool OtherSetting
        {
            get { return _otherSetting; }
            set
            {
                if (SetProperty(ref _otherSetting, value, () => OtherSetting))
                    base.IsModified = true;
            }
        }

        

        private static bool _isGeneralExpanded = true;
        public bool IsGeneralExpanded
        {
            get { return _isGeneralExpanded; }
            set { SetProperty(ref _isGeneralExpanded, value, () => IsGeneralExpanded); }
        }

        private static bool _isOtherExpanded = true;
        public bool IsOtherExpanded
        {
            get { return _isOtherExpanded; }
            set { SetProperty(ref _isOtherExpanded, value, () => IsOtherExpanded); }
        }

        //#region Page Overrides

        protected override Task InitializeAsync()
        {
            // get the default settings
            //CustomSettings.Properties.Settings settings = CustomSettings.Properties.Settings.Default;
            EsriNL.ProLogger.Properties.ProLoggerSettings settings = EsriNL.ProLogger.Properties.ProLoggerSettings.Default;
            // assign to the values binding to the controls
            _saveToLogFile = settings.SaveToLogFileSetting;
            _otherSetting = settings.OtherSetting;
            _logFilePath = settings.LogFilePathSetting;

            // keep track of the original values (used for comparison when saving)
            _origSaveToLogFile = SaveToLogFile;
            _origOtherSetting = OtherSetting;
            _origLogFilePath = LogFilePath;
            return Task.FromResult(0);
        }

        private bool IsDirty()
        {
            if (_origSaveToLogFile != SaveToLogFile)
            {
                return true;
            }
            if (_origLogFilePath != LogFilePath)
            {
                return true;
            }
            if (_origOtherSetting != OtherSetting)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Invoked when the OK or apply button on the property sheet has been clicked.
        /// </summary>
        /// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
        /// <remarks>This function is only called if the page has set its IsModified flag to true.</remarks>
        protected override Task CommitAsync()
        {
            if (IsDirty())
            {
                // save the new settings
                EsriNL.ProLogger.Properties.ProLoggerSettings settings = EsriNL.ProLogger.Properties.ProLoggerSettings.Default;

                settings.SaveToLogFileSetting = SaveToLogFile;
                settings.OtherSetting = OtherSetting;
                settings.LogFilePathSetting = LogFilePath;

                settings.Save();
            }
            return Task.FromResult(0);
        }
    }

   
}
