﻿using FPDL.Common;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// Interface module
    /// </summary>
    public class ModuleInterface : IModule
    {
        /// <summary>
        /// Interface binding for the policy
        /// </summary>
        [DeployIf("interfaceName", "Unique name for this interface")]
        public string InterfaceName { get; set; }
        /// <summary>
        /// IP address
        /// </summary>
        [DeployIf("ipAddress", "IP address for this interface")]
        public string IpAddress { get; set; }
        /// <summary>
        /// Network prefix
        /// </summary>
        [DeployIf("netPrefix", "Network prefix for the address (eg /24)")]
        public string NetPrefix { get; set; }
        /// <summary>
        /// Default router (optional)
        /// </summary>
        [DeployIf("defaultRouter", "Default router IP address", true)]
        public string DefaultRouter { get; set; }

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.@interface;
        }

        /// <summary>
        /// Construct an Interface module
        /// </summary>
        public ModuleInterface()
        {
            InterfaceName = "";
            IpAddress = "";
            NetPrefix = "";
            DefaultRouter = "";
        }
        /// <summary>
        /// Construct an Interface module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleInterface(XElement fpdl) : this ()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Interface module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "interface")
                throw new ApplicationException("Cannot parse: Not an FPDL interface description");
            try
            {
                InterfaceName = fpdl.Element("interfaceName").Value;
                IpAddress = fpdl.Element("ipAddress").Value;
                NetPrefix = fpdl.Element("ipAddress").Attribute("netPrefix").Value;
                DefaultRouter = (String)fpdl.Element("defaultRouter") ?? "";
                //if (fpdl.Element("defaultRouter") != null)
                //    DefaultRouter = fpdl.Element("defaultRouter").Value;
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Interface module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "interface",
                new XElement(ns + "interfaceName", InterfaceName),
                new XElement(ns + "ipAddress", IpAddress,
                    new XAttribute("netPrefix", NetPrefix)
                )
            );
            if (DefaultRouter != "")
                fpdl.Add(new XElement(ns + "defaultRouter", DefaultRouter));
            return fpdl;
        }
        /// <summary>
        /// String representation of Interface module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Interface:\n");
            str.AppendFormat("\tInterface Name: {0}\n", InterfaceName);
            str.AppendFormat("\tIP Address: {0}/{1}\n", IpAddress, NetPrefix);
            if (DefaultRouter != "")
                str.AppendFormat("\tDeFaultRouter: {0}\n", DefaultRouter);
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[4];
            t[0] = new TreeNode("InterfaceName = " + InterfaceName);
            t[0].Tag = new Specification { ParamName = "interfaceName", Value = InterfaceName };
            t[0].ToolTipText = "Interface Name";
            t[1] = new TreeNode("IP Address = " + IpAddress);
            t[1].Tag = new Specification { ParamName = "ipAddress", Value = IpAddress };
            t[1].ToolTipText = "IP Address";
            t[2] = new TreeNode("Network Prefix = " + NetPrefix);
            t[2].Tag = new Specification { ParamName = "netPrefix", Value = NetPrefix };
            t[2].ToolTipText = "Network Prefix (eg 24)";
            t[3] = new TreeNode("Default Router = " + DefaultRouter);
            t[3].Tag = new Specification { ParamName = "defaultRouter", Value = DefaultRouter };
            t[3].ToolTipText = "Default Router";

            TreeNode a = new TreeNode("Interface", t);
            a.ToolTipText = "Interface module";
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
                case "interfaceName":
                    InterfaceName = specification.Value;
                    break;
                case "ipAddress":
                    IpAddress = specification.Value;
                    break;
                case "netPrefix":
                    NetPrefix = specification.Value;
                    break;
                case "defaultRouter":
                    DefaultRouter = specification.Value;
                    break;
            }
        }
    }
}
