using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Common
{
    /// <summary>
    /// HLA interaction 
    /// </summary>
    public class HlaInteraction
    {
        /// <summary>
        /// Interaction class name
        /// </summary>
        [DeployIf("interactionClassName", "Fully qualified ingteraction classname")]
        public string InteractionClassName { get; set; }
        /// <summary>
        /// Interaction parameters
        /// </summary>
        [DeployIf("PARAMETERS", "Parameters", true, true)]
        public List<HlaParameter> Parameters { get; set; }

        /// <summary>
        /// Construct an HlaInteraction object
        /// </summary>
        public HlaInteraction()
        {
            Parameters = new List<HlaParameter>();
        }
        /// <summary>
        /// Construct and HlaInteraction object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public HlaInteraction(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Construct an HlaInteraction object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        /// <returns></returns>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "interaction")
                throw new ApplicationException("Cannot parse: Not an FPDL HLA Interaction description");

            try
            {
                InteractionClassName = fpdl.Element("interactionClassName").Value;
                // There may not be any parameters
                if (fpdl.Element("parameterName") != null)
                {
                    foreach (XElement param in fpdl.Elements("parameterName"))
                    {
                        HlaParameter parameter = new HlaParameter { ParameterName = param.Value };
                        if (param.Attribute("dataType") != null)
                            parameter.DataType = param.Attribute("dataType").Value;
                        if (param.Attribute("defaultValue") != null)
                            parameter.DataType = param.Attribute("defaultValue").Value;
                        Parameters.Add(parameter);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                //string ms = e.Message;
                throw new ApplicationException("HlaInteraction parse error: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise HLA interaction into FPDL
        /// </summary>
        /// <returns>FPDL HLA interaction</returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdlType =
                new XElement(ns + "interaction",
                    new XElement(ns + "interactionClassName", InteractionClassName)
                );
            // There may not be any parameters
            if (Parameters.Count > 0)
            {
                foreach (HlaParameter param in Parameters)
                {
                    XElement _a = new XElement(ns + "parameterName", param.ParameterName);
                    if (param.DataType != null)
                        _a.SetAttributeValue("dataType", param.DataType);
                    if ((param.DefaultValue != null) && (param.DataType == null))
                        throw new ApplicationException("HlaInteraction.ToXML: defaultValue defined with null dataType. AttributeName = " + param.ParameterName);
                    else
                        _a.SetAttributeValue("defaultValue", param.DefaultValue);
                    fpdlType.Add(_a);
                }
            }
            return fpdlType;
        }
        /// <summary>
        /// String representation of an HLA Interaction
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("\tInteractionClassName: {0}\n", InteractionClassName);
            foreach (HlaParameter param in Parameters)
            {
                str.AppendFormat("\t\tParameter: {0}\n", param.ToString());
            }
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[Parameters.Count];
            for (int i = 0; i < Parameters.Count; i++)
                t[i] = Parameters[i].GetNode();
            TreeNode a = new TreeNode(InteractionClassName, t);
            a.ToolTipText = "Interaction class name";
            a.Tag = this;
            return a;
        }
    }
}
