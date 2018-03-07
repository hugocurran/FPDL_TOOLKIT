using FPDL.Common;
using System;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// PolicyObject class
    /// </summary>
    public class PolicyObject : IFpdlObject
    {
        /// <summary>
        /// ConfigMgmt
        /// </summary>
        ConfigMgmt ConfigMgmt;
        /// <summary>
        /// Federate
        /// </summary>
        Federate Federate;

        /// <summary>
        /// Construct a PolicyObject
        /// </summary>
        public PolicyObject() { }
        /// <summary>
        /// Construct PolicyObject from FPDL document
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyObject(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise PolicyObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "Policy")
                throw new ApplicationException("Cannot parse: Not an FPDL Design file");
            try
            {
                ConfigMgmt = new ConfigMgmt(fpdl.Element("configMgmt"));
                Federate = new Federate(fpdl.Element("federate"));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Policy parse error: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise PolicyObject to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            XNamespace ns = XNamespace.Get("http://www.niteworks.net/fpdl");
            XElement fpdl = new XElement(ns + "Policy",
                    new XAttribute("xmlns", ns.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                new XElement(ConfigMgmt.ToFPDL(ns)),
                new XElement(Federate.ToFPDL(ns))
                );
            return fpdl;
        }

        /// <summary>
        /// String representation of PolicyObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("Policy =>\n");
            str.AppendFormat("{0}", ConfigMgmt);
            str.AppendFormat("\nFederate:\n{0}\n", Federate);
            return str.ToString();
        }
    }
}
