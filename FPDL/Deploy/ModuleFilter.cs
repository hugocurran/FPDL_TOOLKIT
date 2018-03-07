using FPDL.Common;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// Filter Module
    /// </summary>
    public class ModuleFilter : IModule
    {
        /// <summary>
        /// UUID of the filter
        /// </summary>
        [DeployIf("filterID", "UUID of the filter entity")]
        public Guid FilterId { get; set; }
        /// <summary>
        /// Filter rules
        /// </summary>
        List<FilterRule> Rules { get; set; }
        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.filter;
        }

        /// <summary>
        /// Construct a Host Module
        /// </summary>
        public ModuleFilter()
        {
            Rules = new List<FilterRule>();
        }
        /// <summary>
        /// Construct an Filter module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleFilter(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Filter module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "filter")
                throw new ApplicationException("Cannot parse: Not an FPDL filter description");
            try
            {
                FilterId = Guid.Parse(fpdl.Element("filterID").Value);
                foreach (XElement rule in fpdl.Elements("rule"))
                {
                    Rules.Add(new FilterRule(rule));
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Filter module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "filter",
                new XElement(ns + "filterID", FilterId.ToString())
            );
            foreach (FilterRule rule in Rules)
                fpdl.Add(rule.ToFPDL(ns));
            return fpdl;
        }
        /// <summary>
        /// String representation of Filter module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Filter:\n");
            str.AppendFormat("\tFilter ID: {0}\n", FilterId);
            foreach (FilterRule rule in Rules)
                str.AppendFormat("{0}\n", rule);
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t0 = new TreeNode[1];
            t0[0] = new TreeNode("Filter ID = " + FilterId.ToString());
            t0[0].Tag = null;
            t0[0].ToolTipText = "Filter ID";

            TreeNode[] t1 = new TreeNode[Rules.Count];
            for (int i = 0; i < Rules.Count; i++)
            {
                t1[i] = Rules[i].GetNode();                
            }
            TreeNode a = new TreeNode("Filter");
            a.Nodes.AddRange(t0);
            a.Nodes.AddRange(t1);
            a.ToolTipText = "Filter Module";
            a.Tag = null;
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
                case "filterID":
                    FilterId = Guid.Parse(specification.Value);
                    break;
                    // Need to figure out time and logging
            }
        }
    }
    /// <summary>
    /// Filter rule
    /// </summary>
    public class FilterRule
    {
        /// <summary>
        /// Numeric reference for the filter
        /// </summary>
        public string Ref;
        /// <summary>
        /// Model for the rule
        /// </summary>
        public string Model;
        /// <summary>
        /// Parameter list
        /// </summary>
        public List<FilterParameter> Parameters = new List<FilterParameter>();
        /// <summary>
        /// Filter Rule
        /// </summary>
        public FilterRule()
        {
            Model = "";
            Ref = "";
        }
        /// <summary>
        /// Construct an Filter Rule from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public FilterRule(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Filter Rule from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "rule")
                throw new ApplicationException("Cannot parse: Not an FPDL ruler description");
            try
            {
                Ref = fpdl.Attribute("ref").Value;
                Model = fpdl.Attribute("model").Value;

                foreach (XElement param in fpdl.Element("parameters").Descendants())
                    Parameters.Add(new FilterParameter(param));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise Host module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "rule",
                new XAttribute("ref", Ref),
                new XAttribute("model", Model),
                new XElement(ns + "parameters")
            );
            foreach (var param in Parameters)
                fpdl.Element(ns + "parameters").Add(param.ToFPDL(ns));
            return fpdl;
        }
        
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t0 = new TreeNode[2];
            t0[0] = new TreeNode("Reference = " + Ref);
            t0[0].Tag = null;
            t0[0].ToolTipText = "Rule reference";
            t0[1] = new TreeNode("Model = " + Model);
            t0[1].Tag = null;
            t0[1].ToolTipText = "Rule model";

            TreeNode[] t1 = new TreeNode[Parameters.Count];
            for (int i = 0; i < Parameters.Count; i++)
            {
                t1[i] = Parameters[i].GetNode();
            }
            TreeNode a = new TreeNode("Rule");
            a.Nodes.AddRange(t0);
            a.Nodes.AddRange(t1);
            a.ToolTipText = "Filter Rule";
            a.Tag = null;
            return a;
        }

        /// <summary>
        /// Filter parameters
        /// </summary>
        public class FilterParameter
        {
            public enum Type
            {
                actionParam,
                decimalParam,
                entityParam,
                hlaAttributeParam,
                hlaObjectParam,
                hlaParameterParam,
                hlaInteractionParam,
                integerParam,
                logicParam,
                stringParam
            }

            public Type ParameterType { get; set; }
            public string ParameterValue { get; set; }
            public string ParameterRef { get; set; }
            public string ParameterDescription { get; set; }
            public string ParameterMetric { get; set; }

            public FilterParameter()
            {
                ParameterMetric = "";
                ParameterValue = "";
                ParameterRef = "";
                ParameterDescription = "";
            }

            /// <summary>
            /// Construct an Interface module from FPDL
            /// </summary>
            /// <param name="fpdl"></param>
            public FilterParameter(XElement fpdl) : this()
            {
                FromFPDL(fpdl);
            }
            /// <summary>
            /// Deserialise Interface module from FPDL
            /// </summary>
            /// <param name="fpdl"></param>
            public void FromFPDL(XElement fpdl)
            {
                try
                {
                    ParameterType = (Type)Enum.Parse(typeof(Type), fpdl.Name.LocalName);
                    ParameterValue = fpdl.Value;
                    ParameterRef = fpdl.Attribute("ref").Value;
                    ParameterDescription = fpdl.Attribute("description").Value;
                    ParameterMetric = fpdl.Attribute("metric").Value ?? "";
                }
                catch (ArgumentException e)
                {
                    throw new ApplicationException("Invalid paramater type: " + e.Message);
                }
                catch (NullReferenceException e)
                {
                    throw new ApplicationException("Cannot parse: " + e.Message);
                }
            }

            /// <summary>
            /// Serialise Parameter to FPDL
            /// </summary>
            /// <returns></returns>
            public XElement ToFPDL(XNamespace ns)
            {
                XElement fpdl = new XElement(ns + ParameterType.ToString(), ParameterValue,
                    new XAttribute("ref", ParameterRef),
                    new XAttribute("description", ParameterDescription),
                    new XAttribute("metric", ParameterMetric)
                );
                return fpdl;
            }

            /// <summary>
            /// Get a TreeNode
            /// </summary>
            /// <returns></returns>
            public TreeNode GetNode()
            {
                TreeNode[] t0 = new TreeNode[4];
                t0[0] = new TreeNode("Type = " + ParameterType.ToString());
                t0[0].Tag = null;
                t0[0].ToolTipText = "Parameter type";
                t0[1] = new TreeNode("Ref = " + ParameterRef);
                t0[1].Tag = null;
                t0[1].ToolTipText = "Parameter ref";
                t0[2] = new TreeNode("Description = " + ParameterDescription);
                t0[2].Tag = null;
                t0[2].ToolTipText = "Parameter description";
                t0[3] = new TreeNode("Metric = " + ParameterMetric);
                t0[3].Tag = null;
                t0[3].ToolTipText = "Parameter metric";

                TreeNode a = new TreeNode("Parameter");
                a.Nodes.AddRange(t0);
                a.ToolTipText = "Parameter";
                a.Tag = null;
                return a;
            }
        }
    }
}
