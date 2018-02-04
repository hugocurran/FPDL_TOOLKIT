using System;
using System.Collections.Generic;
using System.Xml.Linq;
using FPDL.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{

    #region Object

    [TestClass]
    public class EntityRefTypesUnit
    {
        [TestMethod]
        public void HlaObjectCreation()
        {
            HlaObject testObj = new HlaObject();
            testObj.ObjectClassName = "HlaObjectRoot.Some.Object";
            testObj.Attributes.Add(new HlaAttribute { AttributeName = "attrib1", DataType = "int", DefaultValue = 1 });
            testObj.Attributes.Add(new HlaAttribute { AttributeName = "attrib2", DataType = "string", DefaultValue = "foo" });

            Assert.IsTrue(testObj.Attributes.Count == 2);

            XElement result = testObj.ToFPDL();
            Assert.AreEqual("HlaObjectRoot.Some.Object", result.Element("objectClassName").Value);
            Assert.AreEqual("attrib1", result.Element("attributeName").Value);
        }

        [TestMethod]
        public void HlaObjectException()
        {
            HlaObject testObj = new HlaObject();
            testObj.ObjectClassName = "HlaObjectRoot.Some.Object";
            testObj.Attributes.Add(new HlaAttribute { AttributeName = "attrib1", DefaultValue = 1 });
            testObj.Attributes.Add(new HlaAttribute { AttributeName = "attrib2", DataType = "string", DefaultValue = "foo" });

            Assert.IsTrue(testObj.Attributes.Count == 2);
            Assert.ThrowsException<ApplicationException>(() => testObj.ToFPDL());
        }

        #endregion

        #region Interaction


        [TestMethod]
        public void HlaInteractionCreation()
        {
            HlaInteraction testObj = new HlaInteraction();
            testObj.InteractionClassName = "HlaInteractionRoot.SomeEvent";
            testObj.Parameters.Add(new HlaParameter { ParameterName = "param1", DataType = "int", DefaultValue = 1 });
            testObj.Parameters.Add(new HlaParameter { ParameterName = "param2", DataType = "string", DefaultValue = "foo" });

            Assert.IsTrue(testObj.Parameters.Count == 2);

            XElement result = testObj.ToFPDL();
            Assert.AreEqual("HlaInteractionRoot.SomeEvent", result.Element("interactionClassName").Value);
            Assert.AreEqual("param1", result.Element("parameterName").Value);
        }

        [TestMethod]
        public void HlaInteractionException()
        {
            HlaInteraction testObj = new HlaInteraction();
            testObj.InteractionClassName = "HlaInteractionRoot.SomeEvent";
            testObj.Parameters.Add(new HlaParameter {ParameterName = "param1", DefaultValue = 1 });
            testObj.Parameters.Add(new HlaParameter { ParameterName = "param2", DataType = "string", DefaultValue = "foo" });

            Assert.IsTrue(testObj.Parameters.Count == 2);
            Assert.ThrowsException<ApplicationException>(() => testObj.ToFPDL());
        }

        [TestMethod]
        public void HlaInteractionFromFPDL_NoParams()
        {
            XElement interact = new XElement("interaction",
                new XElement("interactionClassName", "HlaInteractionRoot.SomeEvent")
                );
            HlaInteraction testObj = HlaInteraction.FromFPDL(interact);

            Assert.IsTrue(testObj.Parameters.Count == 0);
            Assert.AreEqual("HlaInteractionRoot.SomeEvent", testObj.InteractionClassName);
        }

        [TestMethod]
        public void HlaInteractionFromFPDL_Params()
        {
            XElement interact = new XElement("interaction",
                new XElement("interactionClassName", "HlaInteractionRoot.SomeEvent"),
                new XElement("parameterName", "Param1",
                    new XAttribute("defaultValue", 1)
                    ),
                new XElement("parameterName", "Param2",
                    new XAttribute("dataType", "string"),
                    new XAttribute("defaultValue", "foo")
                    )
                );
            HlaInteraction testObj = HlaInteraction.FromFPDL(interact);

            Assert.IsTrue(testObj.Parameters.Count == 2);
            Assert.AreEqual("HlaInteractionRoot.SomeEvent", testObj.InteractionClassName);
        }
        #endregion
    }
}


