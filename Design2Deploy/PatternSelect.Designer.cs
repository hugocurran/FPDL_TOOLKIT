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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.patternTypeLabel.Location = new System.Drawing.Point(30, 9);
            this.patternTypeLabel.Name = "patternTypeLabel";
            this.patternTypeLabel.Size = new System.Drawing.Size(116, 17);
            this.patternTypeLabel.TabIndex = 0;
            this.patternTypeLabel.Text = "Patterns of type :";
            // 
            // applyPatternSelect
            // 
            this.applyPatternSelect.Location = new System.Drawing.Point(230, 242);
            this.applyPatternSelect.Name = "applyPatternSelect";
            this.applyPatternSelect.Size = new System.Drawing.Size(75, 23);
            this.applyPatternSelect.TabIndex = 2;
            this.applyPatternSelect.Text = "Apply";
            this.applyPatternSelect.UseVisualStyleBackColor = true;
            this.applyPatternSelect.Click += new System.EventHandler(this.applyPatternSelect_Click);
            // 
            // cancelPatternSelect
            // 
            this.cancelPatternSelect.Location = new System.Drawing.Point(428, 242);
            this.cancelPatternSelect.Name = "cancelPatternSelect";
            this.cancelPatternSelect.Size = new System.Drawing.Size(75, 23);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.patternListGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.patternListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternListGrid.Location = new System.Drawing.Point(0, 0);
            this.patternListGrid.MultiSelect = false;
            this.patternListGrid.Name = "patternListGrid";
            this.patternListGrid.ReadOnly = true;
            this.patternListGrid.RowTemplate.Height = 24;
            this.patternListGrid.Size = new System.Drawing.Size(695, 176);
            this.patternListGrid.TabIndex = 4;
            this.patternListGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            this.panel1.Location = new System.Drawing.Point(13, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 176);
            this.panel1.TabIndex = 5;
            // 
            // PatternSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 279);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelPatternSelect);
            this.Controls.Add(this.applyPatternSelect);
            this.Controls.Add(this.patternTypeLabel);
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