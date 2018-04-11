using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace EsriNL.ProLogger.ExampleImplementation
{
    internal class HelloWorldButton : Button
    {
        protected override void OnClick()
        {

            ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Hello World!");
            Logger.LogDebug("Hello world");
        }
    }
}
