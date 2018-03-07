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
    public partial class SpecEditor : Form
    {
        internal Specification specification;
        private Enums.ModuleType moduleType;

        // Add mode
        public SpecEditor(Enums.ModuleType moduleType)
        {
            this.moduleType = moduleType;
            InitializeComponent();
            loadList(moduleType);
            this.Text = moduleType.ToString().ToUpperFirst() + " Module";
            specification = new Specification();
        }

        // Edit mode
        public SpecEditor(Specification specification, Enums.ModuleType moduleType)
        {
            this.specification = specification;
            this.moduleType = moduleType;
            InitializeComponent();
            loadList(moduleType);
            this.Text = "Edit Specification";
            if (moduleType == Enums.ModuleType.extension)
                paramCbx.Items.Add(specification.ParamName);
            paramCbx.SelectedItem = specification.ParamName;
            valueTbx.Text = specification.Value;
            readOnlyCk.Checked = specification.ReadOnly;
        }

        // Generic param/value edit
        public SpecEditor(string parameter, string value, bool readOnlyPossible = false, bool readOnly = false)
        {
            specification = new Specification()
            {
                ParamName = parameter,
                Value = value,
                ReadOnly = readOnly
            };
            InitializeComponent();
            this.Text = "Edit " + parameter;
            paramCbx.Items.Add(specification.ParamName);
            paramCbx.SelectedItem = specification.ParamName;
            valueTbx.Text = specification.Value;
            if (readOnlyPossible)
                readOnlyCk.Checked = specification.ReadOnly;
            else
                readOnlyCk.Enabled = false;
            moduleType = Enums.ModuleType.extension; // Allows soft parameter names
        }

        private void apply_Click(object sender, EventArgs e)
        {
            if ((paramCbx.SelectedIndex >= 0) || (moduleType == Enums.ModuleType.extension))
            {
                specification.ParamName = paramCbx.Text;
                specification.Value = valueTbx.Text;
                specification.ReadOnly = readOnlyCk.Checked;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("No parameter selected", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }                
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
                case Enums.ModuleType.host:
                    paramCbx.Items.Add("hostName");
                    paramCbx.Items.Add("loggingServerAddress");
                    paramCbx.Items.Add("loggingServerPort");
                    paramCbx.Items.Add("loggingServerProtocol");
                    paramCbx.Items.Add("timeServerAddress");
                    paramCbx.Items.Add("timeServerPort");
                    paramCbx.Items.Add("timeServerProtocol");
                    break;
                case Enums.ModuleType.federation:
                    paramCbx.Items.Add("federationName");
                    paramCbx.Items.Add("federateName");
                    paramCbx.Items.Add("interfaceName");
                    paramCbx.Items.Add("crcAddress");
                    paramCbx.Items.Add("addressType");
                    paramCbx.Items.Add("crcPortNumber");
                    paramCbx.Items.Add("hlaSpec");
                    paramCbx.Items.Add("fomUri");
                    paramCbx.Items.Add("fomFile");
                    break;
                case Enums.ModuleType.osp:
                    paramCbx.Items.Add("path");
                    paramCbx.Items.Add("protocol");
                    paramCbx.Items.Add("outputPort");
                    paramCbx.Items.Add("inputPort");
                    break;
                case Enums.ModuleType.extension:
                    paramCbx.Items.Add("vendorName");
                    break;
                case Enums.ModuleType.export:
                case Enums.ModuleType.import:
                case Enums.ModuleType.filter:
                    paramCbx.Items.Add("interfaceName");
                    break;
                 
            }
        }
    }
}

