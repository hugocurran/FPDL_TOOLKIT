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
    public partial class ModuleOptions : Form
    {

        private ContextMenu contextMenu;

        public ModuleOptions(Module module)
        {
            InitializeComponent();

             contextMenu = new ContextMenu();

            this.Text = module.ModuleType.ToString().ToUpperFirst();
            foreach(Specification spec in module.Specifications)
            {
                string s = (spec.ReadOnly) ? String.Format("{0} = {1} (Read Only)", spec.ParamName, spec.Value) : String.Format("{0} = {1}", spec.ParamName, spec.Value);
                specLbx.Items.Add(s);
            }
            specLbx.ContextMenu = contextMenu;
            specLbx.MouseClick += new MouseEventHandler(showContextMenu);
        }

        //private TreeNode old_selectNode;
        private void showContextMenu(object sender, MouseEventArgs e)
        {
            int index = specLbx.IndexFromPoint(e.Location);
            if (e.Button == MouseButtons.Right)
            {
                if (index == ListBox.NoMatches) // Add new item
                {
                    contextMenu.MenuItems.Clear();
                    MenuItem[] items = new MenuItem[1];
                    items[0] = new MenuItem("Add specification", addSpec);
                    contextMenu.MenuItems.AddRange(items);
                    contextMenu.Name = "Edit Specification";
                    //contextMenu.Tag = node.Tag;

                    // Module menu
                    if (node.Tag.GetType() == typeof(Module))
                    {
                        contextMenu.MenuItems.Clear();
                        MenuItem[] items = new MenuItem[1];
                        items[0] = new MenuItem("Edit specification", editSpec);
                        contextMenu.MenuItems.AddRange(items);
                        contextMenu.Name = "Edit Specification";
                        contextMenu.Tag = index;
                    }
                }
            }
        }


        private void addSpec(object sender, EventArgs e)
        {


        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
