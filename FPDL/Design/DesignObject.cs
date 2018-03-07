using FPDL.Common;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Design
{
    /// <summary>
    /// Design object
    /// </summary>
    public class DesignObject : IFpdlObject
    {
        /// <summary>
        /// ConfigMgmt for the Pattern document
        /// </summary>
        public ConfigMgmt ConfigMgmt;
        /// <summary>
        /// Federation Name
        /// </summary>
        public Federation Federation;



        /// <summary>
        /// Construct DesignObject
        /// </summary>
        public DesignObject() { }

        /// <summary>
        /// Construct DesignObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public DesignObject(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise DesignObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "Design")
                throw new ApplicationException("Cannot parse: Not an FPDL Design file");
            try
            {
                ConfigMgmt = new ConfigMgmt(fpdl.Descendants("configMgmt").FirstOrDefault());
                Federation = new Federation(fpdl.Element("federation"));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Design parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise PatternObject to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            XNamespace ns = XNamespace.Get("http://www.niteworks.net/fpdl");
            XElement fpdl = new XElement(ns + "Design",
                    new XAttribute("xmlns", ns.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                ConfigMgmt.ToFPDL(ns),
                new XElement(ns + "federation", Federation.ToFPDL(ns))
            );
            return fpdl;
        }
        /// <summary>
        /// String representation of PatternObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("Design =>\n");
            str.AppendFormat("{0}", ConfigMgmt);
            str.AppendFormat("\nFederation:\n{0}\n", Federation);
            return str.ToString();
        }
    }
}
