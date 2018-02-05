using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// PolicyException
    /// </summary>
    public class PolicyException
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        public string ParameterName;
        /// <summary>
        /// Entity IDs
        /// </summary>
        public List<Guid> EntityIDs = new List<Guid>();
        /// <summary>
        /// Release Statements
        /// </summary>
        public List<IReleaseStatement> ReleaseStatements = new List<IReleaseStatement>();

        /// <summary>
        /// Construct PolicyException object
        /// </summary>
        public PolicyException() { }
        /// <summary>
        /// Construct PolicyException objext from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyException(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Deserialise PolicyException from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "exception")
                throw new ApplicationException("Cannot parse: Not an FPDL Exception description");
            try
            {
                ParameterName = fpdl.Element("ParameterName").Value;
                foreach (XElement ex in fpdl.Elements("entityID"))
                    EntityIDs.Add(Guid.Parse(fpdl.Elements("entityID").ToString()));
                 foreach (XElement statement in fpdl.Element("releaseStatements").Descendants())
                        ReleaseStatements.Add(ReleaseStatement.FromFPDL(statement));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("HlaInteraction parse error: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise PolicyException to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("exception",
                new XElement("parameterName", ParameterName)
                );
            foreach (Guid entity in EntityIDs)
                fpdl.Add(new XElement("entityID", entity.ToString()));
            XElement rs = new XElement("releaseStatements");
            foreach (IReleaseStatement statement in ReleaseStatements)
                rs.Add(statement.ToFPDL());
            fpdl.Add(rs);
            return fpdl;
        }
        /// <summary>
        /// String representation of PolicyException
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("Parameter: {0} ", ParameterName);
            foreach (Guid entity in EntityIDs)
                str.AppendFormat("Entity: {0} ", entity);
            foreach (IReleaseStatement statement in ReleaseStatements)
                str.AppendFormat("ReleaseStatement: {0}\n", statement);
            return str.ToString();
        }
    }
}