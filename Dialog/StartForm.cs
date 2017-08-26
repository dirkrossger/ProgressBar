using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace ProgressBar_Dialog
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Startet den BackgroundWorker
        /// </summary>
        private void startButton_Click(object sender, EventArgs e)
        {
            // BackgroundWorker starten
            this.backgroundWorker.RunWorkerAsync(
               this.maxNumberTextBox.Text);

            // Start-Schalter deaktivieren und
            // Abbrechen-Schalter aktivieren
            this.startButton.Enabled = false;
            this.cancelButton.Enabled = true;
        }

        /// <summary>
        /// Bricht die Ausführung der Thread-Methode ab
        /// </summary>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Abbrechen des aktuellen Thread
            this.backgroundWorker.CancelAsync();
        }

        /// <summary>
        /// Die Thread-Methode
        /// </summary>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Argument auslesen
            long maxNumber = 0;
            try
            {
                maxNumber = Convert.ToInt64(e.Argument);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Das Argument der Thread-Methode " +
                   "muss eine positive Ganzzahl sein: " + ex.Message);
            }

            long result = 0;
            for (long number = 1; number <= maxNumber; number++)
            {
                // Abfragen, ob abgebrochen werden soll
                if (this.backgroundWorker.CancellationPending)
                {
                    // Angeben, dass abgebrochen wurde
                    e.Cancel = true;

                    // und raus aus der Methode 
                    return;
                }

                // Berechnung ausführen
                result += number;

                // Thread kurz anhalten
                Thread.Sleep(20);

                // Den Fortschritt melden
                int percent = (int)((number / (float)maxNumber) * 100);
                this.backgroundWorker.ReportProgress(percent, number);
            }

            // Das Ergebnis zurückgeben
            e.Result = result;
        }


        /// <summary>
        /// Wird vom BackgroundWorker aufgerufen wenn der Fortschritt geändert wurde
        /// </summary>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // ProgressBar und Label aktualisieren
            this.progressBar.Value = e.ProgressPercentage;
            this.resultLabel.Text = e.UserState.ToString();
        }


        /// <summary>
        /// Wird vom BackgroundWorker aufgerufen nachdem die Thread-Methode beendet wurde
        /// </summary>
        private void backgroundWorker_RunWorkerCompleted(object sender,
           RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // In der Thread-Methode ist ein Fehler aufgetreten
                MessageBox.Show(e.Error.Message, Application.ProductName,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled == false)
            {
                // Der Thread wurde abgebrochen
                this.resultLabel.Text =
                   "Fertig. Das Ergebnis ist: " + e.Result.ToString();
            }
            else
            {
                // Der Thread wurde normal beendet
                this.resultLabel.Text = "Abgebrochen";
            }

            // Start-Schalter aktivieren und
            // Abbrechen-Schalter deaktivieren
            this.startButton.Enabled = true;
            this.cancelButton.Enabled = false;
        }

    }
}
