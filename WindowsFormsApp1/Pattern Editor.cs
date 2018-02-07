using FPDL;
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

namespace PatternEditor
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
            
        }

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

            patternGenericType.Enabled = false;
            patternGenericType.SelectedItem = pattern.PatternType.ToString();

            patternName.Enabled = false;
            patternName.Text = pattern.PatternName;

            patternDescription.Enabled = false;
            patternDescription.Text = pattern.Description;

            TreeNode[] components = new TreeNode[pattern.Components.Count];

            for (int i = 0; i < pattern.Components.Count; i++)
            {
                var component = pattern.Components[i];
                TreeNode[] modules = new TreeNode[component.Modules.Count];

                for (int j = 0; j < component.Modules.Count; j++)
                {
                    var module = pattern.Components[i].Modules[j];

                    TreeNode[] specification = new TreeNode[module.Specifications.Count];
                    for (int k = 0; k < module.Specifications.Count; k++)
                    {
                        specification[k] = new TreeNode(module.Specifications[k].ParamName + " = " + module.Specifications[k].Value);
                    }
                    modules[j] = new TreeNode(module.ModuleType.ToString(), specification);

                }
                components[i] = new TreeNode(pattern.Components[i].ComponentType.ToString(), modules);
            }
            TreeNode system = new TreeNode(pattern.PatternType.ToString() + " (" + pattern.PatternName + ")", components);
            
            treeView1.Nodes.Add(system);
            treeView1.ExpandAll();
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
    }
}
