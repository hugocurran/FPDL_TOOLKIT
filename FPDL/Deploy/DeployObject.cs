using FPDL.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// FPDL Deploy
    /// </summary>
    public class DeployObject : IFpdlObject
    {
        /// <summary>
        /// ConfigMgmt for Deploy document
        /// </summary>
        [DeployIf("configMgmt", "Configuration Management Data")]
        public ConfigMgmt ConfigMgmt { get; set; }
        /// <summary>
        /// Reference to the design document
        /// </summary>
        [DeployIf("designReference", "Design Reference")]
        public Guid DesignReference { get; set; }
        /// <summary>
        /// Systems within Deploy document
        /// </summary>
        [DeployIf("Systems", "Systems within Deploy", false, true)]
        public List<DeploySystem> Systems { get; set; }
        /// <summary>
        /// Contains either an error message of 'OK' following verify check
        /// </summary>
        [DeployIf("","",true)]
        public string VerifyResult { get; set; }

        /// <summary>
        /// Construct a DeployObject
        /// </summary>
        public DeployObject()
        {
            Systems = new List<DeploySystem>();
            ConfigMgmt = new ConfigMgmt();
        }

        /// <summary>
        /// Construct DeployObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public DeployObject(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise DeployObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "Deploy")
                throw new ApplicationException("Cannot parse: Not an FPDL Deploy file");

            ConfigMgmt = new ConfigMgmt(fpdl.Element("configMgmt"));
            DesignReference = Guid.Parse(fpdl.Element("designReference").Value);
            IEnumerable<XElement> systems = fpdl.Descendants("system");
            foreach (XElement system in systems)
            {
                Systems.Add(new DeploySystem(system));
            }
        }

        /// <summary>
        /// Verify that all required values have been set
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            if (!CheckObject(this))
                return false;

            foreach (DeploySystem system in Systems)
            {
                if (!CheckObject(system))
                    return false;
                foreach (Component component in system.Components)
                {
                    if (!CheckObject(component))
                        return false;
                    foreach (IModule module in component.Modules)
                        if (!CheckObject(module))
                            return false;
                }
            }
            return true;
        }
        // Use the DeployIfAttribute to check that non-optional values are not empty
        private bool CheckObject(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo p in pi)
            {
                DeployIfAttribute a = p.GetCustomAttribute<DeployIfAttribute>();
                if ((!a.Optional) && (!a.List))     // Not marked optional and not a list
                {
                    object value = p.GetValue(obj);
                    if (value == null)
                    {
                        VerifyResult = String.Format("Missing value for {0}", a.FpdlName);
                        return false;
                    }
                    if (value != null || !string.IsNullOrEmpty(value.ToString()))
                        continue;
                     VerifyResult = String.Format("Missing value for {0}", a.FpdlName);
                     return false;
                }
                if ((!a.Optional) && (a.List))     // Not optional and is a list
                {
                    object value = p.GetValue(obj);
                    if (value is IList)
                    {
                        if (((IList)value).Count < 1)
                        {
                            VerifyResult = String.Format("Empty collection for {0}", a.FpdlName);
                            return false;
                        }
                    }
                    if (value is IDictionary)
                    {
                        if (((IDictionary)value).Count < 1)
                        {
                            VerifyResult = String.Format("Empty collection for {0}", a.FpdlName);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

            /// <summary>
            /// Serialise DeployObject to FPDL
            /// </summary>
            /// <returns></returns>
            public XElement ToFPDL()
        {
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            XNamespace ns = XNamespace.Get("http://www.niteworks.net/fpdl");
            XElement fpdl = new XElement(ns + "Deploy",
                    new XAttribute("xmlns", ns.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName)
                );
            fpdl.Add(ConfigMgmt.ToFPDL(ns));
            fpdl.Add(new XElement("designReference", DesignReference));
            foreach(DeploySystem system in Systems)
            {
                fpdl.Add(system.ToFPDL(ns));
            }
            return fpdl;
        }
        /// <summary>
        /// Serialise DeployObject to FPDL
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public XElement ToFPDL(DeploySystem system)
        {
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace xsd = XNamespace.Get("http://www.w3.org/2001/XMLSchema");
            XNamespace ns = XNamespace.Get("http://www.niteworks.net/fpdl");
            XElement fpdl = new XElement(ns+"Deploy",
                    new XAttribute("xmlns", ns.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName)
                    );

            fpdl.Add(ConfigMgmt.ToFPDL(ns));
            fpdl.Add(new XElement(ns + "designReference", DesignReference));
            fpdl.Add(system.ToFPDL(ns));
            return fpdl;
        }

        /// <summary>
        /// String representation of DeployObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("DEPLOY =>\n");
            str.AppendFormat("{0}", ConfigMgmt);
            str.AppendFormat("Design Ref: {0}\n", DesignReference);
            foreach (DeploySystem sys in Systems)
                str.AppendFormat("{0}\n", sys);
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t1 = new TreeNode[1];
            t1[0] = new TreeNode("Design reference = " + DesignReference);
            t1[0].ToolTipText = "Design reference";

            TreeNode[] t = new TreeNode[Systems.Count];
            for (int i = 0; i < Systems.Count; i++)
                t[i] = Systems[i].GetNode();

            TreeNode a = new TreeNode("Deploy");
            a.Nodes.AddRange(t1);
            a.Nodes.AddRange(t);
            a.ToolTipText = "Deploy";
            a.Tag = this;
            return a;
        }

        /// <summary>
        /// Returns true if this object is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if ((ConfigMgmt.DocReference != null) && (Systems.Count > 0))
            {
                if (Systems[0].Components.Count > 0)
                    if (Systems[0].Components[0].Modules.Count > 0)
                        return false;
            }
            return true;
        }
    }
}
