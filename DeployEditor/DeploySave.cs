using FPDL.Deploy;
using FPDL.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPDL.Tools.DeployEditor
{
    public partial class DeploySave : Form
    {
        private DeployObject deploy;
        private DesignObject design;
        internal bool singleFile = false;
        internal string fileName;

        public DeploySave(DeployObject deploy, DesignObject design)
        {
            this.deploy = deploy;
            this.design = design;

            InitializeComponent();

            if (design != null)
                fileNameTbx.Text = String.Format("{0}-{1}", design.Federation.Name, deploy.DesignReference);
            else
                fileNameTbx.Text = deploy.ConfigMgmt.DocReference.ToString();
        }

        private void applyBut_Click(object sender, EventArgs e)
        {
            if (!deploy.Verify())
            {
                if (DialogResult.Yes == MessageBox.Show("Verify error: " + deploy.VerifyResult + "\nThis Deploy file can be saved but it may not be possible to successfully parse the content.\n Click Yes to continue or No to abort", "Deploy Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Error))
                    DialogResult = DialogResult.OK;
                else
                    DialogResult = DialogResult.Cancel;
            }
            else
                DialogResult = DialogResult.OK;

            singleFile = (singleRb.Checked) ? true : false;
            fileName = fileNameTbx.Text;
            Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            singleFile = (singleRb.Checked) ? true : false;
            if (singleFile)
            {
                if (design != null)
                {
                    fileNameTbx.Text = String.Format("{0}-{1}", design.Federation.Name, deploy.DesignReference);
                    fileNameTbx.ReadOnly = false;
                }
            }
            else
            {
                fileNameTbx.Text = "[FederateName]-[Design Reference]";
                fileNameTbx.ReadOnly = true;
            }
        }
    }
}
