using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;


namespace ProgressBar_TaskForm
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            // Enable progress reporting
            backgroundWorker.WorkerReportsProgress = true;

            // Hook up event handlers
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            label1.Text = "";
            this.Show();
            backgroundWorker.RunWorkerAsync(100);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            long result = 0;
            for (int i = 0; i < 100; i++)
            {
                result += i;
                int percent = i;
                Thread.Sleep(20);
                backgroundWorker.ReportProgress(percent, i);
            }
            e.Result = result;
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
    }
}
