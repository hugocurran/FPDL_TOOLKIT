using System;
using System.Collections.Generic;
using System.Text;
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
        public string InteractionClassName;
        /// <summary>
        /// Interaction parameters
        /// </summary>
        public List<HlaParameter> Parameters = new List<HlaParameter>();
        /// <summary>
        /// Construct an HlaInteraction object
        /// </summary>
        public HlaInteraction() { }
        /// <summary>
        /// Construct an HlaInteraction object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        /// <returns></returns>
        public static HlaInteraction FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "interaction")
                throw new ApplicationException("Cannot parse: Not an FPDL HLA Interaction description");

            HlaInteraction obj = new HlaInteraction();
            try
            {
                obj.InteractionClassName = fpdl.Element("interactionClassName").Value;
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
                        obj.Parameters.Add(parameter);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                //string ms = e.Message;
                throw new ApplicationException("HlaInteraction parse error: " + e.Message);
            }
            return obj;
        }

        /// <summary>
        /// Serialise HLA interaction into FPDL
        /// </summary>
        /// <returns>FPDL HLA interaction</returns>
        public XElement ToFPDL()
        {
            XElement fpdlType =
                new XElement("interaction",
                    new XElement("interactionClassName", InteractionClassName)
                );
            // There may not be any parameters
            if (Parameters.Count > 0)
            {
                foreach (HlaParameter param in Parameters)
                {
                    XElement _a = new XElement("parameterName", param.ParameterName);
                    if (param.DataType != null)
                        _a.SetAttributeValue("dataType", param.DataType);
                    if ((param.DefaultValue != null) && (param.DataType != null))
                        _a.SetAttributeValue("defaultValue", param.DefaultValue);
                    else
                        throw new ApplicationException("HlaInteraction.ToXML: defaultValue defined with null dataType. AttributeName = " + param.ParameterName);
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
    }
}
