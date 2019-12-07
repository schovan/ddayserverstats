namespace Stats
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxKills = new System.Windows.Forms.TextBox();
            this.checkBoxKills = new System.Windows.Forms.CheckBox();
            this.checkBoxPlusMinus = new System.Windows.Forms.CheckBox();
            this.checkBoxConnections = new System.Windows.Forms.CheckBox();
            this.checkBoxWords = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewOpen = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listViewExport = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listViewWords = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTable = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Cancel);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Ok);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxKills);
            this.tabPage1.Controls.Add(this.checkBoxKills);
            this.tabPage1.Controls.Add(this.checkBoxPlusMinus);
            this.tabPage1.Controls.Add(this.checkBoxConnections);
            this.tabPage1.Controls.Add(this.checkBoxWords);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxKills
            // 
            resources.ApplyResources(this.textBoxKills, "textBoxKills");
            this.textBoxKills.Name = "textBoxKills";
            // 
            // checkBoxKills
            // 
            resources.ApplyResources(this.checkBoxKills, "checkBoxKills");
            this.checkBoxKills.Name = "checkBoxKills";
            this.checkBoxKills.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlusMinus
            // 
            resources.ApplyResources(this.checkBoxPlusMinus, "checkBoxPlusMinus");
            this.checkBoxPlusMinus.Checked = true;
            this.checkBoxPlusMinus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlusMinus.Name = "checkBoxPlusMinus";
            this.checkBoxPlusMinus.UseVisualStyleBackColor = true;
            // 
            // checkBoxConnections
            // 
            resources.ApplyResources(this.checkBoxConnections, "checkBoxConnections");
            this.checkBoxConnections.Checked = true;
            this.checkBoxConnections.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxConnections.Name = "checkBoxConnections";
            this.checkBoxConnections.UseVisualStyleBackColor = true;
            // 
            // checkBoxWords
            // 
            resources.ApplyResources(this.checkBoxWords, "checkBoxWords");
            this.checkBoxWords.Name = "checkBoxWords";
            this.checkBoxWords.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listViewOpen);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewOpen
            // 
            this.listViewOpen.CheckBoxes = true;
            this.listViewOpen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            resources.ApplyResources(this.listViewOpen, "listViewOpen");
            this.listViewOpen.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewOpen.Name = "listViewOpen";
            this.listViewOpen.UseCompatibleStateImageBehavior = false;
            this.listViewOpen.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listViewExport);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listViewExport
            // 
            this.listViewExport.CheckBoxes = true;
            this.listViewExport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            resources.ApplyResources(this.listViewExport, "listViewExport");
            this.listViewExport.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewExport.Name = "listViewExport";
            this.listViewExport.UseCompatibleStateImageBehavior = false;
            this.listViewExport.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listViewWords);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listViewWords
            // 
            this.listViewWords.CheckBoxes = true;
            this.listViewWords.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4});
            resources.ApplyResources(this.listViewWords, "listViewWords");
            this.listViewWords.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewWords.LabelEdit = true;
            this.listViewWords.Name = "listViewWords";
            this.listViewWords.UseCompatibleStateImageBehavior = false;
            this.listViewWords.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.textBoxTable);
            this.tabPage5.Controls.Add(this.label6);
            this.tabPage5.Controls.Add(this.textBoxUrl);
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.textBoxDatabase);
            this.tabPage5.Controls.Add(this.textBoxPassword);
            this.tabPage5.Controls.Add(this.textBoxUser);
            this.tabPage5.Controls.Add(this.textBoxHost);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.label3);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage5, "tabPage5");
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // textBoxUrl
            // 
            resources.ApplyResources(this.textBoxUrl, "textBoxUrl");
            this.textBoxUrl.Name = "textBoxUrl";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxDatabase
            // 
            resources.ApplyResources(this.textBoxDatabase, "textBoxDatabase");
            this.textBoxDatabase.Name = "textBoxDatabase";
            // 
            // textBoxPassword
            // 
            resources.ApplyResources(this.textBoxPassword, "textBoxPassword");
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.UseSystemPasswordChar = true;
            // 
            // textBoxUser
            // 
            resources.ApplyResources(this.textBoxUser, "textBoxUser");
            this.textBoxUser.Name = "textBoxUser";
            // 
            // textBoxHost
            // 
            resources.ApplyResources(this.textBoxHost, "textBoxHost");
            this.textBoxHost.Name = "textBoxHost";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxTable
            // 
            resources.ApplyResources(this.textBoxTable, "textBoxTable");
            this.textBoxTable.Name = "textBoxTable";
            // 
            // FormOptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxWords;
        private System.Windows.Forms.CheckBox checkBoxConnections;
        private System.Windows.Forms.CheckBox checkBoxPlusMinus;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listViewExport;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView listViewOpen;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox checkBoxKills;
        private System.Windows.Forms.TextBox textBoxKills;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView listViewWords;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxTable;
        private System.Windows.Forms.Label label6;


    }
}