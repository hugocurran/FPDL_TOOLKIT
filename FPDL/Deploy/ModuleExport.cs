using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Export policy module.
    /// </summary>
    public class ModuleExport : IModule
    {
        // See the FPDL definition
        // export
        // interfaceName
        //  source
        //      federateSource | entitySource
        //      object
        //      interaction
        
        /// <summary>
        /// Interface binding for the policy
        /// </summary>
        public string InterfaceName { get; set; }
        /// <summary>
        /// Sources
        /// </summary>
        public IReadOnlyList<Source> Sources { get { return _sources; } }

        private List<Source> _sources = new List<Source>();
        /// <summary>
        /// Construct Export module
        /// </summary>
        public ModuleExport() { }
        /// <summary>
        /// Construct Export module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleExport(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Export module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "export")
                throw new ApplicationException("Cannot parse: Not an FPDL export description");
            try
            {
                if (fpdl.Element("interfaceName") != null)
                    InterfaceName = fpdl.Element("interfaceName").Value;

                foreach (XElement source in fpdl.Elements("source"))
                    _sources.Add(new Source(source));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Export module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("export");
            fpdl.Add(new XElement("interfaceName", InterfaceName));

            foreach (Source sub in _sources)
            {
                fpdl.Add(sub.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of Export module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Export Policy:\n");
            str.AppendFormat("\tInterfaceName: {0}\n", InterfaceName);
            foreach (Source sub in _sources)
                str.AppendFormat("\t{0}\n", sub.ToString());
            return str.ToString();
        }
    }
}
