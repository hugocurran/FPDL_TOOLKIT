using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Redefinition of HlaAttribute for Policy
    /// </summary>
    /// 
    public class PolicyHlaAttribute
    {
        /// <summary>
        /// Attribute name
        /// </summary>
        public string AttributeName;
        /// <summary>
        /// Default Value (optional)
        /// </summary>
        public string DefaultValue;
        /// <summary>
        /// Data Type (required if Default Value set)
        /// </summary>
        public string DataType;
        /// <summary>
        /// Release statements for the Attribute
        /// </summary>
        public List<IReleaseStatement> ReleaseStatements = new List<IReleaseStatement>();

        
        /// <summary>
        /// Construct a PolicyHlaAttribute
        /// </summary>
        public PolicyHlaAttribute() { }
        /// <summary>
        /// Construct a PolicyHlaAttribute from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyHlaAttribute(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise PolicyHlaAttribute from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "attribute")
                throw new ApplicationException("Cannot parse: Not an FPDL attribute description");
            try
            {
                AttributeName = fpdl.Element("attributeName").Value;
                if (fpdl.Element("defaultValue") != null)
                {
                    DefaultValue = fpdl.Element("defaultValue").Value;
                    DataType = fpdl.Element("defaultValue").Attribute("dataType").Value;
                }
                if (fpdl.Element("releaseStatements") != null)
                {
                    foreach (XElement statement in fpdl.Element("releaseStatements").Descendants())
                        ReleaseStatements.Add(ReleaseStatement.FromFPDL(statement));
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise PolicyHlaAttribute to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("attribute",
                new XElement("attributeName", AttributeName));
            if (DefaultValue != null)
                fpdl.Add(new XElement("defaultValue", DefaultValue, new XAttribute("dataType", DataType)));
            if (ReleaseStatements.Count > 0)
            {
                XElement rs = new XElement("releaseStatements");
                foreach (IReleaseStatement statement in ReleaseStatements)
                    rs.Add(statement.ToFPDL());
                fpdl.Add(rs);
            }
            return fpdl;
        }
    }
}
