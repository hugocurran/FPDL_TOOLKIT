﻿using FPDL.Common;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public ModuleFederation()
        {
            FederationName = "";
            FederateName = "";
            InterfaceName = "";
            RTI = new Rti();
        }
        /// <summary>
        /// Construct Federation Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleFederation(XElement fpdl) : this()
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
                RTI.CrcAddress = fpdl.Element("rti").Element("crcAddress").Value;
                RTI.AddressType = fpdl.Element("rti").Element("crcAddress").Attribute("addressType").Value;
                RTI.CrcPortNumber = fpdl.Element("rti").Element("crcPortNumber").Value;
                RTI.HlaSpec = fpdl.Element("rti").Element("hlaSpec").Value;

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
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "federation",
               new XElement(ns + "federationName", FederationName),
               new XElement(ns + "federateName", FederateName),
               new XElement(ns + "interfaceName", InterfaceName)
            );
            XElement rti = new XElement(ns + "rti",
                new XElement(ns + "crcAddress", RTI.CrcAddress,
                    new XAttribute("addressType", RTI.AddressType)),
                new XElement(ns + "crcPortNumber", RTI.CrcPortNumber),
                new XElement(ns + "hlaSpec", RTI.HlaSpec),
                new XElement(ns + "fom")
                );
            foreach (string fom in RTI.FomUri)
                rti.Element(ns + "fom").Add(new XElement(ns + "uri", fom));
            foreach (string fom in RTI.FomFile)
                rti.Element(ns + "fom").Add(new XElement(ns + "fileName", fom));
            fpdl.Add(rti);
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
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[4];
            t[0] = new TreeNode("Federation name = " + FederationName);
            t[0].ToolTipText = "Name of this Federation";
            t[1] = new TreeNode("Federate name = " + FederateName);
            t[1].ToolTipText = "Federate name in the federation";
            t[2] = new TreeNode("Interface name = " + InterfaceName);
            t[2].ToolTipText = "Interface binding";
            t[3] = RTI.GetNode(this);

            TreeNode a = new TreeNode("Federation", t);
            a.ToolTipText = "Federation module";
            a.Tag = this;
            return a;
        }
        /// <summary>
        /// Apply specifications from a Pattern to this module
        /// </summary>
        /// <param name="specifications"></param>
        public void ApplyPattern(List<Specification> specifications)
        {
            foreach (Specification spec in specifications)
                ApplyPattern(spec);

        }
        /// <summary>
        /// Apply specifications from a Pattern to this module
        /// </summary>
        /// <param name="specification"></param>
        public void ApplyPattern(Specification specification)
        {
            switch (specification.ParamName)
            {
                case "federationName":
                    FederationName = specification.Value;
                    break;
                case "federateName":
                    FederateName = specification.Value;
                    break;
                case "interfaceName":
                    InterfaceName = specification.Value;
                    break;
                case "crcAddress":
                    RTI.CrcAddress = specification.Value;
                    break;
                case "addressType":
                    RTI.AddressType = specification.Value;
                    break;
                case "crcPortNumber":
                    RTI.CrcPortNumber = specification.Value;
                    break;
                case "hlaSpec":
                    RTI.HlaSpec = specification.Value;
                    break;
                case "fomUri":
                    RTI.FomUri.Add(specification.Value);
                    break;
                case "fomFile":
                    RTI.FomFile.Add(specification.Value);
                    break;
            }
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
        [DeployIf("fom:uri","URI of FOM module", true, true)]
        public List<string> FomUri { get; set; }
        /// <summary>
        /// Filenames of FOM modules
        /// </summary>
        [DeployIf("fom:fileName", "Filepath of FOM module", true, true)]
        public List<string> FomFile { get; set; }

        /// <summary>
        /// Rti constructor
        /// </summary>
        public Rti()
        {
            CrcAddress = "";
            AddressType = "";
            CrcPortNumber = "";
            HlaSpec = "";
            FomUri = new List<string>();
            FomFile = new List<string>();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode(ModuleFederation module)
        {
            TreeNode[] t = new TreeNode[4];
            t[0] = new TreeNode("CRC Address = " + CrcAddress);
            t[0].Tag = new Specification { ParamName = "crcAddress", Value = CrcAddress };
            t[0].ToolTipText = "CRC server FQDN or IP adress";
            t[1] = new TreeNode("Address type = " + AddressType);
            t[1].Tag = new Specification { ParamName = "addressType", Value = AddressType };
            t[1].ToolTipText = "Address type(hostname/IP address)";
            t[2] = new TreeNode("CRC Port Number = " + CrcPortNumber);
            t[2].Tag = new Specification { ParamName = "crcPortNumber", Value = CrcPortNumber };
            t[2].ToolTipText = "CRC server port number";
            t[3] = new TreeNode("HLA Specification = " + HlaSpec);
            t[3].Tag = new Specification { ParamName = "hlaSpec", Value = HlaSpec };
            t[3].ToolTipText = "Hla specification (eg HLA Evolved";

            TreeNode[] t1 = new TreeNode[FomUri.Count];
            for (int i = 0; i < FomUri.Count; i++)
            {
                t1[i] = new TreeNode("FOM URI = " + FomUri[i]);
                t1[i].Tag = new Specification { ParamName = "fomUri", Value = FomUri[i] };
            }

            TreeNode[] t2 = new TreeNode[FomFile.Count];
            for (int i = 0; i < FomFile.Count; i++)
            {
                t2[i] = new TreeNode("FOM Filename = " + FomFile[i]);
                t2[i].Tag = new Specification { ParamName = "fomFile", Value = FomFile[i] };
            }

            TreeNode a = new TreeNode("RTI");
            a.Nodes.AddRange(t);
            a.Nodes.AddRange(t1);
            a.Nodes.AddRange(t2);
            a.ToolTipText = "RTI specification";
            a.Tag = module;
            return a;
        }
    }
}
