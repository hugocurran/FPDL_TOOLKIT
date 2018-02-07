using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Federate publish
    /// </summary>
    public class PolicyPublish
    {
        /// <summary>
        /// Objects to publish
        /// </summary>
        public List<PolicyHlaObject> Objects = new List<PolicyHlaObject>();
        /// <summary>
        /// Interactions to publish
        /// </summary>
        public List<PolicyHlaInteraction> Interactions = new List<PolicyHlaInteraction>();

        /// <summary>
        /// Construct a Publish object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PolicyPublish(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Publish object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "publish")
                throw new ApplicationException("Cannot parse: Not a publish description");
            try
            {
                if (fpdl.Element("object") != null)
                {
                    foreach (XElement obj in fpdl.Elements("object"))
                        Objects.Add(new PolicyHlaObject(obj));
                }
                if (fpdl.Element("interaction") != null)
                {
                    foreach (XElement inter in fpdl.Elements("interaction"))
                        Interactions.Add(new PolicyHlaInteraction(inter));
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Module parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Publish onject to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("publish");
            if (Objects.Count > 0)
            {
                foreach (PolicyHlaObject obj in Objects)
                    fpdl.Add(obj.ToFPDL());
            }
            if (Interactions.Count > 0)
            {
                foreach (PolicyHlaInteraction inter in Interactions)
                    fpdl.Add(inter.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of Module object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("publish\n");
            foreach (PolicyHlaObject obj in Objects)
                str.Append(obj.ToString());
            foreach (PolicyHlaInteraction inter in Interactions)
                str.Append(inter.ToString());
            return str.ToString();
        }
    }
}
    
