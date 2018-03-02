using FPDL.Common;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static FPDL.Common.Enums;

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
        [DeployIf("vendorName", "Name of the vendor")]
        public string VendorName { get; set; }
        /// <summary>
        /// Parameters (Key, Value) pairs
        /// </summary>
        [DeployIf("PARAMETERS", "Vendor-specific parameters", false, true)]
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.extension;
        }

        /// <summary>
        /// Construct module
        /// </summary>
        public ModuleExtension()
        {
            Parameters = new Dictionary<string, string>();
        }
        /// <summary>
        /// Construct Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleExtension(XElement fpdl) : this()
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
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[Parameters.Count];
            int i = 0;
            foreach (var param in Parameters)
            {
                t[i++] = new TreeNode(param.Key + " = " + param.Value);
                t[i].Tag = new Specification { ParamName = param.Key, Value = param.Value };
            }

            TreeNode[] t1 = new TreeNode[1];
            t1[0] = new TreeNode("Vendor name = " + VendorName, t);
            t1[0].Tag = new Specification { ParamName = "vendorName", Value = VendorName };

            TreeNode a = new TreeNode("Extensions", t1)
            {
                ToolTipText = "Extensions Module",
                Tag = this
            };
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
                case "vendorName":
                    VendorName = specification.Value;
                    break;
                default:
                    Parameters.Add(specification.ParamName, specification.Value);
                    break;
            }
        }
    }
}
