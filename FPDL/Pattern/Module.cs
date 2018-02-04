using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Pattern Module
    /// </summary>
    public class Module
    {
        /// <summary>
        /// Module type
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Interface
            /// </summary>
            @interface, // character literal to protocol c# reserved word
            /// <summary>
            /// OSP
            /// </summary>
            osp,
            /// <summary>
            /// Export Policy
            /// </summary>
            export,
            /// <summary>
            /// Import Policy
            /// </summary>
            import,
            /// <summary>
            /// Filter
            /// </summary>
            filter,
            /// <summary>
            /// Federation
            /// </summary>
            federation,
            /// <summary>
            /// Vendor extension
            /// </summary>
            extension
        }
        /// <summary>
        /// Module type
        /// </summary>
        public Module.Type ModuleType;
        /// <summary>
        /// Parameter specifications
        /// </summary>
        public List<Specification> Specifications = new List<Specification>();
        /// <summary>
        /// Construct Module object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Module(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Module object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "module")
                throw new ApplicationException("Cannot parse: Not an FPDL module description");
            try
            {
                ModuleType = (Module.Type)Enum.Parse(typeof(Module.Type), fpdl.Element("moduleType").Value);
                foreach (XElement spec in fpdl.Descendants("specification"))
                    Specifications.Add(new Specification(spec));
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid module type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Module parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Module object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("module",
                new XElement("moduleType", ModuleType.ToString())
                );
            foreach (Specification spec in Specifications)
                fpdl.Add(spec.ToFPDL());
            return fpdl;
        }
        /// <summary>
        /// String representation of Module object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("module\n");
            str.AppendFormat("  Type: {0}\n", ModuleType);
            foreach (Specification spec in Specifications)
                str.Append(spec.ToString());
            return str.ToString();
        }
    }
}