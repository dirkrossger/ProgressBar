using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;


namespace ProgressBar_TaskForm
{
    public partial class Form1 : Form
    {
        public BackgroundWorker BackgroundProcess;

        public Form1()
        {
            InitializeComponent();

            this.BackgroundProcess = backgroundWorker;

            label1.Text = "";

            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 1;
            //backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            for (int j = 0; j < 100; j++)
            {
                Thread.Sleep(5);
                Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
                doc.Editor.WriteMessage("\n Lauf:" + j);
                //while (this.backgroundWorker.IsBusy)
                //{
                //}

                backgroundWorker.ReportProgress(j);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = progressBar1.Value.ToString();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // TODO: do something with final calculation.
            label1.Text = "Complete!";
            Thread.Sleep(5);
            this.Close();
        }
    }
}
