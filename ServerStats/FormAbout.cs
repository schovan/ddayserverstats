using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Stats
{
    /// <summary>
    /// Form showing the informations about this program.
    /// </summary>
    public partial class FormAbout : Form
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the FormAbout class.
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
            Assembly asm = Assembly.GetExecutingAssembly();
            labelVersion.Text += asm.GetName().Version.ToString();
        }

        #endregion

        #region event handlers

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}