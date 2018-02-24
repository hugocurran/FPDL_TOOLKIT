using FPDL.Common;
using FPDL.Deploy;
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
    public partial class CreatePattern : Form
    {
        internal string patternName;
        internal string patternDescription;
        internal Enums.PatternType patternType;

        public CreatePattern()
        {
            InitializeComponent();
        }

        private void applyBut_Click(object sender, EventArgs e)
        {
            patternName = patternNameTbx.Text;
            patternDescription = patternDescriptionRtbx.Text;
            patternType = (Enums.PatternType)Enum.Parse(typeof(Enums.PatternType), patternTypeCbx.Text.ToLower());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBut_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
