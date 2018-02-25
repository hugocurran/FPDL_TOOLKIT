
using FPDL;
using FPDL.Common;
using FPDL.Deploy;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPDL.Tools.PatternEditor
{
    public partial class Form1 : Form
    {
        private PatternObject pattern;
        private PatternLibrary library;

        private ContextMenu contextMenu;


        public Form1()
        {
            InitializeComponent();
            //tabControl.TabPages.Clear();
            contextMenu = new ContextMenu();
            treeView1.ContextMenu = contextMenu;
            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(showContextMenu);
        }

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
                pattern.ConfigMgmt = new ConfigMgmt();
                pattern.ConfigMgmt.Initialise(
                    Environment.UserName,
                    1, 0,
                    "Initial Version");
                showPatternFile(pattern);
                treeView1.ContextMenu = contextMenu;
            }
        }

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
                        showPatternFile(pattern);
                    }
                    else if (input.GetType() == (typeof(PatternLibrary)))
                    {
                        this.library = (PatternLibrary)input;
                        showPatternLibrary(library);
                    }
                    else
                        MessageBox.Show("Not a Pattern or Pattern Library file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (ApplicationException)
                {
                    MessageBox.Show("Error parsing the file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

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
            treeView1.ExpandAll();
        }
        #endregion

        #region Context Menu

        private TreeNode old_selectNode;
        private void showContextMenu(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode node = treeView1.GetNodeAt(p);
                if (node != null)
                {
                    old_selectNode = treeView1.SelectedNode;
                    treeView1.SelectedNode = node;

                    if (node.Tag == null)
                        return;

                    // Root (system) menu
                    if (node.Tag.GetType() == typeof(PatternObject))
                    {
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[3];
                        items[0] = new MenuItem("Proxy", addComponent);
                        items[1] = new MenuItem("Guard", addComponent);
                        items[2] = new MenuItem("Filter", addComponent);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Add Component";
                    }
                    // Component menu
                    if (node.Tag.GetType() == typeof(PatternComponent))
                    {
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
                    // Module menu
                    if (node.Tag.GetType() == typeof(Module))
                    {
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Edit specification", editSpec);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Edit Specification";
                        contextMenu.Tag = node.Tag;
                    }
                }
            }
        }

        private void addComponent(object sender, EventArgs e)
        {
            PatternComponent component = new PatternComponent();
            component.ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), ((MenuItem)sender).Text.ToLower());
            component.ComponentID = Guid.NewGuid();
            pattern.Components.Add(component);
            showPatternFile(pattern);
            contextMenu.MenuItems.Clear();
        }

        private void addModule(object sender, EventArgs e)
        {
            PatternComponent component = (PatternComponent)contextMenu.Tag;
            Enums.ModuleType moduleType = (Enums.ModuleType)Enum.Parse(typeof(Enums.ModuleType), ((MenuItem)sender).Text.ToLower());
            component.Modules.Add(new Module(moduleType));
            showPatternFile(pattern);
            contextMenu.MenuItems.Clear();
        }

        private void editSpec(object sender, EventArgs e)
        {
            Module module = (Module)contextMenu.Tag;

            ModuleOptions options = new ModuleOptions(module);
            options.ShowDialog();

            //PatternComponent component = (PatternComponent)contextMenu.Tag;
            //Enums.ModuleType moduleType = (Enums.ModuleType)Enum.Parse(typeof(Enums.ModuleType), ((MenuItem)sender).Text.ToLower());
            //component.Modules.Add(new Module(moduleType));
            //showPatternFile(pattern);
            contextMenu.MenuItems.Clear();
        }



        #endregion

        #region showPatternLibrary

        private void showPatternLibrary(PatternLibrary library)
        {
            tabControl.Show();
            tabControl.Enabled = true;

            //dataGridView1.
            foreach (var entry in library.Library)
            {
                int rowId = dataGridView1.Rows.Add();
                DataGridViewRow row = dataGridView1.Rows[rowId];
                row.Cells["pattType"].Value = entry.Type.ToString();
                row.Cells["pattName"].Value = entry.Name;
                row.Cells["pattVer"].Value = entry.Version;
                row.Cells["pattRef"].Value = entry.Reference;
                //dataGridView1.Rows.Add(row);
            }


        }

        #endregion

        private void proxyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
