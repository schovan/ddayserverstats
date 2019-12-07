using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

namespace Stats
{
    public partial class FormUpdate : Form
    {
        #region delegates

        public delegate void SetResultDelegate(string result);

        #endregion

        #region public members

        public readonly SetResultDelegate setResultDelegate;

        #endregion

        #region private constants

        private const int MaxDotCount = 30;

        #endregion

        #region private members

        private Settings settings;
        private string content;
        private Thread updateThread;
        private int dotCounter = 1;

        #endregion

        #region constructor

        public FormUpdate(Settings settings, string content)
        {
            this.settings = settings;
            this.content = content;
            this.updateThread = new Thread(new ThreadStart(UpdateThreadMethod));
            this.setResultDelegate = new SetResultDelegate(SetResult);
            InitializeComponent();
        }

        #endregion

        #region event handlers

        private void FormUpdate_Load(object sender, EventArgs e)
        {
            updateThread.Start();
            timer1.Enabled = true;
        }

        private void FormUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (updateThread.IsAlive)
            {
                updateThread.Abort();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dotCounter == MaxDotCount)
            {
                dotCounter = 1;
                labelDots.Text = ".";
            }
            else
            {
                dotCounter++;
                labelDots.Text += ".";
            }
        }

        private void buttonCancelUpdating_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region private methods

        private void SetResult(string result)
        {
            timer1.Enabled = false;
            labelDots.Text = string.Empty;
            labelUpdating.Text = string.Format("{0}.", result);
            buttonCancelUpdating.Text = "OK";
        }

        private void UpdateThreadMethod()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(settings.Url);
                request.Timeout = Timeout.Infinite;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = content.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(Encoding.ASCII.GetBytes(content), 0, content.Length);
                requestStream.Flush();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                this.Invoke(this.setResultDelegate, sr.ReadToEnd());
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                this.Invoke(this.setResultDelegate, ex.Message);
            }
        }

        #endregion
    }
}