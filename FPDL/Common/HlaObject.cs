using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Common
{
    /// <summary>
    /// HLA object 
    /// </summary>
    public class HlaObject
    {
        /// <summary>
        /// Object class name
        /// </summary>
        [DeployIf("objectClassName", "Fully qualified object classname")]
        public string ObjectClassName { get; set; }
        /// <summary>
        /// Object attributes
        /// </summary>
        [DeployIf("ATTRIBUTES", "Attributes", true, true)]
        public List<HlaAttribute> Attributes { get; set; }

        /// <summary>
        /// Construct an HlaObject
        /// </summary>
        public HlaObject()
        {
            Attributes = new List<HlaAttribute>();
        }
        /// <summary>
        /// Construct an HlaObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public HlaObject(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Construct an HlaObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        /// <returns></returns>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "object")
                throw new ApplicationException("Cannot parse: Not an FPDL HLA Object description");
            try
            {
                ObjectClassName = fpdl.Element("objectClassName").Value;
                // There may not be any attributes
                if (fpdl.Element("attributeName") != null)
                {
                    foreach (XElement attrib in fpdl.Elements("attributeName"))
                    {
                        HlaAttribute attribute = new HlaAttribute { AttributeName = attrib.Value };
                        if (attrib.Attribute("dataType") != null)
                            attribute.DataType = attrib.Attribute("dataType").Value;
                        if (attrib.Attribute("defaultValue") != null)
                            attribute.DefaultValue = attrib.Attribute("defaultValue").Value;
                        Attributes.Add(attribute);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("HlaObject parse error: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise HlaObject to FPDL
        /// </summary>
        /// <returns>FPDL HLA object</returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdlType =
                new XElement(ns + "object",
                    new XElement(ns + "objectClassName", ObjectClassName)
                );
            // There may not be any attributes
            if (Attributes.Count > 0)
            {
                foreach (HlaAttribute attrib in Attributes)
                {
                    XElement _a = new XElement(ns + "attributeName", attrib.AttributeName);
                    if (attrib.DataType != null)
                        _a.SetAttributeValue("dataType", attrib.DataType);
                    if ((attrib.DefaultValue != null) && (attrib.DataType == null))
                        throw new ApplicationException("HlaObject.ToXML: defaultValue defined with null dataType. AttributeName = " + attrib.AttributeName);
                    else
                        _a.SetAttributeValue("defaultValue", attrib.DefaultValue);
                     fpdlType.Add(_a);
                }
            }
            return fpdlType;
        }
        /// <summary>
        /// String representation of HlaObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("\tObjectClassName: {0}\n", ObjectClassName);
            foreach (HlaAttribute attrib in Attributes)
            {
                str.AppendFormat("\t\tAttribute: {0}\n", attrib.ToString());
            }
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[Attributes.Count];
            for (int i = 0; i < Attributes.Count; i++)
                t[i] = Attributes[i].GetNode();
            TreeNode a = new TreeNode(ObjectClassName, t);
            a.ToolTipText = "Object class name";
            a.Tag = null;
            return a;
        }
    }
}

