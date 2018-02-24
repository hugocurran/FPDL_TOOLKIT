namespace FPDL.Tools.DeployEditor
{
    partial class ModuleExtensionEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.vendorTbx = new System.Windows.Forms.TextBox();
            this.applyBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.specDGView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.specDGView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "OSP Protocol:";
            // 
            // vendorTbx
            // 
            this.vendorTbx.Location = new System.Drawing.Point(135, 15);
            this.vendorTbx.Name = "vendorTbx";
            this.vendorTbx.Size = new System.Drawing.Size(238, 24);
            this.vendorTbx.TabIndex = 6;
            // 
            // applyBut
            // 
            this.applyBut.Location = new System.Drawing.Point(60, 169);
            this.applyBut.Name = "applyBut";
            this.applyBut.Size = new System.Drawing.Size(85, 35);
            this.applyBut.TabIndex = 8;
            this.applyBut.Text = "Apply";
            this.applyBut.UseVisualStyleBackColor = true;
            this.applyBut.Click += new System.EventHandler(this.applyBut_Click);
            // 
            // cancelBut
            // 
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.Location = new System.Drawing.Point(277, 169);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(83, 35);
            this.cancelBut.TabIndex = 9;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // specDGView
            // 
            this.specDGView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.specDGView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.specDGView.Location = new System.Drawing.Point(135, 49);
            this.specDGView.Name = "specDGView";
            this.specDGView.RowTemplate.Height = 24;
            this.specDGView.Size = new System.Drawing.Size(238, 114);
            this.specDGView.TabIndex = 10;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Parameter Name";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Parameter Value";
            this.Column2.Name = "Column2";
            // 
            // ModuleExtensionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(433, 220);
            this.Controls.Add(this.specDGView);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyBut);
            this.Controls.Add(this.vendorTbx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModuleExtensionEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vendor Extensions Module";
            ((System.ComponentModel.ISupportInitialize)(this.specDGView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox vendorTbx;
        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.DataGridView specDGView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}