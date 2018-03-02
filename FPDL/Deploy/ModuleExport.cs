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
        [DeployIf("interfaceName", "Interface binding for export policy")]
        public string InterfaceName { get; set; }
        /// <summary>
        /// Sources
        /// </summary>
        [DeployIf("SOURCE", "Source federate or entity", false, true)]
        public IReadOnlyList<Source> Sources { get { return _sources; } }

        private List<Source> _sources = new List<Source>();

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.export;
        }
        
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
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t0 = new TreeNode[1];
            t0[0] = new TreeNode(InterfaceName)
            {
                ToolTipText = "Interface Binding",
                Tag = new Specification { ParamName = "interfaceName", Value = InterfaceName }
        };

            TreeNode[] t1 = new TreeNode[Sources.Count];
            for (int i = 0; i < Sources.Count; i++)
            {
                t1[i] = Sources[i].GetNode();
                t1[i].Tag = null;
            }

            TreeNode a = new TreeNode("Export Policy");
            a.Nodes.AddRange(t0);
            a.Nodes.AddRange(t1);
            a.ToolTipText = "Export Module";
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
            }
        }


        /// <summary>
        /// Apply specifications from a Design to this module
        /// </summary>
        /// <param name="sources"></param>
        public void ApplyPattern(List<Source> sources)
        {
            if (sources.Count > 0)
                _sources = sources;
        }

    }
}
