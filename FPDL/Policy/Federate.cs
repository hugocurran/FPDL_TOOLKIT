using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Federate
    /// </summary>
    public class Federate
    {
        /// <summary>
        /// Federate Type
        /// </summary>
        public Sys.Type FederateType;
        /// <summary>
        /// Federate Name
        /// </summary>
        public string FederateName;
        /// <summary>
        /// Security Owner
        /// </summary>
        public string SecurityOwner;
        /// <summary>
        /// Security Classification
        /// </summary>
        public string SecurityClassification;
        /// <summary>
        /// Security Caveat (optional)
        /// </summary>
        public string SecurityCaveat;
        /// <summary>
        /// Physical Location
        /// </summary>
        public string PhysicalLocation;
        /// <summary>
        /// Description (optional)
        /// </summary>
        public string Description;
        /// <summary>
        /// Publish (optional)
        /// </summary>
        public PolicyPublish Publish;
        /// <summary>
        /// Entities (optional)
        /// </summary>
        public List<PolicyEntity> Entities = new List<PolicyEntity>();
        /// <summary>
        /// Construct a Federate object
        /// </summary>
        public Federate() { }
        /// <summary>
        /// Construct a Federate object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Federate(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Federate object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "federate")
                throw new ApplicationException("Cannot parse: Not an FPDL federate description");
            try
            {
                FederateType = (Sys.Type)Enum.Parse(typeof(Sys.Type), fpdl.Attribute("federateType").Value);
                FederateName = fpdl.Element("federateName").Value;
                SecurityOwner = fpdl.Element("securityOwner").Value;
                SecurityClassification = fpdl.Element("securityClassification").Value;
                if (fpdl.Element("securityCaveat") != null)
                    SecurityCaveat = fpdl.Element("securityCaveat").Value;
                PhysicalLocation = fpdl.Element("physicalLocation").Value;
                if (fpdl.Element("description") != null)
                    Description = fpdl.Element("description").Value;
                if (fpdl.Element("publish") != null)
                    Publish = new PolicyPublish(fpdl.Element("publish"));
                if (fpdl.Element("entity") != null)
                    foreach (XElement entity in fpdl.Elements("entity"))
                    {
                        Entities.Add(new PolicyEntity(entity));
                    }
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid federate type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Federate parse error: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise Federate object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("federate",
                new XElement("federateType", FederateType.ToString()),
                new XElement("federateName", FederateName),
                new XElement("securityOwner", SecurityOwner),
                new XElement("securityClassification", SecurityClassification)
                );
            if (SecurityCaveat != null)
                fpdl.Add(new XElement("securityCaveat", SecurityCaveat));
            fpdl.Add(new XElement("physicalLocation", PhysicalLocation));
            if (Description != null)
                fpdl.Add(new XElement("description", Description));
            if (Publish != null)
                fpdl.Add(Publish.ToFPDL());
            if (Entities != null)
                foreach (PolicyEntity entity in Entities)
                    fpdl.Add(entity.ToFPDL());
            return fpdl;
        }
    }
}