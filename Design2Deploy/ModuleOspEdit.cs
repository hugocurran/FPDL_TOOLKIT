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

namespace FPDL.Tools.DeployEditor
{
    public partial class ModuleOspEdit : Form
    {
        private ModuleOsp module;

        public ModuleOspEdit(ModuleOsp module)
        {
            this.module = module;

            InitializeComponent();
            protoLbx.DataSource = Enum.GetValues(typeof(ModuleOsp.OspProtocol));

            pathLbx.SelectedItem = module.Path;
            protoLbx.SelectedItem = module.Protocol.ToString();
            inputTbx.Text = module.InputPort;
            outputTbx.Text = module.OutputPort;
        }

        private void applyBut_Click(object sender, EventArgs e)
        {
            module.Path = pathLbx.SelectedItem.ToString();
            module.Protocol = (ModuleOsp.OspProtocol)protoLbx.SelectedItem;
            module.InputPort = inputTbx.Text;
            module.OutputPort = outputTbx.Text;
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
