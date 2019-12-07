using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;

namespace Stats
{
    /// <summary>
    /// Form showing progress bar while a log file is parsed. It also have a private method that provides the parsing called after the form is loaded.
    /// </summary>
    public partial class FormParser : Form
    {
        #region delegates

        public delegate void SetProgressBarMaximumDelegate(int maximum);
        public delegate void SetProgressDelegate(int progress, int percentage);
        public delegate void FinishDelegate();

        #endregion

        #region public members

        public readonly SetProgressBarMaximumDelegate setProgressBarMaximumDelegate;
        public readonly SetProgressDelegate setProgressDelegate;
        public readonly FinishDelegate finish;

        #endregion

        #region private members

        private string fileName;
        private Settings settings;
        private LogParser logParser;
        private Thread parseThread;
        private bool abortParseThread = true;

        #endregion

        #region properties

        public LogParser Parser
        {
            get
            {
                return logParser;
            }
        }

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the FormParser class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="fileName"></param>
        public FormParser(Settings settings, string fileName)
        {
            this.settings = settings;
            this.fileName = fileName;
            this.logParser = new LogParser(this, settings, fileName);
            this.parseThread = new Thread(new ThreadStart(logParser.Parse));
            this.setProgressBarMaximumDelegate = new SetProgressBarMaximumDelegate(SetProgressBarMaximum);
            this.setProgressDelegate = new SetProgressDelegate(SetProgress);
            this.finish = new FinishDelegate(Finish);
            InitializeComponent();
            Assembly asm = Assembly.GetExecutingAssembly();
            this.Text += string.Format(" {0}", asm.GetName().Version.ToString());
        }

        #endregion

        #region private methods

        private void SetProgressBarMaximum(int maximum)
        {
            progressBar1.Maximum = maximum;
        }

        private void SetProgress(int progress, int percentage)
        {
            progressBar1.Value = progress;
            labelPercentage.Text = String.Format("{0,2} %", percentage);
        }

        private void Finish()
        {
            abortParseThread = false;
            Close();
        }

        #endregion

        #region event handlers

        /// <summary>
        /// After the form is loaded, Parse method is invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormParser_Load(object sender, EventArgs e)
        {
            parseThread.Start();
        }

        /// <summary>
        /// When the form is closed, Parse method is aborted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormParser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (abortParseThread)
            {
                logParser = null;
                if (parseThread.IsAlive)
                {
                    parseThread.Abort();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}