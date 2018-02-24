namespace FPDL.Tools.DeployEditor
{
    partial class ModuleInterfaceEdit
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
            this.interfaceNameTbx = new System.Windows.Forms.TextBox();
            this.ipAddressTbx = new System.Windows.Forms.TextBox();
            this.netPrefixTbx = new System.Windows.Forms.TextBox();
            this.defaultRouterTbx = new System.Windows.Forms.TextBox();
            this.applyBut = new System.Windows.Forms.Button();
            this.cancelBut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Interface Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "IP Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Network Prefix:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Default Router:";
            // 
            // interfaceNameTbx
            // 
            this.interfaceNameTbx.Location = new System.Drawing.Point(134, 15);
            this.interfaceNameTbx.Name = "interfaceNameTbx";
            this.interfaceNameTbx.Size = new System.Drawing.Size(239, 24);
            this.interfaceNameTbx.TabIndex = 4;
            // 
            // ipAddressTbx
            // 
            this.ipAddressTbx.Location = new System.Drawing.Point(134, 46);
            this.ipAddressTbx.Name = "ipAddressTbx";
            this.ipAddressTbx.Size = new System.Drawing.Size(239, 24);
            this.ipAddressTbx.TabIndex = 5;
            // 
            // netPrefixTbx
            // 
            this.netPrefixTbx.Location = new System.Drawing.Point(135, 77);
            this.netPrefixTbx.Name = "netPrefixTbx";
            this.netPrefixTbx.Size = new System.Drawing.Size(238, 24);
            this.netPrefixTbx.TabIndex = 6;
            // 
            // defaultRouterTbx
            // 
            this.defaultRouterTbx.Location = new System.Drawing.Point(135, 108);
            this.defaultRouterTbx.Name = "defaultRouterTbx";
            this.defaultRouterTbx.Size = new System.Drawing.Size(238, 24);
            this.defaultRouterTbx.TabIndex = 7;
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
            // ModuleInterfaceEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBut;
            this.ClientSize = new System.Drawing.Size(433, 220);
            this.Controls.Add(this.cancelBut);
            this.Controls.Add(this.applyBut);
            this.Controls.Add(this.defaultRouterTbx);
            this.Controls.Add(this.netPrefixTbx);
            this.Controls.Add(this.ipAddressTbx);
            this.Controls.Add(this.interfaceNameTbx);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModuleInterfaceEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface Module";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox interfaceNameTbx;
        private System.Windows.Forms.TextBox ipAddressTbx;
        private System.Windows.Forms.TextBox netPrefixTbx;
        private System.Windows.Forms.TextBox defaultRouterTbx;
        private System.Windows.Forms.Button applyBut;
        private System.Windows.Forms.Button cancelBut;
    }
}