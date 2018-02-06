using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Extension Module
    /// </summary>
    public class ModuleExtension : IModule
    {
        /// <summary>
        /// Vendor Name
        /// </summary>
        public string VendorName;
        /// <summary>
        /// Parameters (Key, Value) pairs
        /// </summary>
        public Dictionary<string, string> Parameters = new Dictionary<string, string>();

        /// <summary>
        /// Construct module
        /// </summary>
        public ModuleExtension() { }
        /// <summary>
        /// Construct Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleExtension(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "extension")
                throw new ApplicationException("Cannot parse: Not an FPDL host description");
            try
            {
                VendorName = fpdl.Element("vendorName").Value;
                foreach (XElement param in fpdl.Elements("parameter"))
                    Parameters.Add(param.Element("name").Value, param.Element("value").Value);
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
            XElement fpdl = new XElement("extension",
                new XElement("vendorName", VendorName)
                );
            foreach (KeyValuePair<string, string> param in Parameters)
                fpdl.Add(new XElement(param.Key, param.Value));
            return fpdl;
        }

        /// <summary>
        /// String representation of Extension module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Extension:\n");
            str.AppendFormat("\tVendor Name: {0}\n", VendorName);
            foreach (KeyValuePair<string, string> param in Parameters)
                str.AppendFormat("\tParameter: Name = {0}, Value = {1}\n", param.Key, param.Value);
            return str.ToString();
        }
    }
}
