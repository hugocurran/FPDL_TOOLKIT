
using FPDL;
using FPDL.Common;
using FPDL.Deploy;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FPDL.Tools.PatternEditor
{
    public partial class Form1 : Form
    {
        private PatternObject pattern;
        private PatternLibrary library;

        private ContextMenu contextMenu;
        private ContextMenu libContextMenu;

        private bool dirtyPattern = false;  // If true then need to advance the version
        private bool newPattern = false;    // if true then this is a new pattern
        private bool libraryAutoSave = true;
        private string libraryFilename;

        public Form1()
        {
            InitializeComponent();
            //tabControl.TabPages.Clear();
            tabControl.SelectTab(0);
            contextMenu = new ContextMenu();
            libContextMenu = new ContextMenu();
            treeView1.ContextMenu = contextMenu;
            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(showContextMenu);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(showLibContextMenu);
            libraryToolStripMenuItem.Checked = true;
            libraryAutosaveMenuItem.Checked = true;
        }

        #region Create new pattern

        // Create a new pattern
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePattern create = new CreatePattern();

            if (create.ShowDialog() == DialogResult.OK)
            {
                pattern = new PatternObject
                {
                    PatternName = create.patternName,
                    PatternType = create.patternType,
                    Description = create.patternDescription
                };
                editablePattern = true;
                dirtyPattern = true;
                pattern.ConfigMgmt = new ConfigMgmt();
                pattern.ConfigMgmt.Initialise(
                    Environment.UserName,
                    1, 0,
                    "Initial Version");
                showPatternFile(pattern);
                treeView1.ContextMenu = contextMenu;
                newPattern = true;
            }
        }

        #endregion

        #region Open a file

        // Open file
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Pattern files | *.xml";
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                try
                {
                    IFpdlObject input = FpdlReader.Parse(openFileDialog1.FileName);

                    if (input.GetType() == typeof(PatternObject))
                    {
                        this.pattern = (PatternObject)input;
                        dirtyPattern = true;
                        newPattern = true;
                        editablePattern = true;
                        showPatternFile(pattern);
                        tabControl.SelectTab(0);

                    }
                    else if (input.GetType() == (typeof(PatternLibrary)))
                    {
                        libraryFilename = openFileDialog1.FileName;
                        this.library = (PatternLibrary)input;
                        showPatternLibrary(library);
                        tabControl.SelectTab(1);
                    }
                    else
                        MessageBox.Show("Not a Pattern or Pattern Library file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (ApplicationException err)
                {
                    MessageBox.Show("Error parsing the file: " + err.Message, "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        private void showConfigMgmt_Click(object sender, EventArgs e)
        {

        }

        #region showPatternFile

        // Show an existing pattern file
        private void showPatternFile(PatternObject pattern)
        {
            tabControl.Show();
            tabControl.Enabled = true;

            treeView1.Nodes.Clear();

            genericType.Enabled = false;
            genericType.Text = pattern.PatternType.ToString().ToUpper();

            patternName.Enabled = false;
            patternName.Text = pattern.PatternName;

            patternDescription.Enabled = false;
            patternDescription.Text = pattern.Description;

            treeView1.Nodes.Add(pattern.GetNode());
            //treeView1.SelectedNode = old_selectNode ?? null;
            if (old_selectNode != null)
                treeView1.SelectedNode = findaNode(old_selectNode);

            if (treeView1.SelectedNode == null)
                treeView1.ExpandAll();
            else
                treeView1.SelectedNode.Expand();
            tabControl.SelectTab(0);
            enableSaveToLibraryButton(pattern);
        }

        private TreeNode findaNode(TreeNode oldNode)
        {
            foreach (TreeNode node in Collect(treeView1.Nodes))
            {
                if (node.Tag == null)
                    continue;
                if (((object)node.Tag).Equals((object)oldNode.Tag))
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

        #region Pattern Context Menu

        private TreeNode old_selectNode;
        private void showContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = treeView1.GetNodeAt(p);
                if (node != null)
                {
                    //old_selectNode = treeView1.SelectedNode;
                    old_selectNode = node;
                    treeView1.SelectedNode = node;

                    if (node.Tag == null)
                        return;

                    // Root (system) menu
                    if (node.Tag.GetType() == typeof(PatternObject))
                    {
                        if (!editablePattern)
                            return;
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[3];
                        items[0] = new MenuItem("Proxy", addComponent);
                        items[1] = new MenuItem("Guard", addComponent);
                        items[2] = new MenuItem("Filter", addComponent);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Add Component";
                    }
                    // Component menu
                    if ((node.Tag.GetType() == typeof(PatternComponent)) && (!node.Text.Contains("Component Name")))
                    {
                        if (!editablePattern)
                            return;
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[8];
                        items[0] = new MenuItem("Interface", addModule);
                        items[1] = new MenuItem("Host", addModule);
                        items[2] = new MenuItem("Federation", addModule);
                        items[3] = new MenuItem("OSP", addModule);
                        items[4] = new MenuItem("Import", addModule);
                        items[5] = new MenuItem("Export", addModule);
                        items[6] = new MenuItem("Filter", addModule);
                        items[7] = new MenuItem("Extension", addModule);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Add Module";
                        contextMenu.Tag = node.Tag;
                    }
                    if ((node.Tag.GetType() == typeof(PatternComponent)) && (node.Text.Contains("Component Name")))
                    {
                        if (!editablePattern)
                            return;
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Edit name", editName);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Add Module";
                        contextMenu.Tag = node.Tag;
                    }
                    // Module menu
                    if (node.Tag.GetType() == typeof(Module))
                    {
                        if (!editablePattern)
                            return;
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Add specification", addSpec);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Add Specification";
                        contextMenu.Tag = node.Tag;
                    }
                    // Specification menu
                    if (node.Tag.GetType() == typeof(Specification))
                    {
                        if (!editablePattern)
                            return;
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[2];
                        items[0] = new MenuItem("Edit specification", editSpec);
                        items[1] = new MenuItem("Delete specification", deleteSpec);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Edit Specification";
                        contextMenu.Tag = node.Tag;
                    }
                }
            }
        }

        private void addComponent(object sender, EventArgs e)
        {
            PatternComponent component = new PatternComponent
            {
                ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), ((MenuItem)sender).Text.ToLower()),
                ComponentID = Guid.NewGuid()
            };
            pattern.Components.Add(component);
            dirtyPattern = true;
            showPatternFile(pattern);
            contextMenu.MenuItems.Clear();
        }

        private void editName(object sender, EventArgs e)
        {
            PatternComponent component = (PatternComponent)contextMenu.Tag;
            SpecEditor spec = new SpecEditor("ComponentName", component.ComponentName);
            if (spec.ShowDialog() == DialogResult.OK)
            {
                component.ComponentName = spec.specification.Value;
                dirtyPattern = true;
                showPatternFile(pattern);
            }
            contextMenu.MenuItems.Clear();
        }

        private void addModule(object sender, EventArgs e)
        {
            PatternComponent component = (PatternComponent)contextMenu.Tag;
            Enums.ModuleType moduleType = (Enums.ModuleType)Enum.Parse(typeof(Enums.ModuleType), ((MenuItem)sender).Text.ToLower());
            component.Modules.Add(new Module(moduleType));
            dirtyPattern = true;
            showPatternFile(pattern);
            contextMenu.MenuItems.Clear();
        }

        private void addSpec(object sender, EventArgs e)
        {
            Module module = (Module)contextMenu.Tag;

            SpecEditor spec = new SpecEditor(module.ModuleType);
            if (spec.ShowDialog() == DialogResult.OK)
            {
                module.Specifications.Add(spec.specification);
                dirtyPattern = true;
                showPatternFile(pattern);
            }
            contextMenu.MenuItems.Clear();
        }

        private void editSpec(object sender, EventArgs e)
        {
            Specification specification = (Specification)contextMenu.Tag;
            if (specification.ReadOnly)
                return;
            Module module = (Module)treeView1.SelectedNode.Parent.Tag;

            SpecEditor spec = new SpecEditor(specification, module.ModuleType);
            if (spec.ShowDialog() == DialogResult.OK)
            {
                dirtyPattern = true;
                showPatternFile(pattern);
            }
            contextMenu.MenuItems.Clear();
        }

        private void deleteSpec(object sender, EventArgs e)
        {
            Specification specification = (Specification)contextMenu.Tag;
            Module module = (Module)treeView1.SelectedNode.Parent.Tag;
            module.Specifications.Remove(specification);
            showPatternFile(pattern);
            contextMenu.MenuItems.Clear();
            dirtyPattern = true;
        }

        #endregion


        #region showPatternLibrary

        private void showPatternLibrary(PatternLibrary library)
        {
            tabControl.Show();
            tabControl.Enabled = true;

            dataGridView1.Rows.Clear();
            foreach (Entry entry in library.Library)
            {
                int rowId = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowId];
                row.Cells["pattType"].Value = entry.Type.ToString().ToUpper();
                row.Cells["pattName"].Value = entry.Name;
                row.Cells["pattVer"].Value = entry.Version;
                row.Cells["pattRef"].Value = entry.Reference;
                row.Tag = entry;
            }
            tabControl.SelectTab(1);
        }

        #endregion

        #region Library Context Menu

        //private TreeNode old_selectNode;
        private void showLibContextMenu(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ContextMenu = libContextMenu;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                var relativeMousePosition = dataGridView1.PointToClient(Cursor.Position);

                if (row != null)
                {
                    if (row.Tag == null)
                        return;

                    libContextMenu.MenuItems.Clear();
                    MenuItem[] items = new MenuItem[4];
                    items[0] = new MenuItem("View Pattern", viewPattern);
                    items[1] = new MenuItem("Edit Pattern", editPattern);
                    items[2] = new MenuItem("Delete Pattern", deletePattern);
                    items[3] = new MenuItem("Export Pattern", exportPattern);
                    libContextMenu.MenuItems.AddRange(items);
                    libContextMenu.Name = "Library";
                    libContextMenu.Tag = row.Tag;
                    libContextMenu.Show(dataGridView1, relativeMousePosition);
                }
            }
        }

        private bool editablePattern = false;
        private void viewPattern(object sender, EventArgs e)
        {
            Entry entry = (Entry)libContextMenu.Tag;
            pattern = entry.Pattern;
            editablePattern = false;
            showPatternFile(pattern);
            libContextMenu.MenuItems.Clear();
        }

        private void editPattern(object sender, EventArgs e)
        {
            Entry entry = (Entry)libContextMenu.Tag;
            pattern = (PatternObject)FPDL.FpdlReader.Parse(new XDocument(entry.Pattern.ToFPDL()));
            editablePattern = true;
            showPatternFile(pattern);
            libContextMenu.MenuItems.Clear();
        }

        private void deletePattern(object sender, EventArgs e)
        {
            Entry entry = (Entry)libContextMenu.Tag;
            library.Remove(entry.Reference);
            if (libraryAutoSave)
                saveLibrary();
            showPatternLibrary(library);
            libContextMenu.MenuItems.Clear();
        }

        private void exportPattern(object sender, EventArgs e)
        {

        }

        #endregion

        #region Helpers

        private void enableSaveToLibraryButton(PatternObject pattern)
        {
            if (library == null)
            {
                addToLibraryBut.Enabled = false;
                return;
            }
            //foreach (Entry entry in library.Library)
            //{
            //    if ((entry.Reference == pattern.ConfigMgmt.DocReference) && (!dirtyPattern))
            //    {
            //        addToLibraryBut.Enabled = false;
            //        return;
            //    }
            //}
            if (dirtyPattern && editablePattern)
                addToLibraryBut.Enabled = true;
            else
                addToLibraryBut.Enabled = false;
        }

        private void saveLibrary()
        {
            if (library != null)
            {
                XDocument doc = new XDocument(library.ToFPDL());
                doc.Save(libraryFilename);
            }
        }

        #endregion

        private void proxyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToLibraryBut_Click(object sender, EventArgs e)
        {
            // Update the Pattern configMgmt if it has been edited
            if (dirtyPattern && editablePattern)
            {
                if (!newPattern)
                    pattern.ConfigMgmt.NewVersion(Environment.UserName, "New Version");
                library.Add(pattern, Environment.UserName, "Added: " + pattern.PatternName);
                if (libraryAutoSave)
                    saveLibrary();
                showPatternLibrary(library);
            }
        }

        private void libraryAutosaveMenuItem_Click(object sender, EventArgs e)
        {
            libraryAutoSave = libraryAutosaveMenuItem.Checked;
            libraryAutosaveMenuItem.Text = (libraryAutoSave) ? "Autosave ON" : "Autosave OFF";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FPDL Pattern Editor\nNiteworks CDS\nVersion 0.3", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveLibrary();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pattern files | *.xml";
            saveFileDialog1.InitialDirectory = "\\";
            saveFileDialog1.FileName = String.Format("{0}-{1}.xml", pattern.PatternName, pattern.ConfigMgmt.DocReference.ToString());
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XDocument doc = new XDocument(pattern.ToFPDL());
                doc.Save(saveFileDialog1.FileName);
            }
        }
    }
}
