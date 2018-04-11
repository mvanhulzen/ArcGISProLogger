using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace EsriNL.ProLogger
{
    internal class LoggerPaneViewModel : DockPane
    {
        private const string _dockPaneID = "EsriNL_ProLogger_LoggerPane";
        private const string _menuID = "EsriNL_ProLogger_LoggerPane_Menu";
        private static LoggerPaneViewModel _instance;
        protected LoggerPaneViewModel()
        {
            _instance = this;
            AddMessage(new LogMessage("First: version: 0.1.0.0", LogLevel.INFO));
        }

        public void AddMessage(LogMessage debugMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(debugMessage);
            });
        }

        public static LoggerPaneViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
                    if (pane != null)
                    {
                        LoggerPaneViewModel dpv = pane as LoggerPaneViewModel;
                        _instance = dpv;
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;

            pane.Activate();
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "My DockPane";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }
       

        private ObservableCollection<LogMessage> _messages = new ObservableCollection<LogMessage>();
        public ObservableCollection<LogMessage> Messages
        {
            get { return _messages; }
            set
            {
                SetProperty(ref _messages, value, () => Messages);
            }
        }


        #region Burger Button

        /// <summary>
        /// Tooltip shown when hovering over the burger button.
        /// </summary>
        public string BurgerButtonTooltip
        {
            get { return "Options"; }
        }

        /// <summary>
        /// Menu shown when burger button is clicked.
        /// </summary>
        public System.Windows.Controls.ContextMenu BurgerButtonMenu
        {
            get { return FrameworkApplication.CreateContextMenu(_menuID); }
        }
        #endregion
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class LoggerPane_ShowButton : Button
    {
        protected override void OnClick()
        {
            LoggerPaneViewModel.Show();
        }
    }

    /// <summary>
    /// Button implementation for the button on the menu of the burger button.
    /// </summary>
    internal class LoggerPane_MenuButton : Button
    {
        protected override void OnClick()
        {
        }
    }
}
