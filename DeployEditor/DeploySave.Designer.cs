namespace FPDL.Tools.DeployEditor
{
    partial class DeploySave
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
            this.label4 = new System.Windows.Forms.Label();
            this.fileNameTbx = new System.Windows.Forms.TextBox();
            this.okBut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.multipleRb = new System.Windows.Forms.RadioButton();
            this.singleRb = new System.Windows.Forms.RadioButton();
            this.cancelBut = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Base filename:";
            // 
            // fileNameTbx
            // 
            this.fileNameTbx.Location = new System.Drawing.Point(135, 108);
            this.fileNameTbx.Name = "fileNameTbx";
            this.fileNameTbx.Size = new System.Drawing.Size(238, 24);
            this.fileNameTbx.TabIndex = 7;
            // 
            // okBut
            // 
            this.okBut.Location = new System.Drawing.Point(60, 169);
            this.okBut.Name = "okBut";
            this.okBut.Size = new System.Drawing.Size(85, 35);
            this.okBut.TabIndex = 8;
            this.okBut.Text = "OK";
            this.okBut.UseVisualStyleBackColor = true;
            this.okBut.Click += new System.EventHandler(this.applyBut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.multipleRb);
            this.groupBox1.Controls.Add(this.singleRb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save options";
            // 
            // multipleRb
            // 
            this.multipleRb.AutoSize = true;
            this.multipleRb.Location = new System.Drawing.Point(229, 48);
            this.multipleRb.Name = "multipleRb";
            this.multipleRb.Size = new System.Drawing.Size(109, 22);
            this.multipleRb.TabIndex = 1;
            this.multipleRb.Text = "Multiple files";
            this.multipleRb.UseVisualStyleBackColor = true;
            this.multipleRb.CheckedChanged += new System.EventHandler(this.Rb_CheckedChanged);
            // 
            // singleRb
            // 
            this.singleRb.AutoSize = true;
            this.singleRb.Checked = true;
            this.singleRb.Location = new System.Drawing.Point(86, 48);
            this.singleRb.Name = "singleRb";
            this.singleRb.Size = new System.Drawing.Size(91, 22);
            this.singleRb.TabIndex = 0;
            this.singleRb.TabStop = true;
            this.singleRb.Text = "Single file";
            this.singleRb.UseVisualStyleBackColor = true;
            this.singleRb.CheckedChanged += new System.EventHandler(this.Rb_CheckedChanged);
            // 
            // cancelBut
            // 
            this.cancelBut.Location = new System.Drawing.Point(263, 169);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(85, 35);
            this.cancelBut.TabIndex = 10;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            // 
            // DeploySave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 220);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.okBut);
            this.Controls.Add(this.fileNameTbx);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeploySave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save Deploy files";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fileNameTbx;
        private System.Windows.Forms.Button okBut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton multipleRb;
        private System.Windows.Forms.RadioButton singleRb;
        private System.Windows.Forms.Button cancelBut;
    }
}