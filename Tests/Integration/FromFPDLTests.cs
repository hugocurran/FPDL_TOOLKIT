using FPDL.Deploy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace IntegrationTests
{
    [TestClass]
    public class FromFPDLTests
    {
        [TestMethod]
        public void FromFPDLDoc()
        {
            XDocument fpdl_doc = XDocument.Load(@"C:\Users\peter\source\repos\FPDL Toolkit\FPDL\Test Data\Deploy1.xml");

           DeployObject deploy = new DeployObject(fpdl_doc.Element("Deploy"));

            Assert.AreEqual("1.0", deploy.ConfigMgmt.CurrentVersion);
        }
    }
}
