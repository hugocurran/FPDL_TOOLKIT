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
        Common.System.Type FederateType;
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
        public Gateway.Type GatewayType;
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
                FederateType = (Common.System.Type)Enum.Parse(typeof(Common.System.Type), fpdl.Attribute("federateType").Value);
                FederateName = fpdl.Element("federateName").Value;
                PolicyReference = Guid.Parse(fpdl.Element("policyReference").Value);
                if (fpdl.Element("simulatorName") != null)
                    SimulatorName = fpdl.Element("simulatorName").Value;

                if ((FederateType == Common.System.Type.Gateway) && (fpdl.Element("gateway") == null))
                    throw new ApplicationException("Missing gateway specification for a Gateway federateType");
                else
                    GatewayType = (Gateway.Type)Enum.Parse(typeof(Gateway.Type), fpdl.Element("gateway").Attribute("type").Value);

                if ((FederateType == Common.System.Type.Gateway) && (fpdl.Element("gateway") == null))
                    throw new ApplicationException("Missing gateway specification for a Gateway federateType");
                else
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
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("federate",
                new XAttribute("federateType", FederateType.ToString()),
                new XElement("federateName", FederateName),
                new XElement("policyReference", PolicyReference)
                );
            if (SimulatorName != null)
                fpdl.Add(new XElement("simulatorName", SimulatorName));
            if (FederateType == Common.System.Type.Gateway)
                fpdl.Add(new XElement("gateway", new XAttribute("type", GatewayType.ToString())));
            if (FederateType == Common.System.Type.Filter)
                fpdl.Add(new XElement("filter", Filter.ToFPDL()));
            if (Sources.Count > 0)
            {
                foreach (Source source in Sources)
                    fpdl.Add(source.ToFPDL());
            }
            if (Publish != null)
                fpdl.Add(Publish.ToFPDL());
            if (Entities.Count > 0)
            {
                foreach (Entity entity in Entities)
                    fpdl.Add(entity.ToFPDL());
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
            if (FederateType == Common.System.Type.Gateway)
                str.AppendFormat("  Gateway Type: {0}\n", GatewayType);
            if (FederateType == Common.System.Type.Filter)
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