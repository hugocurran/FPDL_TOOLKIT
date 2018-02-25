using FPDL.Common;
using FPDL.Deploy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
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
        public Enums.ModuleType ModuleType;
        /// <summary>
        /// Parameter specifications
        /// </summary>
        public List<Specification> Specifications = new List<Specification>();

        /// <summary>
        /// Construct Module object
        /// </summary>
        /// <param name="moduleType"></param>
        public Module(Enums.ModuleType moduleType)
        {
            ModuleType = moduleType;
        }
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
                ModuleType = (Enums.ModuleType)Enum.Parse(typeof(Enums.ModuleType), fpdl.Element("moduleType").Value);
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

        internal void ApplyPattern(IModule module)
        {
            Type type = null;
            switch (ModuleType)
            {
                case Enums.ModuleType.@interface:
                    type = typeof(ModuleInterface);
                    break;
            }
            PropertyInfo[] properties = type.GetProperties();

            // Match the pre-defined specifications to the appropriate variable in the module
            DeployIfAttribute a;
            foreach (PropertyInfo p in properties)
            {
                a = (DeployIfAttribute)p.GetCustomAttribute(typeof(DeployIfAttribute));
                foreach (Specification spec in Specifications)
                {
                    if (spec.ParamName == a.FpdlName)
                    {
                        p.SetValue(module, spec.Value);
                    }
                }
            }
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[Specifications.Count];
            for (int i = 0; i < Specifications.Count; i++)
                t[i] = Specifications[i].GetNode();

            TreeNode a = new TreeNode("Module = " + ModuleType.ToString().ToUpperFirst(), t)
            {
                ToolTipText = "Right-click to add specification",
                Tag = this
            };
            return a;
        }
    }
}