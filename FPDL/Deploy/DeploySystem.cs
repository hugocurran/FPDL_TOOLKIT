using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// System object
    /// </summary>
    public class DeploySystem
    {
        /// <summary>
        /// Federate Name
        /// </summary>
        [DeployIf("federateName", "Federate name")]
        public string FederateName { get; set; }
        /// <summary>
        /// Components
        /// </summary>
        [DeployIf("Components", "List of components", false, true)]
        public List<Component> Components { get; set; }
        /// <summary>
        /// System type
        /// </summary>
        [DeployIf("System Type", "Type of system")]
        public Enums.FederateType SystemType { get; set; }
        //public Enums.PatternType SystemType { get; set; }
        /// <summary>
        /// Pattern name
        /// </summary>
        [DeployIf("pattern", "Pattern name")]
        public string Pattern { get; set; }
        /// <summary>
        /// Pattern reference
        /// </summary>
        [DeployIf("patternReference", "Pattern reference")]
        public Guid PatternRef { get; set; }
        /// <summary>
        /// Pattern Type (internal use)
        /// </summary>
        [DeployIf("patternType", "Pattern Type", true)]
        public Enums.PatternType PatternType { get; set; }
        /// <summary>
        /// Construct System object
        /// </summary>
        public DeploySystem()
        {
            Components = new List<Component>();
            PatternType = Enums.PatternType.NotApplicable;
        }


        /// <summary>
        /// Construct System object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public DeploySystem(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise System object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        { 
            if (fpdl.Name != "system")
                throw new ApplicationException("Cannot parse: Not an FPDL system description");
            try
            {
                SystemType = (Enums.FederateType)Enum.Parse(typeof(Enums.FederateType), fpdl.Attribute("systemType").Value);
                Pattern = fpdl.Element("pattern").Value;
                PatternRef = Guid.Parse(fpdl.Element("pattern").Attribute("patternReference").Value);
                FederateName = fpdl.Element("federateName").Value;

                foreach (XElement component in fpdl.Elements("component"))
                    Components.Add(new Component(component));
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid system type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("System parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise System object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("system",
                    new XAttribute("systemType", SystemType.ToString()),
                new XElement("pattern", Pattern,
                    new XAttribute("patternReference", PatternRef.ToString()),
                new XElement("federateName", FederateName)
                )
            );
            foreach (Component component in Components)
            {
                fpdl.Add(component.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of System object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("\nSystem\n");
            str.AppendFormat("  Type: {0}\n", SystemType);
            str.AppendFormat("  Pattern: {0} ({1})\n", Pattern, PatternRef);
            str.AppendFormat("  Federate Name: {0}\n", FederateName);
            foreach (Component comp in Components)
                str.AppendFormat("\n{0}", comp);
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t1 = new TreeNode[3];
            t1[0] = new TreeNode("Federate name = " + FederateName);
            t1[0].ToolTipText = "Federate Name";
            t1[1] = new TreeNode("Pattern name = " + Pattern);
            t1[1].ToolTipText = " Pattern Name";
            t1[2] = new TreeNode("Pattern reference = " + PatternRef.ToString());
            t1[2].ToolTipText = "Pattern Reference";

            TreeNode[] t = new TreeNode[Components.Count];
            for (int i = 0; i < Components.Count; i++)
                t[i] = Components[i].GetNode();

            TreeNode a = new TreeNode("System = " + SystemType.ToString().ToUpper());
            a.Nodes.AddRange(t1);
            a.Nodes.AddRange(t);
            a.ToolTipText = "Right-click to add pattern";
            a.Tag = this;
            return a;
        }

    }
}