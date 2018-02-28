using FPDL.Common;
using FPDL.Deploy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Component
    /// </summary>
    public class PatternComponent
    {
        /// <summary>
        /// Component type
        /// </summary>
        public Enums.ComponentType ComponentType;
        /// <summary>
        /// Component ID
        /// </summary>
        public Guid ComponentID;
        /// <summary>
        /// Component name
        /// </summary>
        public string ComponentName;
        /// <summary>
        /// Modules
        /// </summary>
        public List<Module> Modules = new List<Module>();

        /// <summary>
        /// Construct Component object
        /// </summary>
        public PatternComponent() { }
        /// <summary>
        /// Construct Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PatternComponent(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "component")
                throw new ApplicationException("Cannot parse: Not an FPDL component description");
            try
            {
                ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), fpdl.Element("componentType").Value);
                ComponentID = Guid.Parse(fpdl.Element("componentID").Value);
                ComponentName = fpdl.Element("componentName").Value;
                foreach (XElement module in fpdl.Descendants("module"))
                    Modules.Add(new Module(module));
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid component type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Component parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Component object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("component",
                new XElement("componentType", ComponentType.ToString()),
                new XElement("componentID", ComponentID.ToString()),
                new XElement("componentName", ComponentName)
                );
            foreach (Module module in Modules)
                fpdl.Add(module.ToFPDL());
            return fpdl;
        }
        /// <summary>
        /// String representation of Component object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("component\n");
            str.AppendFormat("  Type: {0} ({1})\n", ComponentType, ComponentID);
            foreach (Module module in Modules)
                str.Append(module.ToString());
            return str.ToString();
        }

        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t1 = new TreeNode[2];
            t1[0] = new TreeNode("Component ID = " + ComponentID.ToString())
            {
                ToolTipText = "Component ID"
            };
            t1[1] = new TreeNode("Component Name = " + ComponentName)
            {
                ToolTipText = "Right-click to edit",
                Tag = this
            };

            TreeNode[] t = new TreeNode[Modules.Count];
            for (int i = 0; i < Modules.Count; i++)
                t[i] = Modules[i].GetNode();

            TreeNode a = new TreeNode("Component (" + ComponentType.ToString().ToUpper() + ")");
            a.Nodes.AddRange(t1);
            a.Nodes.AddRange(t);
            a.ToolTipText = "Right-click to add modules";
            a.Tag = this;
            return a;
        }
    }
}
