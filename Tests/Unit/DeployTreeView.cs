using System;
using System.Windows.Forms;
using FPDL.Deploy;
using FPDL.Tools.DeployEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FPDL.Test.Unit
{
    [TestClass]
    public class DeployViewTree
    {
        public object DeployView { get; private set; }

        [TestMethod]
        public void ParameterCreation()
        {
            ModuleOsp module = new ModuleOsp();
            module.Path = "export";
            module.Protocol = ModuleOsp.OspProtocol.HPSD_TCP;

            TreeNode[] tn = DeployViewBuilder.GetParams(typeof(ModuleOsp), module);

            Assert.IsTrue(tn.Length == 4);
        }
    }
}
