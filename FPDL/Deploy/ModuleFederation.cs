﻿using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// Federation Module
    /// </summary>
    public class ModuleFederation : IModule
    {
        /// <summary>
        /// Federation Name
        /// </summary>
        [DeployIf("federationName", "The name of this federation")]
        public string FederationName { get; set; }
        /// <summary>
        /// Federate Name
        /// </summary>
        [DeployIf("federateName", "Federate name on this federation")]
        public string FederateName { get; set; }
        /// <summary>
        /// Interface Binding
        /// </summary>
        [DeployIf("interfaceName", "Interface binding for this federation")]
        public string InterfaceName { get; set; }
        /// <summary>
        /// RTI specification
        /// </summary>
        [DeployIf("RTI", "RTI information")]
        public Rti RTI { get; set; }

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.federation;
        }

        /// <summary>
        /// Construct Federation Module
        /// </summary>
        public ModuleFederation() { }
        /// <summary>
        /// Construct Federation Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleFederation(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "federation")
                throw new ApplicationException("Cannot parse: Not an FPDL federation description");
            try
            {
                FederationName = fpdl.Element("federationName").Value;
                FederateName = fpdl.Element("federateName").Value;
                InterfaceName = fpdl.Element("interfaceName").Value;
                RTI = new Rti
                {
                    CrcAddress = fpdl.Element("rti").Element("crcAddress").Value,
                    AddressType = fpdl.Element("rti").Element("crcAddress").Attribute("addressType").Value,
                    CrcPortNumber = fpdl.Element("rti").Element("crcPortNumber").Value,
                    HlaSpec = fpdl.Element("rti").Element("hlaSpec").Value,
                };
                XElement foo = fpdl.Element("rti").Element("fom");
                if (fpdl.Element("rti").Element("fom").Element("uri") != null)
                {
                    foreach (XElement fom in fpdl.Element("rti").Element("fom").Elements("uri"))
                        RTI.FomUri.Add(fom.Value);
                }
                if (fpdl.Element("rti").Element("fom").Element("fileName") != null)
                {
                    foreach (XElement fom in fpdl.Element("rti").Element("fom").Elements("fileName"))
                        RTI.FomFile.Add(fom.Value);
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("federation",
               new XElement("federationName", FederationName),
               new XElement("federateName", FederateName),
               new XElement("interfaceName", InterfaceName)
            );

            XElement rti = new XElement("rti",
                new XElement("crcAddress", RTI.CrcAddress,
                    new XAttribute("addressType", RTI.AddressType)),
                new XElement("crcPortNumber", RTI.CrcPortNumber),
                new XElement("hlaSpec", RTI.HlaSpec)
                );
            foreach (string fom in RTI.FomUri)
                rti.Add(new XElement("uri", fom));
            foreach (string fom in RTI.FomFile)
                rti.Add(new XElement("uri", fom));
            return fpdl;
        }

        /// <summary>
        /// String representation of Federation module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Federation:\n");
            str.AppendFormat("\tFederation Name: {0}\n", FederationName);
            str.AppendFormat("\tFederate Name: {0}\n", FederateName);
            str.AppendFormat("\tInterface Name: {0}\n", InterfaceName);
            str.AppendFormat("\tRTI: CRC Address: {0}\n", RTI.CrcAddress);
            str.AppendFormat("\tRTI: CRC Port: {0}\n", RTI.CrcPortNumber);
            str.AppendFormat("\tRTI HLA Spec: {0}\n", RTI.HlaSpec);
            foreach (string fom in RTI.FomUri)
                str.AppendFormat("\tFOM: {0}\n", fom);
            foreach (string fom in RTI.FomFile)
                str.AppendFormat("\tFOM: {0}\n", fom);
            return str.ToString();
        }

    }

    /// <summary>
    /// RTI class
    /// </summary>
    public class Rti
    {
        /// <summary>
        /// CRC Address
        /// </summary>
        [DeployIf("crcAddress", "IP address or hostname of CRC server")]
        public string CrcAddress { get; set; }
        /// <summary>
        /// Address Type
        /// </summary>
        [DeployIf("addressType", "Type of CRC adress (IP or name)")]
        public string AddressType { get; set; }
        /// <summary>
        /// CRC Port number
        /// </summary>
        [DeployIf("crcPortNumber", "Port number used by CRC server")]
        public string CrcPortNumber { get; set; }
        /// <summary>
        /// HLA Specification
        /// </summary>
        [DeployIf("hlaSpec", "HLA specification for this federation (eg HLA Evolved)")]
        public string HlaSpec { get; set; }
        /// <summary>
        /// URI of FOM modules
        /// </summary>
        [DeployIf("fom:uri","URI of FOM module", false, true)]
        public List<string> FomUri { get; set; }
        /// <summary>
        /// Filenames of FOM modules
        /// </summary>
        [DeployIf("fom:fileName", "Filepath of FOM module", false, true)]
        public List<string> FomFile { get; set; }

        /// <summary>
        /// Rti constructor
        /// </summary>
        public Rti()
        {
            FomUri = new List<string>();
            FomFile = new List<string>();
        }
    }
}
