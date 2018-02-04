using FPDL.Deploy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;

namespace FPDL.Test.Integration
{
    [TestClass]
    public class FromFPDLTests
    {
        [TestMethod]
        public void DeployFromFPDLDoc()
        {
            XDocument fpdl_doc = XDocument.Load(@"..\..\..\FPDL\Test Data\Deploy1.xml");

            // Exercise the full codepath
            DeployObject deploy = FpdlDeployParser.Load(fpdl_doc);

            Tuple<int, int> expectedVer = Tuple.Create(1,0);
            Assert.AreEqual(expectedVer, deploy.ConfigMgmt.CurrentVersion);
            Guid expectedRef = Guid.Parse("52769B0B-A240-45E2-972C-ED3B6307F71D");
            Assert.AreEqual(expectedRef, deploy.ConfigMgmt.DocReference);
            Assert.AreEqual(3, deploy.Systems[0].Components.Count);
        }
    }
}
