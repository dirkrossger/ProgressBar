using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;


namespace App_Dialog
{
    public partial class Form1 : Form
    {
        private bool complete;
        private App_Alignment.Extensions o1;
        private App_Corridor.Extensions o2;

        public Form1()
        {
            InitializeComponent();

            SelectCommand();

            // Enable progress reporting
            backgroundWorker.WorkerReportsProgress = true;

            // Hook up event handlers
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            label1.Text = "";
            this.Show();
            backgroundWorker.RunWorkerAsync();

            complete = ExecuteCommand();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            do
            {
                //complete = ExecuteCommand();
                i++;
                int percent = i;
                Thread.Sleep(50);
                backgroundWorker.ReportProgress(percent, i);
            }
            while (i < 100);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = progressBar1.Value.ToString() + "%";
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO: do something with final calculation.
            label1.Text = "Complete!";
            Thread.Sleep(5);
            this.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

        }

        private void SelectCommand()
        {
            o1 = new App_Alignment.Extensions();
            o2 = new App_Corridor.Extensions();

            o1.SelectAlignment("\nSelect Alignment with SampleLines!");
            if (o1.Alignment != null)
                o2.SelectCorridor("\nSelect Corridor!");
            else
                return;
        }

        private bool success;
        private bool ExecuteCommand()
        {
            o2.CorridorFeatureLinesTo3dPoyline(o2.Corridor);
            success = App_Sampleline.Extensions.DrawPointOnSampleLineIntersection(o1.Alignment, o2.curvIds);
            return success;
        }
    }
}
