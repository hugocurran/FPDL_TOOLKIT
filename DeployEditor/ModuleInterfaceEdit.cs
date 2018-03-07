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
    public partial class ModuleInterfaceEdit : Form
    {
        private ModuleInterface module;

        public ModuleInterfaceEdit(ModuleInterface module)
        {
            this.module = module;

            InitializeComponent();
            interfaceNameTbx.Text = module.InterfaceName;
            ipAddressTbx.Text = module.IpAddress;
            netPrefixTbx.Text = module.NetPrefix;
            defaultRouterTbx.Text = module.DefaultRouter;
        }

        private void applyBut_Click(object sender, EventArgs e)
        {
            module.InterfaceName = interfaceNameTbx.Text;
            module.IpAddress = ipAddressTbx.Text;
            module.NetPrefix = netPrefixTbx.Text;
            module.DefaultRouter = defaultRouterTbx.Text;
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
