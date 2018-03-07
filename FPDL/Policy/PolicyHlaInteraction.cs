using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Redefined HlaInteraction for Policy
    /// </summary>
    public class PolicyHlaInteraction
    {
        /// <summary>
        /// Interaction class name
        /// </summary>
        public string InteractionClassName;
        /// <summary>
        /// Release Statements
        /// </summary>
        public List<IReleaseStatement> ReleaseStatements = new List<IReleaseStatement>();
        /// <summary>
        /// Exceptions
        /// </summary>
        public List<PolicyException> Exceptions = new List<PolicyException>();

        /// <summary>
        /// Construct a PolicyHlaInteraction object
        /// </summary>
        public PolicyHlaInteraction() { }
        /// <summary>
        /// Construct a PolicyHlaInteraction object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyHlaInteraction(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise a PolicyHlaInteraction object from FPDL
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
                if (fpdl.Element("releaseStatements") != null)
                {
                    foreach (XElement statement in fpdl.Element("releaseStatements").Descendants())
                        ReleaseStatements.Add(ReleaseStatement.FromFPDL(statement));
                }
                if (fpdl.Element("exception") != null)
                {
                    foreach (XElement ex in fpdl.Elements("attribute"))
                        Exceptions.Add(new PolicyException(ex));
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("HlaInteraction parse error: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise HLA interaction into FPDL
        /// </summary>
        /// <returns>FPDL HLA interaction</returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "interaction",
                    new XElement(ns + "interactionClassName", InteractionClassName)
                );
            if (ReleaseStatements.Count > 0)
            {
                XElement rs = new XElement(ns + "releaseStatements");
                foreach (IReleaseStatement statement in ReleaseStatements)
                    rs.Add(statement.ToFPDL(ns));
                fpdl.Add(rs);
            }
            if (Exceptions.Count > 0)
            {
                foreach (PolicyException ex in Exceptions)
                {
                    fpdl.Add(ex.ToFPDL(ns));
                }
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of a PolicyHLAInteraction
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("\tInteractionClassName: {0}\n", InteractionClassName);
            foreach (IReleaseStatement statement in ReleaseStatements)
                str.AppendFormat("\t\tReleaseStatement: {0}\n", statement);
            foreach (PolicyException ex in Exceptions)
                str.AppendFormat("\t\tException: {0}\n", ex);
            return str.ToString();
        }
    }
}
