using System;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Specification
    /// </summary>
    public class Specification
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParamName;
        /// <summary>
        /// Value
        /// </summary>
        public string Value;
        /// <summary>
        /// Read only
        /// </summary>
        public bool ReadOnly = false;
        /// <summary>
        /// Construct specification object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Specification(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Specification object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "specification")
                throw new ApplicationException("Cannot parse: Not an FPDL specification");
            ParamName = fpdl.Element("paramName").Value;
            Value = fpdl.Element("value").Value;
            if (fpdl.Element("paramName").Attribute("readOnly") != null)
                ReadOnly = Convert.ToBoolean(fpdl.Element("paramName").Attribute("readOnly").Value);
        }
        /// <summary>
        /// Serialise Specification object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("specification",
                new XElement("paramName", ParamName,
                    new XAttribute("readOnly", ReadOnly)),
                new XElement("value", Value)
                );
            return fpdl;
        }
        /// <summary>
        /// String representation of Specification object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("\t{0} = {1} (ReadOnly = {2})\n", ParamName, Value, ReadOnly);
            return str.ToString();
        }
    }
}