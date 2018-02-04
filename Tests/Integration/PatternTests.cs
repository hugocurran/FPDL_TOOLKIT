using System;
using System.Xml.Linq;
using FPDL.BL;
using FPDL.Pattern.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class PatternTests
    {
        [TestMethod]
        public void PatternReadFromFile()
        {
            XDocument patternFile = XDocument.Load(@"..\..\..\Test Data\Pattern1.xml");
            PatternObject pattern = FpdlPatternParser.Load(patternFile);
        }
    }
}
