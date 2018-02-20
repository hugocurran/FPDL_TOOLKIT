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
        internal Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        // Keep track of the total number of System nodes in the Deploy doc
        private int deploySystemsCount;

        public DeployEditor()
        {
            InitializeComponent();
            createDeploy.Enabled = false;
            toolStripStatusLabel1.Text = "Pattern Library not loaded";
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
                    //showDeployTree();
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show("Error parsing the file: " + ex.Message, "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                showDeployTree();
                designToolStripMenuItem.Visible = false;
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
                    {
                        MessageBox.Show("Not a Design file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    foreach (Federate federate in design.Federation.Federates)
                    {
                        if ((federate.FederateType == Enums.FederateType.gateway) || (federate.FederateType == Enums.FederateType.filter))
                            checkList.Add(federate.FederateName, false);
                    }
                    showDesignTree();
                    createDeploy.Enabled = checkListTest();
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
                        toolStripStatusLabel1.Text = "Pattern Library loaded";
                    }
                    else
                        MessageBox.Show("Not a Pattern Library file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show("Error parsing the file: " + ex.Message, "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        #region Create Deploy

        private void createDeploy_Click(object sender, EventArgs e)
        {
            if (designTreeView.SelectedNode == null)
                return;

            if ((string)designTreeView.SelectedNode.Tag == "ROOT")      // Design tree root
            {
                if (MessageBox.Show("Deploy all federates?", "Deploy Editor", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    deploy = BuildIt.Initialise(design, "UK", "Official", this);
                    showDeployTree();
                }
            }
            createDeploy.Enabled = checkListTest();
        }


        //private void deployAdd(Federate federate)
        //{
        //    if (checkList[federate.FederateName])
        //    {
        //        MessageBox.Show("That federate has been deployed (edit in the Deploy pane)", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }
        //    if (deploy == null)
        //    {
        //        deploy = new DeployObject();

        //        deploy.ConfigMgmt.Initialise("PC", 1, 0, "Initial Version");
        //        deploy.DesignReference = design.ConfigMgmt.DocReference;
        //        foreach (Federate fed in design.Federation.Federates)
        //        {
        //            if ((fed.FederateType == Enums.FederateType.gateway) || (fed.FederateType == Enums.FederateType.filter))
        //                deploySystemsCount++;
        //        }
        //    }
        //    DeploySystem system = new DeploySystem
        //    {
        //        SystemType = (federate.FederateType == Enums.FederateType.filter) ? Enums.PatternType.filter : (Enums.PatternType)federate.GatewayType,
        //        FederateName = federate.FederateName
        //    };
        //    deploy.Systems.Add(system);
        //    checkList[federate.FederateName] = true;
        //}

        // Test if all applicable federates in design have been copied to deploy
        private bool checkListTest()
        {
            bool allDone = true;
            foreach (var entry in checkList)
            {
                if (!entry.Value)
                    allDone = false;
            }
            if (allDone)
                return false;
            else
                return true;
        }

        #endregion

        #region Show Deploy Tree

        private void showDeployTree()
        {
            deployTreeView.Nodes.Clear();

            if (deploy == null)
                return;

            deployVersionTbox.Text = ConfigMgmt.VersionToString(deploy.ConfigMgmt.CurrentVersion);
            //deployDateTbox.Text = deploy.ConfigMgmt
            deployRefTbox.Text = deploy.ConfigMgmt.DocReference.ToString();

            TreeNode treeRoot = deploy.GetNode();
            deployTreeView.Nodes.Add(treeRoot);
            deployTreeView.ShowNodeToolTips = true;
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

        #region Deploy Item Edit

        private void deployNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (library == null)
            {
                MessageBox.Show("No Pattern Library Loaded", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (e.Node.Tag.GetType() == typeof(DeploySystem))
            {
                DeploySystem system = (DeploySystem)e.Node.Tag;
                if (system.Components.Count == 0)      // No pattern applied
                {
                    PatternSelect patternSelect = new PatternSelect();
                    patternSelect.initialise(library, system.SystemType);
                    PatternObject pattern;
                    if (patternSelect.ShowDialog() == DialogResult.OK)
                    {
                        pattern = patternSelect.getPattern();
                        // Find the federate in Design by name
                        foreach (Federate federate in design.Federation.Federates)
                            if (system.FederateName == federate.FederateName)
                            {

                            }
                    }
                }
            }

        }


        #endregion
    }
}


