﻿namespace FPDL.Tools.DeployEditor
{
    partial class DeployEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deployDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.createDeploy = new System.Windows.Forms.Button();
            this.designTreeView = new System.Windows.Forms.TreeView();
            this.federationDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.federationTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deployTreeView = new System.Windows.Forms.TreeView();
            this.patternLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1305, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deployDocumentToolStripMenuItem,
            this.designToolStripMenuItem,
            this.patternLibraryToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // deployDocumentToolStripMenuItem
            // 
            this.deployDocumentToolStripMenuItem.Name = "deployDocumentToolStripMenuItem";
            this.deployDocumentToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.deployDocumentToolStripMenuItem.Text = "Deploy";
            this.deployDocumentToolStripMenuItem.Click += new System.EventHandler(this.deployDocumentToolStripMenuItem_Click);
            // 
            // designToolStripMenuItem
            // 
            this.designToolStripMenuItem.Name = "designToolStripMenuItem";
            this.designToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.designToolStripMenuItem.Text = "Design";
            this.designToolStripMenuItem.Click += new System.EventHandler(this.designToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1305, 645);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createDeploy);
            this.groupBox1.Controls.Add(this.designTreeView);
            this.groupBox1.Controls.Add(this.federationDescription);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.federationTxtBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(395, 625);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Design";
            // 
            // createDeploy
            // 
            this.createDeploy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.createDeploy.Location = new System.Drawing.Point(4, 593);
            this.createDeploy.Margin = new System.Windows.Forms.Padding(4);
            this.createDeploy.Name = "createDeploy";
            this.createDeploy.Size = new System.Drawing.Size(387, 28);
            this.createDeploy.TabIndex = 5;
            this.createDeploy.Text = "Create Deploy";
            this.createDeploy.UseVisualStyleBackColor = true;
            this.createDeploy.Click += new System.EventHandler(this.createDeploy_Click);
            // 
            // designTreeView
            // 
            this.designTreeView.Location = new System.Drawing.Point(15, 159);
            this.designTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.designTreeView.Name = "designTreeView";
            this.designTreeView.Size = new System.Drawing.Size(355, 411);
            this.designTreeView.TabIndex = 4;
            // 
            // federationDescription
            // 
            this.federationDescription.Location = new System.Drawing.Point(111, 63);
            this.federationDescription.Margin = new System.Windows.Forms.Padding(4);
            this.federationDescription.Multiline = true;
            this.federationDescription.Name = "federationDescription";
            this.federationDescription.Size = new System.Drawing.Size(220, 68);
            this.federationDescription.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Description:";
            // 
            // federationTxtBox
            // 
            this.federationTxtBox.Location = new System.Drawing.Point(111, 25);
            this.federationTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.federationTxtBox.Name = "federationTxtBox";
            this.federationTxtBox.Size = new System.Drawing.Size(220, 22);
            this.federationTxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Federation:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.deployTreeView);
            this.groupBox2.Location = new System.Drawing.Point(4, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(865, 625);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deploy";
            // 
            // deployTreeView
            // 
            this.deployTreeView.Location = new System.Drawing.Point(24, 136);
            this.deployTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.deployTreeView.Name = "deployTreeView";
            this.deployTreeView.Size = new System.Drawing.Size(816, 434);
            this.deployTreeView.TabIndex = 0;
            // 
            // patternLibraryToolStripMenuItem
            // 
            this.patternLibraryToolStripMenuItem.Name = "patternLibraryToolStripMenuItem";
            this.patternLibraryToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.patternLibraryToolStripMenuItem.Text = "Pattern Library";
            this.patternLibraryToolStripMenuItem.Click += new System.EventHandler(this.patternLibraryToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Pattern Library Loaded";
            // 
            // DeployEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 673);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DeployEditor";
            this.Text = "Deploy Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem deployDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem designToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView designTreeView;
        private System.Windows.Forms.TextBox federationDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox federationTxtBox;
        private System.Windows.Forms.Button createDeploy;
        private System.Windows.Forms.TreeView deployTreeView;
        private System.Windows.Forms.ToolStripMenuItem patternLibraryToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}

