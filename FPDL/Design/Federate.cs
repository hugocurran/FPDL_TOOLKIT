using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Design
{
    /// <summary>
    /// Federate
    /// </summary>
    public class Federate
    {
        /// <summary>
        /// Federate type
        /// </summary>
        public Enums.FederateType FederateType;
        /// <summary>
        /// Federation name
        /// </summary>
        public string FederateName;
        /// <summary>
        /// Policy reference
        /// </summary>
        public Guid PolicyReference;
        /// <summary>
        /// Simulator Name (optional)
        /// </summary>
        public string SimulatorName;
        /// <summary>
        /// Gateway Type (optional)
        /// </summary>
        public Enums.GatewayType GatewayType;
        /// <summary>
        /// Filter Type (optional)
        /// </summary>
        public Enums.FilterType FilterType;
        /// <summary>
        /// Filter specification (optional)
        /// </summary>
        public Filter Filter;
        /// <summary>
        /// Federate subscriptions
        /// </summary>
        public List<Source> Sources = new List<Source>();
        /// <summary>
        /// Federate-level publish
        /// </summary>
        public Publish Publish;
        /// <summary>
        /// Entity-level publish
        /// </summary>
        public List<Entity> Entities = new List<Entity>();

        /// <summary>
        /// Construct Federation object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Federate(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Federation object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "federate")
                throw new ApplicationException("Cannot parse: Not an FPDL federate description");
            try
            {
                FederateType = (Enums.FederateType)Enum.Parse(typeof(Enums.FederateType), fpdl.Attribute("federateType").Value);
                FederateName = fpdl.Element("federateName").Value;
                PolicyReference = Guid.Parse(fpdl.Element("policyReference").Value);
                if (fpdl.Element("simulatorName") != null)
                    SimulatorName = fpdl.Element("simulatorName").Value;

                if ((FederateType == Enums.FederateType.gateway) && (fpdl.Element("gateway") == null))
                    throw new ApplicationException("Missing gateway specification for a Gateway federateType");
                else if (FederateType == Enums.FederateType.gateway)
                    GatewayType = (Enums.GatewayType)Enum.Parse(typeof(Enums.GatewayType), fpdl.Element("gateway").Attribute("gatewayType").Value);

                if ((FederateType == Enums.FederateType.filter) && (fpdl.Element("filter") == null))
                    throw new ApplicationException("Missing filter specification for a Filter federateType");
                else if (FederateType == Enums.FederateType.filter)
                    Filter = new Filter(fpdl.Element("filter"));

                if (fpdl.Element("subscribe") != null)
                {
                    foreach (XElement source in fpdl.Element("subscribe").Elements("source"))
                        Sources.Add(new Source(source));
                }

                if (fpdl.Element("publish") != null)
                    Publish = new Publish(fpdl.Element("publish"));

                if (fpdl.Element("entity") != null)
                {
                    foreach (XElement entity in fpdl.Elements("entity"))
                        Entities.Add(new Entity(entity));
                }
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid federate or gateway type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Federation parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Federation object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "federate",
                new XAttribute("federateType", FederateType.ToString()),
                new XElement(ns + "federateName", FederateName),
                new XElement(ns + "policyReference", PolicyReference)
                );
            if (SimulatorName != null)
                fpdl.Add(new XElement(ns + "simulatorName", SimulatorName));
            if (FederateType == Enums.FederateType.gateway)
                fpdl.Add(new XElement(ns + "gateway", new XAttribute("gatewayType", GatewayType.ToString())));
            if (FederateType == Enums.FederateType.filter)
                fpdl.Add(new XElement(ns + "filter", Filter.ToFPDL()));
            if (Sources.Count > 0)
            {
                foreach (Source source in Sources)
                    fpdl.Add(source.ToFPDL(ns));
            }
            if (Publish != null)
                fpdl.Add(Publish.ToFPDL(ns));
            if (Entities.Count > 0)
            {
                foreach (Entity entity in Entities)
                    fpdl.Add(entity.ToFPDL(ns));
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of Component object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("Federate");
            str.AppendFormat("  Name: {0} ({1})\n", FederateName, FederateType);
            str.AppendFormat("  PolicyRef: {0}\n", PolicyReference);
            if (SimulatorName != null)
                str.AppendFormat("  SimulatorName: {0}\n", PolicyReference);
            if (FederateType == Enums.FederateType.gateway)
                str.AppendFormat("  Gateway Type: {0}\n", GatewayType);
            if (FederateType == Enums.FederateType.filter)
                str.AppendFormat("  Filter:\n\t{0}\n", Filter.ToString());
            if (Sources.Count > 0)
            {
                foreach (Source source in Sources)
                    str.AppendFormat("  Subscribe:\n\t{0}\n", source.ToString());
            }
            if (Publish != null)
                str.AppendFormat("  Federate Publish:\n\t{0}\n", Publish.ToString());
            if (Entities.Count > 0)
            {
                foreach (Entity entity in Entities)
                    str.AppendFormat("  Entity:\n\t{0}\n", entity.ToString());
            }
            return str.ToString();
        }
    }
}