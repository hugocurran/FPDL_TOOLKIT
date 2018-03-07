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
    public partial class ModuleExtensionEdit : Form
    {
        internal class Parameters
        {
            internal string name { get; set; }
            internal string value { get; set; }
        }

        private ModuleExtension module;
        private List<Parameters> p;

        public ModuleExtensionEdit(ModuleExtension module)
        {
            this.module = module;

            InitializeComponent();

            //var x = module.Parameters.ToList();
            //var bl = new BindingList<KeyValuePair<string, string>>(x);
            //specDGView.DataSource = new BindingSource(bl, null);

            DataTable table = new DataTable();

            specDGView.AutoGenerateColumns = true;
            p = new List<Parameters>();
            foreach (var spec in module.Parameters)
                p.Add(new Parameters { name = spec.Key, value = spec.Value });
            var bindingList = new BindingList<Parameters>(p);
            var source = new BindingSource(bindingList, null);
            specDGView.DataSource = source;
            specDGView.Show();

            vendorTbx.Text = module.VendorName;

        }

        private void applyBut_Click(object sender, EventArgs e)
        {
            module.VendorName = vendorTbx.Text;

            module.Parameters.Clear();
            foreach(var spec in p)
                module.Parameters.Add(spec.name, spec.value);

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
