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

namespace FPDL.Tools.DeployEditor
{
    public partial class PatternSelect : Form
    {
        private PatternLibrary library;
        private Enums.PatternType patternType;
        internal List<Entry> patternList = new List<Entry>();
        private PatternObject pattern;
        private bool patternSelected;

        public PatternSelect()
        {
            InitializeComponent();
        }

        internal void initialise(PatternLibrary library, Enums.PatternType patternType)
        {
            this.library = library;
            this.patternType = patternType;

            // Get a subset of the library of the patternType given
            foreach (Entry pattern in library.Library)
            {
                if (pattern.Type == patternType)
                {
                    patternList.Add(pattern);   // Read in pattern files if not embedded
                    if (!pattern.Embedded)
                        pattern.Pattern = (PatternObject)FpdlReader.Parse(pattern.Filename);
                }
            }
            for (int i = 0; i < patternList.Count; i++)
            {
                string[] row =
                {
                    patternList[i].Type.ToString().ToUpper(),
                    patternList[i].Name,
                    patternList[i].Version,
                    patternList[i].Reference.ToString()
                };
                patternListGrid.Rows.Add(row);
                patternListGrid.Rows[i].Tag = patternList[i].Pattern;
                patternTypeLabel.Text += patternType.ToString().ToUpper();
            }
        }

        internal PatternObject getPattern()
        {
            if (patternSelected)
                return pattern;
            else
                return null;
        }
        
        private void applyPatternSelect_Click(object sender, EventArgs e)
        {
            if (patternListGrid.SelectedRows.Count < 1)
                return;
            var selected = patternListGrid.SelectedRows;
            pattern = (PatternObject)selected[0].Tag;
            patternSelected = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelPatternSelect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
