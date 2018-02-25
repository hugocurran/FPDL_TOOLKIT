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
    public partial class SpecThingy : Form
    {
        internal Specification specification;

        public SpecThingy(Enums.ModuleType moduleType)
        {
            InitializeComponent();
            loadList(moduleType);
            this.Text = moduleType.ToString().ToUpperFirst();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            specification = new Specification
            {
                ParamName = paramCbx.SelectedText,
                Value = valueTbx.Text,
                ReadOnly = readOnlyCk.Checked;
            }
            dialogResult = OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {

        }
        
        private void loadList(Enums.ModuleType moduleType)
        {
            paramCbx.Items.Clear();
            switch(moduleType)
            {
                case Enums.ModuleType.@interface:
                    paramCbx.Items.Add("interfaceName");
                    paramCbx.Items.Add("ipAddress");
                    paramCbx.Items.Add("netPrefix");
                    paramCbx.Items.Add("defaultRouter");
                    break;
            }
        }
    }
}
