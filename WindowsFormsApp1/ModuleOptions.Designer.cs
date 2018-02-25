namespace FPDL.Tools.PatternEditor
{
    partial class ModuleOptions
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
            this.applyBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.specLbx = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // applyBut
            // 
            this.applyBut.Location = new System.Drawing.Point(148, 270);
            this.applyBut.Margin = new System.Windows.Forms.Padding(4);
            this.applyBut.Name = "applyBut";
            this.applyBut.Size = new System.Drawing.Size(94, 35);
            this.applyBut.TabIndex = 2;
            this.applyBut.Text = "Apply";
            this.applyBut.UseVisualStyleBackColor = true;
            // 
            // cancelBut
            // 
            this.cancelBut.Location = new System.Drawing.Point(335, 270);
            this.cancelBut.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(94, 35);
            this.cancelBut.TabIndex = 3;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            // 
            // specLbx
            // 
            this.specLbx.FormattingEnabled = true;
            this.specLbx.ItemHeight = 22;
            this.specLbx.Location = new System.Drawing.Point(13, 13);
            this.specLbx.Name = "specLbx";
            this.specLbx.Size = new System.Drawing.Size(529, 202);
            this.specLbx.TabIndex = 4;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(182, 36);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 28);
            // 
            // ModuleOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 328);
            this.Controls.Add(this.specLbx);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyBut);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ModuleOptions";
            this.Text = "ModuleOptions";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.ListBox specLbx;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}