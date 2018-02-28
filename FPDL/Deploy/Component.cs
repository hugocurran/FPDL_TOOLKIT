using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Deploy
{

    /// <summary>
    /// Deploy Component
    /// </summary>
    public class Component
    {
        /// <summary>
        /// Modules
        /// </summary>
        [DeployIf("configMgmt", "Configuration Management Data",false, true)]
        public List<IModule> Modules { get; set; }
        /// <summary>
        /// Component ID
        /// </summary>
        [DeployIf("configMgmt", "Configuration Management Data")]
        public Guid ComponentID { get; set; }
        /// <summary>
        /// Component Type
        /// </summary>
        [DeployIf("configMgmt", "Configuration Management Data")]
        public Enums.ComponentType ComponentType { get; set; }
        /// <summary>
        /// Friendly name for component
        /// </summary>
        [DeployIf("componentName", "Friendly name for component", true)]
        public string ComponentName { get; set; }
        /// <summary>
        /// Construct Component object
        /// </summary>
        public Component()
        {
            Modules = new List<IModule>();
        }
        /// <summary>
        /// Construct Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Component(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Component from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "component")
                throw new ApplicationException("Cannot parse: Not an FPDL component description");
            try
            {
                ComponentID = Guid.Parse(fpdl.Attribute("componentID").Value);
                ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), fpdl.Attribute("componentType").Value);
                if (fpdl.Attribute("componentName") != null)
                    ComponentName = fpdl.Attribute("componentName").Value;
                ModuleFactory.Create(fpdl, this);
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
        /// Serialise Component to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("component",
                    new XElement("componentID", ComponentID.ToString()),
                    new XElement("componentType", ComponentType.ToString())
                    );
            if (ComponentName != "")
                fpdl.Add(new XAttribute("componentName", ComponentName));

            foreach (IModule module in Modules)
            {
                fpdl.Add(module.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of Component
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("=======\nComponent ");
            str.AppendFormat("({0}):\n", ComponentID);
            str.AppendFormat("  Type: {0}\n", ComponentType);
            str.AppendFormat("  Name: {0}\n", ComponentName);
            foreach (IModule mod in Modules)
                str.AppendFormat("  {0}\n", mod.ToString());
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
                ToolTipText = "Component ID reference"
            };
            t1[1] = new TreeNode("Component Name = " + ComponentName)
            {
                ToolTipText = "Component friendly name"
            };

            TreeNode[] t = new TreeNode[Modules.Count];
            for (int i = 0; i < Modules.Count; i++)
                t[i] = Modules[i].GetNode();

            TreeNode a = new TreeNode("Component = " + ComponentType.ToString());
            a.Nodes.AddRange(t1);
            a.Nodes.AddRange(t);
            a.ToolTipText = "Component";
            a.Tag = this;
            return a;
        }
    }
}

