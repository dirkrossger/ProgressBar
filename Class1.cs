using Autodesk.AutoCAD.Runtime;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProgressMeterTest
{
    public class Cmds
    {
        [CommandMethod("PB")]
        public void ProgressBarManaged()
        {
            ProgressMeter pm = new ProgressMeter();
            pm.Start("Testing Progress Bar");
            pm.SetLimit(100);
            // Now our lengthy operation
            for (int i = 0; i <= 100; i++)
            {
                System.Threading.Thread.Sleep(5);
                // Increment Progress Meter...
                pm.MeterProgress();
                // This allows AutoCAD to repaint
                Application.DoEvents();
            }
            pm.Stop();
        }
    }
}