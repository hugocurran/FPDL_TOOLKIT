using FPDL;
using FPDL.Common;
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


        public Form1()
        {
            InitializeComponent();
            //tabControl.TabPages.Clear();
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
                treeView1.ContextMenuStrip = contextMenu;
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

        }

        // Add a new component based on the context menu selection
        private void addComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripComboBox s = (ToolStripComboBox)sender;

            PatternComponent component = new PatternComponent();
            component.ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), ((string)s.SelectedItem).ToLower());
            component.ComponentID = Guid.NewGuid();
            pattern.Components.Add(component);
            showPatternFile(pattern);
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

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
    }
}
