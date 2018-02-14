using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// System object
    /// </summary>
    public class DeploySystem
    {
        /// <summary>
        /// Federate Name
        /// </summary>
        public string FederateName;
        /// <summary>
        /// Components
        /// </summary>
        public List<Component> Components;
        /// <summary>
        /// System type
        /// </summary>
        public Enums.FederateType SystemType;
        /// <summary>
        /// Pattern name
        /// </summary>
        public string Pattern;
        /// <summary>
        /// Pattern reference
        /// </summary>
        public Guid PatternRef;
        /// <summary>
        /// Construct System object
        /// </summary>
        public DeploySystem()
        {
            Components = new List<Component>();
        }


        /// <summary>
        /// Construct System object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public DeploySystem(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise System object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        { 
            if (fpdl.Name != "system")
                throw new ApplicationException("Cannot parse: Not an FPDL system description");
            try
            {
                SystemType = (Enums.FederateType)Enum.Parse(typeof(Enums.FederateType), fpdl.Attribute("systemType").Value);
                Pattern = fpdl.Element("pattern").Value;
                PatternRef = Guid.Parse(fpdl.Element("pattern").Attribute("patternReference").Value);

                foreach (XElement component in fpdl.Elements("component"))
                    Components.Add(new Component(component));
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid system type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("System parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise System object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("system",
                    new XAttribute("systemType", SystemType.ToString()),
                new XElement("pattern", Pattern,
                    new XAttribute("patternReference", PatternRef.ToString())
                )
            );
            foreach (Component component in Components)
            {
                fpdl.Add(component.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of System object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("\nSystem\n");
            str.AppendFormat("  Type: {0}\n", SystemType);
            str.AppendFormat("  Pattern: {0} ({1})\n", Pattern, PatternRef);
            foreach (Component comp in Components)
                str.AppendFormat("\n{0}", comp);
            return str.ToString();
        }
    }
}