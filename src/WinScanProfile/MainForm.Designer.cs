namespace WinScanProfile
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.lsvProfiles = new System.Windows.Forms.ListView();
            this.lsvProfiles_colProfile = new System.Windows.Forms.ColumnHeader();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContextEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContextSetDefault = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Size = new System.Drawing.Size(462, 25);
            this.mnu.TabIndex = 0;
            // 
            // lsvProfiles
            // 
            this.lsvProfiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvProfiles_colProfile});
            this.lsvProfiles.ContextMenuStrip = this.mnuContext;
            this.lsvProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvProfiles.FullRowSelect = true;
            this.lsvProfiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvProfiles.HideSelection = false;
            this.lsvProfiles.LabelEdit = true;
            this.lsvProfiles.Location = new System.Drawing.Point(0, 25);
            this.lsvProfiles.MultiSelect = false;
            this.lsvProfiles.Name = "lsvProfiles";
            this.lsvProfiles.Size = new System.Drawing.Size(462, 328);
            this.lsvProfiles.TabIndex = 1;
            this.lsvProfiles.UseCompatibleStateImageBehavior = false;
            this.lsvProfiles.View = System.Windows.Forms.View.Details;
            this.lsvProfiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lsvProfiles_AfterLabelEdit);
            this.lsvProfiles.ItemActivate += new System.EventHandler(this.lsvProfiles_ItemActivate);
            this.lsvProfiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lsvProfiles_KeyDown);
            // 
            // lsvProfiles_colProfile
            // 
            this.lsvProfiles_colProfile.Text = "Profile";
            this.lsvProfiles_colProfile.Width = 240;
            // 
            // mnuContext
            // 
            this.mnuContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContextEdit,
            this.mnuContextSetDefault});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(153, 52);
            this.mnuContext.Opening += new System.ComponentModel.CancelEventHandler(this.mnuContext_Opening);
            // 
            // mnuContextEdit
            // 
            this.mnuContextEdit.Name = "mnuContextEdit";
            this.mnuContextEdit.Size = new System.Drawing.Size(152, 24);
            this.mnuContextEdit.Text = "&Edit...";
            this.mnuContextEdit.Click += new System.EventHandler(this.mnuContextEdit_Click);
            // 
            // mnuContextSetDefault
            // 
            this.mnuContextSetDefault.Name = "mnuContextSetDefault";
            this.mnuContextSetDefault.Size = new System.Drawing.Size(152, 24);
            this.mnuContextSetDefault.Text = "Set &Default";
            this.mnuContextSetDefault.Click += new System.EventHandler(this.mnuContextSetDefault_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 353);
            this.Controls.Add(this.lsvProfiles);
            this.Controls.Add(this.mnu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(480, 400);
            this.Name = "MainForm";
            this.Text = "WinScan Profile";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ListView lsvProfiles;
        private System.Windows.Forms.ColumnHeader lsvProfiles_colProfile;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContextEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuContextSetDefault;
    }
}

