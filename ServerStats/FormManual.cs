using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Stats
{
    /// <summary>
    /// Form viewing manul form rtf file.
    /// </summary>
    public partial class FormManual : Form
    {
        #region constructor

        /// <summary>
        /// Initializes a new instance of the FormPreferences class.
        /// </summary>
        public FormManual()
        {
            InitializeComponent();
        }

        #endregion

        #region event handlers

        /// <summary>
        /// After the form is loaded, rtf file with manual is showed in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormHelp_Load(object sender, EventArgs e)
        {
            try
            {
                string helpFile = Path.GetDirectoryName(Application.ExecutablePath) + @"\Manual.rtf";
                richTextBox1.LoadFile(helpFile);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// When the form is closed, form is hided but not disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        #endregion
    }
}