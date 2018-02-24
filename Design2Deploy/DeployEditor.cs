using System;
using FPDL;
using FPDL.Common;
using FPDL.Design;

using System.Collections.Generic;
using System.Windows.Forms;
using FPDL.Deploy;
using FPDL.Pattern;
using System.IO;
using System.Xml;
using System.Xml.Linq;

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
        //private int deploySystemsCount;

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

            if ((((TreeView)sender).SelectedNode == null) || (((TreeView)sender).SelectedNode.Tag == null))
                return;
            object tag = ((TreeView)sender).SelectedNode.Tag;

            // Pattern 
            if (tag.GetType() == typeof(DeploySystem))
            {
                DeploySystem system = (DeploySystem)tag;
                if (system.Components.Count == 0)      // No pattern applied
                {
                    PatternSelect patternSelect = new PatternSelect();
                    patternSelect.initialise(library, system.PatternType);
                    if (patternSelect.ShowDialog() == DialogResult.OK)
                    {
                        PatternObject pattern = patternSelect.getPattern();
                        Federate federate = null;
                        // Find the federate in Design by name
                        foreach (Federate fed in design.Federation.Federates)
                        {
                            if (system.FederateName == fed.FederateName)
                            {
                                federate = fed;
                                break;
                            }
                        }
                        BuildIt.AddPattern(federate, system, pattern);
                    }
                }
            } // end pattern

            // Module edit
            if (tag.GetType() == typeof(ModuleInterface))
            {
                ModuleInterface module = (ModuleInterface)tag;
                ModuleInterfaceEdit edit = new ModuleInterfaceEdit(module);
                if (edit.ShowDialog() == DialogResult.Cancel)
                    return;
            }
            if (tag.GetType() == typeof(ModuleOsp))
            {
                ModuleOsp module = (ModuleOsp)tag;
                ModuleOspEdit edit = new ModuleOspEdit(module);
                if (edit.ShowDialog() == DialogResult.Cancel)
                    return;
            }
            if (tag.GetType() == typeof(ModuleExtension))
            {
                ModuleExtension module = (ModuleExtension)tag;
                ModuleExtensionEdit edit = new ModuleExtensionEdit(module);
                if (edit.ShowDialog() == DialogResult.Cancel)
                    return;
            }// end module edit
            showDeployTree();
        }


        #endregion


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeploySave save = new DeploySave(deploy, design);
            if (save.ShowDialog() == DialogResult.OK)
            {
                if (save.singleFile)
                {
                    saveFileDialog1.AddExtension = true;
                    saveFileDialog1.DefaultExt = ".xml";
                    saveFileDialog1.Filter = "FPDL files | *.xml";
                    openFileDialog1.InitialDirectory = "\\";
                    saveFileDialog1.FileName = save.fileName + ".xml";
                    saveFileDialog1.RestoreDirectory = true;
                    if (DialogResult.OK == saveFileDialog1.ShowDialog())
                    {
                        XDocument doc = new XDocument(deploy.ToFPDL());
                        doc.Save(saveFileDialog1.FileName);
                    }
                }
                else
                {
                    //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
                    folderBrowserDialog1.Description = "Deploy Editor";
                    if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
                    {
                        string path = folderBrowserDialog1.SelectedPath;
                        foreach (DeploySystem system in deploy.Systems)
                        {
                            XDocument doc = new XDocument(deploy.ToFPDL(system));
                            string filename = String.Format("{0}\\{1} - {2}",
                                folderBrowserDialog1.SelectedPath,
                                system.FederateName, 
                                deploy.DesignReference
                                );
                            doc.Save(filename);
                        }
                    }
                }
            }
        }
    }
}


