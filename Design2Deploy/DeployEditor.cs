using System;
using FPDL;
using FPDL.Common;
using FPDL.Design;

using System.Collections.Generic;
using System.Windows.Forms;
using FPDL.Deploy;
using FPDL.Pattern;

namespace FPDL.Tools.DeployEditor
{
    public partial class DeployEditor : Form
    {
        internal DesignObject design;
        internal DeployObject deploy;
        internal PatternLibrary library;

        // Keep a list of federates and if they have been deployed
        private Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        // Keep track of the total number of System nodes in the Deploy doc
        private int deploySystemsCount;


        public DeployEditor()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FPDL Deploy Editor\nNiteworks CDS\nVersion 0.1", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Deploy Tool Strip Menu

        private void deployDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FPDL files | *.xml";
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                try
                {
                    IFpdlObject input = FpdlReader.Parse(openFileDialog1.FileName);

                    if (input.GetType() == typeof(DeployObject))
                        deploy = (DeployObject)input;
                    else
                    {
                        MessageBox.Show("Not a Deploy file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (deploy.IsEmpty())
                    {
                        deploy = null;
                        MessageBox.Show("Deploy is empty", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    showDeployTree();
                }
                catch (ApplicationException)
                {
                    MessageBox.Show("Error parsing the file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                showDeployTree();
            }
        }

        #endregion

        #region Design Tool Strip Menu

        private void designToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FPDL files | *.xml";
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                try
                {
                    IFpdlObject input = FpdlReader.Parse(openFileDialog1.FileName);

                    if (input.GetType() == typeof(DesignObject))
                        design = (DesignObject)input;
                    else
                        MessageBox.Show("Not a Design file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    foreach (Federate federate in design.Federation.Federates)
                        checkList.Add(federate.FederateName, false);
                    showDesignTree();
                }
                catch (ApplicationException)
                {
                    MessageBox.Show("Error parsing the file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        #region Pattern Library Tool Strip Menu

        private void patternLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FPDL files | *.xml";
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                try
                {
                    IFpdlObject input = FpdlReader.Parse(openFileDialog1.FileName);

                    if (input.GetType() == typeof(PatternLibrary))
                    {
                        library = (PatternLibrary)input;
                        patternLibraryToolStripMenuItem.Visible = false;
                    }
                    else
                        MessageBox.Show("Not a Pattern Library file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (ApplicationException)
                {
                    MessageBox.Show("Error parsing the file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion


        #region Create Deploy

        private void createDeploy_Click(object sender, EventArgs e)
        {
            if (designTreeView.SelectedNode.Tag == null)
            {
                MessageBox.Show("Can't deploy a service", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (designTreeView.SelectedNode.Tag.GetType() != typeof(Federate))
                return;

            Federate federate = (Federate)designTreeView.SelectedNode.Tag;
            if (checkList[federate.FederateName])
            {
                MessageBox.Show("That federate has been deployed (edit in the Deploy pane)", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (deploy == null)
            {
                deploy = new DeployObject();

                deploy.ConfigMgmt.Initialise("PC", 1, 0, "Initial Version");
                deploy.DesignReference = design.ConfigMgmt.DocReference;
                foreach (Federate fed in design.Federation.Federates)
                {
                    if ((fed.FederateType == Enums.FederateType.gateway) || (fed.FederateType == Enums.FederateType.filter))
                        deploySystemsCount++;
                }
            }

            DeploySystem system = new DeploySystem {
                SystemType = federate.FederateType,
                FederateName = federate.FederateName
                };
            deploy.Systems.Add(system);
            showDeployTree();
        }

        #endregion

        #region Show Deploy Tree

        private void showDeployTree()
        {
            deployTreeView.Nodes.Clear();

            if (deploy == null)
                return;

            TreeNode treeRoot = new TreeNode("Deploy", DeployViewBuilder.getSystems(deploy.Systems));
            deployTreeView.Nodes.Add(treeRoot);
            deployTreeView.ExpandAll();
        }

        #endregion

        #region Show Design Tree

        private void showDesignTree()
        {
            federationTxtBox.Text = design.Federation.Name;
            federationDescription.Text = design.Federation.Summary;

            designTreeView.Nodes.Clear();

            TreeNode federation = new TreeNode(design.Federation.Name, DesignViewBuilder.getFederates(design.Federation.Federates));
            federation.Tag = "ROOT";
            designTreeView.Nodes.Add(federation);
            designTreeView.ExpandAll();
        }


        #endregion


    }
}


