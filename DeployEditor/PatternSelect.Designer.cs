namespace FPDL.Tools.DeployEditor
{
    partial class PatternSelect
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
            this.patternTypeLabel = new System.Windows.Forms.Label();
            this.applyPatternSelect = new System.Windows.Forms.Button();
            this.cancelPatternSelect = new System.Windows.Forms.Button();
            this.patternListGrid = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatternName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Version = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatternRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.patternListGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // patternTypeLabel
            // 
            this.patternTypeLabel.AutoSize = true;
            this.patternTypeLabel.Location = new System.Drawing.Point(38, 11);
            this.patternTypeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.patternTypeLabel.Name = "patternTypeLabel";
            this.patternTypeLabel.Size = new System.Drawing.Size(137, 20);
            this.patternTypeLabel.TabIndex = 0;
            this.patternTypeLabel.Text = "Patterns of type :";
            // 
            // applyPatternSelect
            // 
            this.applyPatternSelect.Location = new System.Drawing.Point(288, 302);
            this.applyPatternSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.applyPatternSelect.Name = "applyPatternSelect";
            this.applyPatternSelect.Size = new System.Drawing.Size(94, 29);
            this.applyPatternSelect.TabIndex = 2;
            this.applyPatternSelect.Text = "Apply";
            this.applyPatternSelect.UseVisualStyleBackColor = true;
            this.applyPatternSelect.Click += new System.EventHandler(this.applyPatternSelect_Click);
            // 
            // cancelPatternSelect
            // 
            this.cancelPatternSelect.Location = new System.Drawing.Point(535, 302);
            this.cancelPatternSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelPatternSelect.Name = "cancelPatternSelect";
            this.cancelPatternSelect.Size = new System.Drawing.Size(94, 29);
            this.cancelPatternSelect.TabIndex = 3;
            this.cancelPatternSelect.Text = "Cancel";
            this.cancelPatternSelect.UseVisualStyleBackColor = true;
            this.cancelPatternSelect.Click += new System.EventHandler(this.cancelPatternSelect_Click);
            // 
            // patternListGrid
            // 
            this.patternListGrid.AllowUserToAddRows = false;
            this.patternListGrid.AllowUserToDeleteRows = false;
            this.patternListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.patternListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.patternListGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.PatternName,
            this.Version,
            this.PatternRef});
            this.patternListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternListGrid.Location = new System.Drawing.Point(0, 0);
            this.patternListGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.patternListGrid.MultiSelect = false;
            this.patternListGrid.Name = "patternListGrid";
            this.patternListGrid.ReadOnly = true;
            this.patternListGrid.RowTemplate.Height = 24;
            this.patternListGrid.Size = new System.Drawing.Size(869, 220);
            this.patternListGrid.TabIndex = 4;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // PatternName
            // 
            this.PatternName.HeaderText = "PatternName";
            this.PatternName.Name = "PatternName";
            this.PatternName.ReadOnly = true;
            // 
            // Version
            // 
            this.Version.HeaderText = "Version";
            this.Version.Name = "Version";
            this.Version.ReadOnly = true;
            // 
            // PatternRef
            // 
            this.PatternRef.HeaderText = "PatternReference";
            this.PatternRef.Name = "PatternRef";
            this.PatternRef.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.patternListGrid);
            this.panel1.Location = new System.Drawing.Point(16, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 220);
            this.panel1.TabIndex = 5;
            // 
            // PatternSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 349);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelPatternSelect);
            this.Controls.Add(this.applyPatternSelect);
            this.Controls.Add(this.patternTypeLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PatternSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatternSelect";
            ((System.ComponentModel.ISupportInitialize)(this.patternListGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label patternTypeLabel;
        private System.Windows.Forms.Button applyPatternSelect;
        private System.Windows.Forms.Button cancelPatternSelect;
        private System.Windows.Forms.DataGridView patternListGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatternName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Version;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatternRef;
        private System.Windows.Forms.Panel panel1;
    }
}