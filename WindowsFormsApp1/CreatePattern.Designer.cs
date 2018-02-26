namespace FPDL.Tools.PatternEditor
{
    partial class CreatePattern
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
            this.label3 = new System.Windows.Forms.Label();
            this.patternNameTbx = new System.Windows.Forms.TextBox();
            this.applyBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.patternTypeCbx = new System.Windows.Forms.ComboBox();
            this.patternDescriptionRtbx = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pattern Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pattern Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Description:";
            // 
            // patternNameTbx
            // 
            this.patternNameTbx.Location = new System.Drawing.Point(149, 18);
            this.patternNameTbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.patternNameTbx.Name = "patternNameTbx";
            this.patternNameTbx.Size = new System.Drawing.Size(265, 28);
            this.patternNameTbx.TabIndex = 4;
            // 
            // applyBut
            // 
            this.applyBut.Location = new System.Drawing.Point(63, 262);
            this.applyBut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.applyBut.Name = "applyBut";
            this.applyBut.Size = new System.Drawing.Size(94, 43);
            this.applyBut.TabIndex = 8;
            this.applyBut.Text = "Create";
            this.applyBut.UseVisualStyleBackColor = true;
            this.applyBut.Click += new System.EventHandler(this.applyBut_Click);
            // 
            // cancelBut
            // 
            this.cancelBut.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBut.Location = new System.Drawing.Point(310, 262);
            this.cancelBut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cancelBut.Name = "cancelBut";
            this.cancelBut.Size = new System.Drawing.Size(92, 43);
            this.cancelBut.TabIndex = 9;
            this.cancelBut.Text = "Cancel";
            this.cancelBut.UseVisualStyleBackColor = true;
            this.cancelBut.Click += new System.EventHandler(this.cancelBut_Click);
            // 
            // patternTypeCbx
            // 
            this.patternTypeCbx.FormattingEnabled = true;
            this.patternTypeCbx.Items.AddRange(new object[] {
            "LTG",
            "MTG",
            "HTG",
            "Filter"});
            this.patternTypeCbx.Location = new System.Drawing.Point(149, 56);
            this.patternTypeCbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.patternTypeCbx.Name = "patternTypeCbx";
            this.patternTypeCbx.Size = new System.Drawing.Size(265, 30);
            this.patternTypeCbx.TabIndex = 10;
            this.patternTypeCbx.Text = "Select pattern type";
            // 
            // patternDescriptionRtbx
            // 
            this.patternDescriptionRtbx.Location = new System.Drawing.Point(149, 98);
            this.patternDescriptionRtbx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.patternDescriptionRtbx.Name = "patternDescriptionRtbx";
            this.patternDescriptionRtbx.Size = new System.Drawing.Size(265, 156);
            this.patternDescriptionRtbx.TabIndex = 11;
            this.patternDescriptionRtbx.Text = "";
            // 
            // CreatePattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(481, 356);
            this.Controls.Add(this.patternDescriptionRtbx);
            this.Controls.Add(this.patternTypeCbx);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyBut);
            this.Controls.Add(this.patternNameTbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePattern";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Pattern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox patternNameTbx;
        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.ComboBox patternTypeCbx;
        private System.Windows.Forms.RichTextBox patternDescriptionRtbx;
    }
}