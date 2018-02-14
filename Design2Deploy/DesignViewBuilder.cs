using FPDL.Common;
using FPDL.Deploy;
using FPDL.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPDL.Tools.DeployEditor
{
    internal class DesignViewBuilder
    {
        internal static TreeNode[] getFederates(List<Federate> federates)
        {
            //  FedName
            //      FedType  (t1)
            //          GwType | FilterType  (t2)
            //
            TreeNode[] feds = new TreeNode[federates.Count];
            for (int i = 0; i < federates.Count; i++)
            {
                TreeNode[] t1 = new TreeNode[1];
                TreeNode[] t2 = new TreeNode[1];
                Enums.FederateType type = federates[i].FederateType;
                if (type == Enums.FederateType.gateway)
                {
                    t2[0] = new TreeNode(federates[i].GatewayType.ToString().ToUpper());
                    t2[0].Tag = federates[i];
                    t1[0] = new TreeNode(federates[i].FederateType.ToString().ToUpperFirst(), t2);
                    t1[0].Tag = federates[i];
                }
                if (type == Enums.FederateType.filter)
                {
                    t2[0] = new TreeNode(federates[i].FilterType.ToString().ToUpperFirst());
                    t2[0].Tag = federates[i];
                    t1[0] = new TreeNode(federates[i].FederateType.ToString().ToUpperFirst(), t2);
                    t1[0].Tag = federates[i];
                }
                if (type == Enums.FederateType.service)
                {
                    t1[0] = new TreeNode(federates[i].FederateType.ToString().ToUpperFirst());
                }
                feds[i] = new TreeNode(federates[i].FederateName, t1);
                if (type != Enums.FederateType.service)
                    feds[i].Tag = federates[i];
            }
            return feds;
        }
    }
}
