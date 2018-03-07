namespace FPDL.Tools.DeployEditor
{
    partial class SpecEditor
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
            this.applyBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.paramCbx = new System.Windows.Forms.ComboBox();
            this.valueTbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.readOnlyCk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // applyBut
            // 
            this.applyBut.Location = new System.Drawing.Point(63, 103);
            this.applyBut.Name = "applyBut";
            this.applyBut.Size = new System.Drawing.Size(94, 35);
            this.applyBut.TabIndex = 0;
            this.applyBut.Text = "Apply";
            this.applyBut.UseVisualStyleBackColor = true;
            this.applyBut.Click += new System.EventHandler(this.apply_Click);
            // 
            // cancelBut
            // 
            this.cancelBut.Location = new System.Drawing.Point(295, 103);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(94, 35);
            this.cancelBut.TabIndex = 1;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancel_Click);
            // 
            // paramCbx
            // 
            this.paramCbx.FormattingEnabled = true;
            this.paramCbx.Location = new System.Drawing.Point(12, 42);
            this.paramCbx.Name = "paramCbx";
            this.paramCbx.Size = new System.Drawing.Size(145, 30);
            this.paramCbx.TabIndex = 2;
            // 
            // valueTbx
            // 
            this.valueTbx.Location = new System.Drawing.Point(164, 42);
            this.valueTbx.Name = "valueTbx";
            this.valueTbx.Size = new System.Drawing.Size(176, 28);
            this.valueTbx.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Parameter:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value:";
            // 
            // readOnlyCk
            // 
            this.readOnlyCk.AutoSize = true;
            this.readOnlyCk.Location = new System.Drawing.Point(346, 43);
            this.readOnlyCk.Name = "readOnlyCk";
            this.readOnlyCk.Size = new System.Drawing.Size(121, 28);
            this.readOnlyCk.TabIndex = 6;
            this.readOnlyCk.Text = "Read Only";
            this.readOnlyCk.UseVisualStyleBackColor = true;
            // 
            // SpecEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 152);
            this.Controls.Add(this.readOnlyCk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.valueTbx);
            this.Controls.Add(this.paramCbx);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyBut);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpecEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SpecThingy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.ComboBox paramCbx;
        private System.Windows.Forms.TextBox valueTbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox readOnlyCk;
    }
}