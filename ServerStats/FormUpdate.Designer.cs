namespace Stats
{
    partial class FormUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdate));
            this.buttonCancelUpdating = new System.Windows.Forms.Button();
            this.labelUpdating = new System.Windows.Forms.Label();
            this.labelDots = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonCancelUpdating
            // 
            resources.ApplyResources(this.buttonCancelUpdating, "buttonCancelUpdating");
            this.buttonCancelUpdating.Name = "buttonCancelUpdating";
            this.buttonCancelUpdating.UseVisualStyleBackColor = true;
            this.buttonCancelUpdating.Click += new System.EventHandler(this.buttonCancelUpdating_Click);
            // 
            // labelUpdating
            // 
            resources.ApplyResources(this.labelUpdating, "labelUpdating");
            this.labelUpdating.Name = "labelUpdating";
            // 
            // labelDots
            // 
            resources.ApplyResources(this.labelDots, "labelDots");
            this.labelDots.Name = "labelDots";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormUpdate
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDots);
            this.Controls.Add(this.labelUpdating);
            this.Controls.Add(this.buttonCancelUpdating);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUpdate";
            this.Load += new System.EventHandler(this.FormUpdate_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUpdate_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancelUpdating;
        private System.Windows.Forms.Label labelUpdating;
        private System.Windows.Forms.Label labelDots;
        private System.Windows.Forms.Timer timer1;
    }
}