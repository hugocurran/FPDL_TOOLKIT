﻿using System;
using System.Xml.Linq;
using FPDL.Pattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FPDL.Test.Integration
{
    [TestClass]
    public class PatternTests
    {
        [TestMethod]
        public void PatternReadFromFile()
        {
            XDocument patternFile = XDocument.Load(@"..\..\..\FPDL\Test Data\Pattern1.xml");
            PatternObject pattern = PatternParser.Load(patternFile);
        }
    }
}
