using System;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Entity
    /// </summary>
    public class PolicyEntity
    {
        /// <summary>
        /// Entity friendly name
        /// </summary>
        public string EntityName;
        /// <summary>
        /// Entity ID (NETN ID)
        /// </summary>
        public Guid EntityId;
        /// <summary>
        /// Entity publish
        /// </summary>
        public PolicyPublish Publish;

        /// <summary>
        /// Construct Entity from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyEntity(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Entity from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "entity")
                throw new ApplicationException("Cannot parse: Not an Entity description");
            try
            {
                EntityName = fpdl.Element("entityName").Value;
                EntityId = Guid.Parse(fpdl.Element("entityID").Value);
                Publish = new PolicyPublish(fpdl.Element("publish"));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Entity parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Entity object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("entity",
                new XElement("entityName", EntityName),
                new XElement("entityID", EntityId.ToString()),
                new XElement(Publish.ToFPDL())
                );
            return fpdl;
        }
    }
}