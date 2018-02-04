using System;
using System.Xml;
using System.Xml.Linq;
using FPDL.Deploy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FPDL.Test.Integration
{
    [TestClass]
    public class ToFPDLTests
    {
        [TestMethod]
        public void ToFPDLDoc()
        {
            // Dumb test, but you get wot you pays for....
            XDocument fpdl_doc = XDocument.Load(@"..\..\..\FPDL\Test Data\Deploy1.xml");
            DeployObject deploy = new DeployObject(fpdl_doc.Element("Deploy"));

            XElement output_doc = deploy.ToFPDL();
        }
    }
}
