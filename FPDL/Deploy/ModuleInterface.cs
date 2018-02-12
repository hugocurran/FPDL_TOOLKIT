using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Interface module
    /// </summary>
    class ModuleInterface : IModule
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
        /// Construct an Interface module
        /// </summary>
        public ModuleInterface() { }
        /// <summary>
        /// Construct an Interface module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleInterface(XElement fpdl)
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
                if (fpdl.Element("defaultRouter") != null)
                    DefaultRouter = fpdl.Element("defaultRouter").Value;
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
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("interface",
                new XElement("interfaceName", InterfaceName),
                new XElement("ipAddress", IpAddress,
                    new XAttribute("netPrefix", NetPrefix)
                )
            );
            if (DefaultRouter != "")
                fpdl.Add(new XElement("defaultRouter", DefaultRouter));
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
    }
}
