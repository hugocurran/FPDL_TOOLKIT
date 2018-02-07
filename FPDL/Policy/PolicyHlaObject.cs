using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Redefinition of HlaObject for Policy
    /// </summary>
    public class PolicyHlaObject
    {
        /// <summary>
        /// Object class name
        /// </summary>
        public string ObjectClassName;
        /// <summary>
        /// Object attributes
        /// </summary>
        public List<PolicyHlaAttribute> Attributes = new List<PolicyHlaAttribute>();
        /// <summary>
        /// Release statements for the Attribute
        /// </summary>
        public List<IReleaseStatement> ReleaseStatements = new List<IReleaseStatement>();

        /// <summary>
        /// Construct a HlaObject
        /// </summary>
        public PolicyHlaObject() { }
        /// <summary>
        /// Construct a HlaObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyHlaObject(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise HlaObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "object")
                throw new ApplicationException("Cannot parse: Not an FPDL HLA Object description");
            try
            {
                ObjectClassName = fpdl.Element("objectClassName").Value;
                if (fpdl.Element("releaseStatements") != null)
                {
                    foreach (XElement statement in fpdl.Element("releaseStatements").Elements())
                        ReleaseStatements.Add(ReleaseStatement.FromFPDL(statement));
                }
                if (fpdl.Element("attribute") != null)
                {
                    foreach (XElement attrib in fpdl.Elements("attribute"))
                        Attributes.Add(new PolicyHlaAttribute(attrib));
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
        public XElement ToFPDL()
        {
            XElement fpdl =
                new XElement("object",
                    new XElement("objectClassName", ObjectClassName)
                );
            if (ReleaseStatements.Count > 0)
            {
                XElement rs = new XElement("releaseStatements");
                foreach (IReleaseStatement statement in ReleaseStatements)
                    rs.Add(statement.ToFPDL());
                fpdl.Add(rs);
            }
            if (Attributes.Count > 0)
            {
                foreach (PolicyHlaAttribute attrib in Attributes)
                    fpdl.Add(attrib.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of HlaObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("\tObjectClassName: {0}\n", ObjectClassName);
            foreach (IReleaseStatement statement in ReleaseStatements)
                str.AppendFormat("\t\tReleaseStatement: {0}\n", statement);
            foreach (PolicyHlaAttribute attrib in Attributes)
                str.AppendFormat("\t\tAttribute: {0}\n", attrib);
            return str.ToString();
        }
    }
}
 
