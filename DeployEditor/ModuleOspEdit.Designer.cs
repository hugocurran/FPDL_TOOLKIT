namespace FPDL.Tools.DeployEditor
{
    partial class ModuleOspEdit
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
            this.label4 = new System.Windows.Forms.Label();
            this.inputTbx = new System.Windows.Forms.TextBox();
            this.outputTbx = new System.Windows.Forms.TextBox();
            this.applyBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.pathLbx = new System.Windows.Forms.ListBox();
            this.protoLbx = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path:";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Input Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Output Port:";
            // 
            // inputTbx
            // 
            this.inputTbx.Location = new System.Drawing.Point(135, 77);
            this.inputTbx.Name = "inputTbx";
            this.inputTbx.Size = new System.Drawing.Size(238, 24);
            this.inputTbx.TabIndex = 6;
            // 
            // outputTbx
            // 
            this.outputTbx.Location = new System.Drawing.Point(135, 108);
            this.outputTbx.Name = "outputTbx";
            this.outputTbx.Size = new System.Drawing.Size(238, 24);
            this.outputTbx.TabIndex = 7;
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
            // pathLbx
            // 
            this.pathLbx.FormattingEnabled = true;
            this.pathLbx.ItemHeight = 18;
            this.pathLbx.Items.AddRange(new object[] {
            "ExportPath",
            "ImportPath"});
            this.pathLbx.Location = new System.Drawing.Point(135, 18);
            this.pathLbx.Name = "pathLbx";
            this.pathLbx.Size = new System.Drawing.Size(238, 22);
            this.pathLbx.TabIndex = 10;
            // 
            // protoLbx
            // 
            this.protoLbx.FormattingEnabled = true;
            this.protoLbx.ItemHeight = 18;
            this.protoLbx.Items.AddRange(new object[] {
            "HPSD_TCP",
            "HPSD_ZMQ",
            "WebLVC_TCP",
            "WebLVC_ZMQ"});
            this.protoLbx.Location = new System.Drawing.Point(135, 46);
            this.protoLbx.Name = "protoLbx";
            this.protoLbx.Size = new System.Drawing.Size(238, 22);
            this.protoLbx.TabIndex = 11;
            // 
            // ModuleOspEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(433, 220);
            this.Controls.Add(this.protoLbx);
            this.Controls.Add(this.pathLbx);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyBut);
            this.Controls.Add(this.outputTbx);
            this.Controls.Add(this.inputTbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModuleOspEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Serialisation Protocol (OSP) Module";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inputTbx;
        private System.Windows.Forms.TextBox outputTbx;
        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.Button cancelBut;
        private System.Windows.Forms.ListBox pathLbx;
        private System.Windows.Forms.ListBox protoLbx;
    }
}