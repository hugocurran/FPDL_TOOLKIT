using FPDL;
using FPDL.Common;
using FPDL.Deploy;
using FPDL.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeployEditor
{
    public partial class DeployEditor : Form
    {
        internal DesignObject design;
        internal DeployObject deploy;

        // Keep a list of federates and if they have been deployed
        private Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        // Keep track of the total number of System nodes in the Deploy doc
        private int deploySystemsCount;


        public DeployEditor()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FPDL Deploy Editor\nNiteworks CDS\nVersion 0.1", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deployDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FPDL files | *.xml";
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                try
                {
                    IFpdlObject input = FpdlReader.Parse(openFileDialog1.FileName);

                    if (input.GetType() == typeof(DeployObject))
                        deploy = (DeployObject)input;
                    else
                        MessageBox.Show("Not a Deploy file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (deploy.IsEmpty())
                    {
                        deploy = null;
                        MessageBox.Show("Deploy is empty", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (ApplicationException)
                {
                    MessageBox.Show("Error parsing the file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void designToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FPDL files | *.xml";
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                try
                {
                    IFpdlObject input = FpdlReader.Parse(openFileDialog1.FileName);

                    if (input.GetType() == typeof(DesignObject))
                        design = (DesignObject)input;
                    else
                        MessageBox.Show("Not a Design file", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    foreach (Federate federate in design.Federation.Federates)
                        checkList.Add(federate.FederateName, false);
                    showDesignTree();
                }
                catch (ApplicationException)
                {
                    MessageBox.Show("Error parsing the file", "Pattern Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void createDeploy_Click(object sender, EventArgs e)
        {
            if (designTreeView.SelectedNode.Tag == null)
                MessageBox.Show("Can't deploy a service", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            Federate federate = (Federate)designTreeView.SelectedNode.Tag;
            if (checkList[federate.FederateName])
                MessageBox.Show("That federate has been deployed (edit in the Deploy pane)", "Deploy Editor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (deploy == null)
            {
                deploy = new DeployObject();

                deploy.ConfigMgmt.Initialise("PC", 1, 0, "Initial Version");
                deploy.DesignReference = design.ConfigMgmt.DocReference;
                foreach (Federate fed in design.Federation.Federates)
                {
                    if ((fed.FederateType == Enums.FederateType.gateway) || (fed.FederateType == Enums.FederateType.filter))
                        deploySystemsCount++;
                }
            }

            FPDL.Deploy.System system = new FPDL.Deploy.System { SystemType = federate.FederateType };
            deploy.Systems.Add(system);
            addSystemToDeployTree();
        }

        private void addSystemToDeployTree()
        {
            deployTreeView.Nodes.Clear();

            TreeNode[] systems = new TreeNode[deploy.Systems.Count];
            TreeNode[] components;
            TreeNode[] modules;
            TreeNode[] parameters;

            for (int i = 0; i < deploy.Systems.Count; i++)
            {
                if (deploy.Systems[i].PatternRef != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    systems[i] = getSystems(deploy.Systems[i].Components);
                }
                else
                {

                    systems[i] = new TreeNode(deploy.Systems[i].SystemType.ToString().ToUpperFirst());
                }
            }
            TreeNode treeRoot = new TreeNode("Deploy", systems);
            deployTreeView.Nodes.Add(treeRoot);
            deployTreeView.ExpandAll();
        }


        private TreeNode getSystems(List<FPDL.Deploy.Component> components)
        {
            throw new NotImplementedException();
        }
        private TreeNode[] getParameters(IModule mod)
        {
            throw new NotImplementedException();
        }


        private void showDesignTree()
        {
            designTreeView.Nodes.Clear();

            List<Federate> federateList = design.Federation.Federates;

            TreeNode[] federates = new TreeNode[federateList.Count];

            for (int i = 0; i < federateList.Count; i++)
            {
                TreeNode[] fedType = new TreeNode[1];
                TreeNode[] type = new TreeNode[1];

                if (federateList[i].FederateType == Enums.FederateType.gateway)
                {
                    type[0] = new TreeNode(federateList[i].GatewayType.ToString().ToUpper());
                    type[0].Tag = federateList[i];
                    fedType[0] = new TreeNode((federateList[i].FederateType.ToString().ToUpperFirst()), type);
                    fedType[0].Tag = federateList[i];
                }
                //if (federateList[i].FederateType == Enums.FederateType.filter)
                //{
                //    type[0] = new TreeNode(federateList[i].FilterType.ToString().ToUpper());
                //    fedType[0] = new TreeNode((federateList[i].FederateType.ToString().ToUpperFirst()), type);
                //}
                if (federateList[i].FederateType == Enums.FederateType.service)
                {
                    fedType[0] = new TreeNode((federateList[i].FederateType.ToString().ToUpperFirst()));
                }

                federates[i] = new TreeNode(federateList[i].FederateName, fedType);
                federates[i].Tag = federateList[i];
            }
            TreeNode federation = new TreeNode(design.Federation.Name, federates);

            designTreeView.Nodes.Add(federation);
            designTreeView.ExpandAll();
        }


    }
}


