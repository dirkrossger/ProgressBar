using System;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace ProgressBar_Dialog
{
    public class ProgressBar : IExtensionApplication
    {
        public void Initialize()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            doc.Editor.WriteMessage("\n-> Test Autocad ProgressBar: PB");
            doc.Editor.WriteMessage("\n-> Test Winform ProgressBar automatically: TB");
            doc.Editor.WriteMessage("\n-> Test Winform ProgressBar by pushing: START");
        }

        public void Terminate()
        {
        }
    }
}
