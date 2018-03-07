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
using System.Drawing;

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

        private ContextMenu designContextMenu;
        private ContextMenu deployContextMenu;

        public DeployEditor()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Pattern Library not loaded";

            designContextMenu = new ContextMenu();
            deployContextMenu = new ContextMenu();
            designTreeView.ContextMenu = designContextMenu;
            designTreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(showDesignContextMenu);
            deployTreeView.ContextMenu = deployContextMenu;
            deployTreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(showDeployContextMenu);

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

        //private void createDeploy_Click(object sender, EventArgs e)
        //{
        //    if (designTreeView.SelectedNode == null)
        //        return;

        //    if ((string)designTreeView.SelectedNode.Tag == "ROOT")      // Design tree root
        //    {
        //        if (MessageBox.Show("Deploy all federates?", "Deploy Editor", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
        //        {
        //            deploy = BuildIt.Initialise(design, "UK", "Official", this);
        //            showDeployTree();
        //        }
        //    }
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
            deployTreeView.CollapseAll();
            if (old_selectNode != null)
                deployTreeView.SelectedNode = findaNode(old_selectNode);
            if (deployTreeView.SelectedNode == null)
                deployTreeView.ExpandAll();
            else
                deployTreeView.SelectedNode.ExpandAll();
        }

        private TreeNode findaNode(TreeNode oldNode)
        {
            TreeNode origNode = null;
            if (oldNode.Tag == null)    // leaf nodes not all tagged
                origNode = oldNode.Parent;
            else
                origNode = oldNode;
            if (origNode.Tag == null)   // give up
                return null;
            foreach (TreeNode node in Collect(deployTreeView.Nodes))
            {
                if (node.Tag == null)
                    continue;
                if (((object)node.Tag).Equals((object)origNode.Tag))
                    return node;
            }
            return null;
        }

        private IEnumerable<TreeNode> Collect(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                yield return node;

                foreach (var child in Collect(node.Nodes))
                    yield return child;
            }
        }

        #endregion

        #region Show Design Tree

        private void showDesignTree()
        {
            federationTxtBox.Text = design.Federation.Name;
            federationDescription.Text = design.Federation.Summary;

            designTreeView.Nodes.Clear();

            TreeNode federation = new TreeNode(design.Federation.Name, DesignViewBuilder.getFederates(design.Federation.Federates))
            {
                Tag = design.Federation
            };
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

            //// Pattern 
            //if (tag.GetType() == typeof(DeploySystem))
            //{
            //    DeploySystem system = (DeploySystem)tag;
            //    if (system.Components.Count == 0)      // No pattern applied
            //    {
            //        PatternSelect patternSelect = new PatternSelect();
            //        patternSelect.initialise(library, system.PatternType);
            //        if (patternSelect.ShowDialog() == DialogResult.OK)
            //        {
            //            PatternObject pattern = patternSelect.getPattern();
            //            Federate federate = null;
            //            // Find the federate in Design by name
            //            foreach (Federate fed in design.Federation.Federates)
            //            {
            //                if (system.FederateName == fed.FederateName)
            //                {
            //                    federate = fed;
            //                    break;
            //                }
            //            }
            //            BuildIt.AddPattern(federate, system, pattern);
            //        }
            //    }
            //} // end pattern

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

        #region Design Context Menu

        //private TreeNode old_selectNode;
        private void showDesignContextMenu(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = designTreeView.GetNodeAt(p);
                if (node != null)
                {
                    //old_selectNode = treeView1.SelectedNode;
                   designTreeView.SelectedNode = node;

                    if (node.Tag == null)
                        return;

                    // Root (system) menu
                    if (node.Tag.GetType() == typeof(Federation))
                    {
                        designContextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Add all federates", addFederation);
                        designContextMenu.MenuItems.AddRange(items);
                        designContextMenu.Name = "Add Federation";
                        designContextMenu.Tag = node.Tag;                   }
                    // Federate menu
                    if (node.Tag.GetType() == typeof(Federate))
                    {
                        designContextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Add this federate", addFederate);
                        designContextMenu.MenuItems.AddRange(items);
                        designContextMenu.Name = "Add Federate";
                        designContextMenu.Tag = node.Tag;
                    }
                }
            }
        }

        private void addFederation(object sender, EventArgs e)
        {
            if (designTreeView.SelectedNode == null)
                return;

            Federation federation = (Federation)designContextMenu.Tag;
            deploy = BuildIt.Initialise(design, "UK", "Official", this);
            showDeployTree();
            designContextMenu.MenuItems.Clear();
        }

        private void addFederate(object sender, EventArgs e)
        {
            if (designTreeView.SelectedNode == null)
                return;

            Federate federation = (Federate)designContextMenu.Tag;
            //deploy = BuildIt.Initialise(design, "UK", "Official", this);
            showDeployTree();
            designContextMenu.MenuItems.Clear();
        }

        #endregion

        #region Deploy Context Menu

        private TreeNode old_selectNode;
        private void showDeployContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = deployTreeView.GetNodeAt(p);
                if (node != null)
                {
                    old_selectNode = node;
                    deployTreeView.SelectedNode = node;

                    if ((node.Tag == null) || (node.Parent == null))    // Catch the tree root and non-editable nodes
                        return;

                    Type nodeType = node.Tag.GetType();
                    Type nodeParentType = node.Parent.Tag.GetType();
                    // Root (System) menu
                    if (nodeType == typeof(DeploySystem))
                    {
                        deployContextMenu.MenuItems.Clear();
                        if (library == null)
                        {
                            MessageBox.Show("No Pattern Library Loaded", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Apply Pattern", applyPattern);
                        deployContextMenu.MenuItems.AddRange(items);
                        deployContextMenu.Name = "Apply pattern";
                        deployContextMenu.Tag = node.Tag;
                    }
                    // Component menu
                    if (node.Tag.GetType() == typeof(Component))
                    {
                        deployContextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Edit component name", editComponent);
                        deployContextMenu.MenuItems.AddRange(items);
                        deployContextMenu.Name = "Edit component";
                        deployContextMenu.Tag = node.Tag;
                    }
                    // Module menu
                    if (node.Tag is IModule)
                    {
                        deployContextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Add specification", addSpec);
                        deployContextMenu.MenuItems.AddRange(items);
                        deployContextMenu.Name = "Add Specification";
                        deployContextMenu.Tag = node.Tag;
                    }
                    // Specification menu
                    if (node.Tag.GetType() == typeof(Specification))
                    {
                        deployContextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Edit specification", editSpec);
                        deployContextMenu.MenuItems.AddRange(items);
                        deployContextMenu.Name = "Edit Specification";
                        deployContextMenu.Tag = node.Tag;
                    }
                }
            }
        }

        private void applyPattern(object sender, EventArgs e)
        {
            DeploySystem system = (DeploySystem)deployContextMenu.Tag;
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
                    showDeployTree();
                }
            }
        }

        private void editComponent(object sender, EventArgs e)
        {
            Component component = (Component)deployContextMenu.Tag;
            SpecEditor spec = new SpecEditor("componentName", component.ComponentName);
            if (spec.ShowDialog() == DialogResult.OK)
            {
                component.ComponentName = spec.specification.Value;
                showDeployTree();
            }
            deployContextMenu.MenuItems.Clear();
        }

        private void addSpec(object sender, EventArgs e)
        {
            IModule module = (IModule)deployContextMenu.Tag;
            SpecEditor spec = new SpecEditor(module.GetModuleType());
            if (spec.ShowDialog() == DialogResult.OK)
            {
                module.ApplyPattern(spec.specification);
                showDeployTree();
            }
            deployContextMenu.MenuItems.Clear();
        }

        private void editSpec(object sender, EventArgs e)
        {
            Specification specification = (Specification)deployContextMenu.Tag;
            if (specification.ReadOnly)
                return;
            IModule module = (IModule)deployTreeView.SelectedNode.Parent.Tag;

            SpecEditor spec = new SpecEditor(specification, module.GetModuleType());
            if (spec.ShowDialog() == DialogResult.OK)
            {
                module.ApplyPattern(spec.specification);
                showDeployTree();
            }
            deployContextMenu.MenuItems.Clear();
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
                        FpdlWriter.Write(deploy, saveFileDialog1.FileName);
                        //XDocument doc = new XDocument(deploy.ToFPDL());
                        //doc.Save(saveFileDialog1.FileName);
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
                            //XDocument doc = new XDocument(deploy.ToFPDL(system));
                            string filename = String.Format("{0}\\{1} - {2}",
                                folderBrowserDialog1.SelectedPath,
                                system.FederateName, 
                                deploy.DesignReference
                                );
                            //doc.Save(filename);
                            FpdlWriter.Write(deploy.ToFPDL(system), filename);
                        }
                    }
                }
            }
        }
    }
}


